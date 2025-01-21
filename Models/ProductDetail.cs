using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace phonezone_backend.Models
{
    public class ProductDetail
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductDescription { get; set; } //thong tin san pham
        public string Camera {  get; set; } 
        public string StorageCapacity { get; set; } //dung luong luu tru
        public string FastCharging { get; set; } //ho tro sac nhanh
        public string FaceID { get; set; }
        public string Technology { get; set; } //cong nghe
        public string Style { get; set; } //kieu dang
        public string Size { get; set; } //kich thuoc
        public string Model { get; set; } 
        public string Colour { get; set; } //mau sac
        public string Weight { get; set; }  //trong luong
        public string Video { get; set; } //quay video
        public string CameraTrueDepth { get; set; }
        public string ChargingConnectivity { get; set; } //sac va ket noi
        public string Battery { get; set; } //Pin và nguồn điện
        public string Country { get; set; } //Thương hiệu
        public string Company { get; set; } //Hang
        public string Guarantee {  get; set; } //Bảo hành
        public string WaterResistant {  get; set; } //Kháng nước, bụi
        public string CameraFeatures { get; set; } //tinh nang camera
        public string GPU {  get; set; } //GPU
        public string Pin { get; set; } //loai Pin
        public string ChargingSupport { get; set; } //Hỗ trợ sạc tối đa
        public string NetworkSupport { get; set; } //Hỗ trợ mạng
        public string WiFi { get; set; } //Wi-fi
        public string Bluetooth { get; set; } //Bluetooth
        public string GPS { get; set; } //GPS
        public string ChargingTechnology { get; set; } //Công nghệ sạc
        public string FingerprintSensor { get; set; } //cam bien van tay
        public string SpecialFeatures { get; set; } //tinh nang dac biet
        public string RearCamera { get; set; } //camera sau
        public string FrontCamera { get; set; } //camera truoc
        public string SIM {  get; set; } //SIM
        public string Sensor { get; set; } //cam bien
        public string Ram {  get; set; } //ram
        public string CPU { get; set; } //loai CPU
        public string NFC { get; set; } //Công nghệ NFC
        public string Chip { get; set; } //Chip
        public string ScreenResolution { get; set; } //do phan giai man hinh
        public string ScreenFeatures { get; set; } //Tính năng màn hình
        public string InternalMemory { get; set; } //Bộ nhớ trong
        public string BatteryCapacity { get; set; } //Dung Lượng Pin
        public string ScreenSize { get; set; } //Kích thước màn hình
        public string Screen {  get; set; } //man hinh
        public string OperatingSystem { get; set; } //Hệ điều hành


        public virtual Product Products { get; set; }
    }
}
