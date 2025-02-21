using phonezone_backend.Libraries;
using phonezone_backend.Models.VNPay;

namespace phonezone_backend.Services.VNPay
{
    public class VnPayService : IVnPayService
    {
        private readonly IConfiguration _configuration;

        public VnPayService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string CreatePaymentUrl(PaymentInformationModel model, HttpContext context)
        {
            var timeZoneById = TimeZoneInfo.FindSystemTimeZoneById(_configuration["TimeZoneId"]);
            var timeNow = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZoneById);
            var tick = DateTime.Now.Ticks.ToString();
            var expireTime = timeNow.AddMinutes(30);
            var expireDate = expireTime.ToString("yyyyMMddHHmmss");
            var pay = new VnpayLibrary();
            var urlCallBack = _configuration["Vnpay:PaymentBackReturnUrl"];

            // In các giá trị cấu hình và tham số đầu vào
            Console.WriteLine("===== INFOR TO SEND TO VNPAY =====");
            Console.WriteLine("vnp_Version: " + _configuration["Vnpay:Version"]);
            Console.WriteLine("vnp_Command: " + _configuration["Vnpay:Command"]);
            Console.WriteLine("vnp_TmnCode: " + _configuration["Vnpay:TmnCode"]);
            Console.WriteLine("vnp_Amount: " + ((int)model.Amount).ToString());
            Console.WriteLine("vnp_CreateDate: " + timeNow.ToString("yyyyMMddHHmmss"));
            Console.WriteLine("vnp_CurrCode: " + _configuration["Vnpay:CurrCode"]);
            Console.WriteLine("vnp_IpAddr: " + pay.GetIpAddress(context));
            Console.WriteLine("vnp_Locale: " + _configuration["Vnpay:Locale"]);
            Console.WriteLine("vnp_OrderInfo: " + $"{model.Name} {model.OrderDescription} {model.Amount}");
            Console.WriteLine("vnp_OrderType: " + model.OrderType);
            Console.WriteLine("vnp_ReturnUrl: " + urlCallBack);
            Console.WriteLine("vnp_TxnRef: " + tick);
            Console.WriteLine("vnp_ExpireDate: " + expireDate);

            pay.AddRequestData("vnp_Version", _configuration["Vnpay:Version"]);
            pay.AddRequestData("vnp_Command", _configuration["Vnpay:Command"]);
            pay.AddRequestData("vnp_TmnCode", _configuration["Vnpay:TmnCode"]);
            pay.AddRequestData("vnp_Amount", ((int)model.Amount*100).ToString());
            //pay.AddRequestData("vnp_Amount", (model.Amount).ToString());
            pay.AddRequestData("vnp_CreateDate", timeNow.ToString("yyyyMMddHHmmss"));
            pay.AddRequestData("vnp_CurrCode", _configuration["Vnpay:CurrCode"]);
            pay.AddRequestData("vnp_IpAddr", pay.GetIpAddress(context));
            pay.AddRequestData("vnp_Locale", _configuration["Vnpay:Locale"]);
            pay.AddRequestData("vnp_OrderInfo", $"{model.Name} {model.OrderDescription} {model.Amount}");
            pay.AddRequestData("vnp_OrderType", model.OrderType);
            pay.AddRequestData("vnp_ReturnUrl", urlCallBack);
            pay.AddRequestData("vnp_TxnRef", tick);
            pay.AddRequestData("vnp_ExpireDate", expireDate);

            

            // In lại một lần nữa thông tin về các tham số gửi đi
            Console.WriteLine("===== FINAL PAYMENT URL PARAMETERS =====");
            Console.WriteLine("vnp_Version: " + _configuration["Vnpay:Version"]);
            Console.WriteLine("vnp_Command: " + _configuration["Vnpay:Command"]);
            Console.WriteLine("vnp_TmnCode: " + _configuration["Vnpay:TmnCode"]);
            Console.WriteLine("vnp_Amount: " + ((int)model.Amount).ToString());
            Console.WriteLine("vnp_CreateDate: " + timeNow.ToString("yyyyMMddHHmmss"));
            Console.WriteLine("vnp_CurrCode: " + _configuration["Vnpay:CurrCode"]);
            Console.WriteLine("vnp_IpAddr: " + pay.GetIpAddress(context));
            Console.WriteLine("vnp_Locale: " + _configuration["Vnpay:Locale"]);
            Console.WriteLine("vnp_OrderInfo: " + $"{model.Name} {model.OrderDescription} {model.Amount}");
            Console.WriteLine("vnp_OrderType: " + model.OrderType);
            Console.WriteLine("vnp_ReturnUrl: " + urlCallBack);
            Console.WriteLine("vnp_TxnRef: " + tick);
            Console.WriteLine("vnp_ExpireDate: " + expireDate);

            var paymentUrl =
                pay.CreateRequestUrl(_configuration["Vnpay:BaseUrl"], _configuration["Vnpay:HashSecret"]);

            return paymentUrl;
        }

        public PaymentResponseModel PaymentExecute(IQueryCollection collections)
        {
            var pay = new VnpayLibrary();
            var response = pay.GetFullResponseData(collections, _configuration["Vnpay:HashSecret"]);

            return response;
        }

    }
}
