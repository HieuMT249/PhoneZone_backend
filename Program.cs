using DocumentFormat.OpenXml.InkML;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using phonezone_backend.Data;
using phonezone_backend.Services;
using phonezone_backend.Services.VNPay;
using System.IO;

namespace phonezone_backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Đăng ký DbContext
            builder.Services.AddDbContext<PhoneZoneDBContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("PhoneZoneDBContext")
                    ?? throw new InvalidOperationException("Connection string 'PhoneZoneDBContext' not found.")));

            // Đăng ký ExcelService
            builder.Services.AddTransient<ExcelService>();
            builder.Services.AddTransient<BrandExcelService>();

            // Đăng ký các dịch vụ khác
            builder.Services.AddControllers();
            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
            });
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("Accept", policy =>
                {
                    policy.WithOrigins("http://localhost:5173")
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                });
            });

            //kết nối vnpay
            builder.Services.AddScoped<IVnPayService, VnPayService>();

            // Xây dựng ứng dụng
            var app = builder.Build();

            // Thêm dữ liệu từ Excel vào cơ sở dữ liệu
            Task.Run(() =>
            {
                var scope = app.Services.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<PhoneZoneDBContext>();
                var excelService = scope.ServiceProvider.GetRequiredService<ExcelService>();
                var brandService = scope.ServiceProvider.GetRequiredService<BrandExcelService>();

                var existingProductsCount = dbContext.Products.Count();
                if (existingProductsCount > 0)
                {
                    Console.WriteLine("Sản phẩm đã có trong cơ sở dữ liệu. Bỏ qua quá trình nhập sản phẩm từ Excel.");
                    return;
                }

                // Đường dẫn tới file Excel
                string excelPath = @"Data/data.xlsx";
                string brandPath = @"Data/brand.xlsx";
                var products = excelService.ReadDataFromExcel(excelPath);
                var brands = brandService.ReadDataFromExcel(brandPath);

                var existingBrands = dbContext.Brands
                    .Where(b => brands.Select(br => br.Name).Contains(b.Name))
                    .ToList();

                var newBrands = brands
                    .Where(brand => !existingBrands.Any(b => b.Name == brand.Name))
                    .ToList();


                dbContext.Brands.AddRange(newBrands);
                dbContext.SaveChanges();

                var existingProducts = dbContext.Products
                    .Where(p => products.Select(prod => prod.ProductName).Contains(p.ProductName))
                    .ToList();

                var newProducts = products
                    .Where(product => !existingProducts.Any(ep => ep.ProductName == product.ProductName && ep.Branch == product.Branch))
                    .ToList();


                dbContext.Products.AddRange(newProducts);
                dbContext.SaveChanges();

                foreach (var product in newProducts)
                {
                    if (product.Specification != null)
                    {
                        product.Specification.ProductId = product.Id;
                    }
                }

                dbContext.Specifications.AddRange(newProducts.Select(p => p.Specification).Where(d => d != null));
                dbContext.SaveChanges();

                Console.WriteLine("Dữ liệu đã được nhập vào cơ sở dữ liệu thành công!");
            });


            // Cấu hình pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("Accept");

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}