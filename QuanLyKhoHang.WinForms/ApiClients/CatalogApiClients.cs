using System.Data;
using QuanLyKhoHang.Models;

namespace QuanLyKhoHang.ApiClients
{
    /// <summary>
    /// Client gọi API quản lý danh mục hàng hóa.
    /// </summary>
    public sealed class HangHoaApiClient
    {
        /// <summary>Lấy toàn bộ danh sách hàng hóa từ API.</summary>
        public DataTable GetAll() => ApiHttpClient.GetTable("api/hang-hoa");

        /// <summary>Gửi yêu cầu thêm một mặt hàng mới.</summary>
        public void Them(HangHoa hangHoa) => ApiHttpClient.Post("api/hang-hoa", hangHoa);

        /// <summary>Gửi yêu cầu cập nhật thông tin mặt hàng theo mã hàng hóa.</summary>
        public void Sua(HangHoa hangHoa) => ApiHttpClient.Put($"api/hang-hoa/{hangHoa.MaHangHoa}", hangHoa);

        /// <summary>Gửi yêu cầu xóa mặt hàng theo mã.</summary>
        public void Xoa(int id) => ApiHttpClient.Delete($"api/hang-hoa/{id}");
    }

    /// <summary>
    /// Client gọi API quản lý danh mục loại hàng.
    /// </summary>
    public sealed class LoaiHangApiClient
    {
        /// <summary>Lấy toàn bộ danh sách loại hàng từ API.</summary>
        public DataTable GetAll() => ApiHttpClient.GetTable("api/loai-hang");

        /// <summary>Gửi yêu cầu thêm loại hàng mới.</summary>
        public void Them(LoaiHang loaiHang) => ApiHttpClient.Post("api/loai-hang", loaiHang);

        /// <summary>Gửi yêu cầu cập nhật loại hàng theo mã.</summary>
        public void Sua(LoaiHang loaiHang) => ApiHttpClient.Put($"api/loai-hang/{loaiHang.MaLoaiHang}", loaiHang);

        /// <summary>Gửi yêu cầu xóa loại hàng theo mã.</summary>
        public void Xoa(int id) => ApiHttpClient.Delete($"api/loai-hang/{id}");
    }

    /// <summary>
    /// Client gọi API quản lý danh mục nhà cung cấp.
    /// </summary>
    public sealed class NhaCungCapApiClient
    {
        /// <summary>Lấy toàn bộ danh sách nhà cung cấp từ API.</summary>
        public DataTable GetAll() => ApiHttpClient.GetTable("api/nha-cung-cap");

        /// <summary>Gửi yêu cầu thêm nhà cung cấp mới.</summary>
        public void Them(NhaCungCap nhaCungCap) => ApiHttpClient.Post("api/nha-cung-cap", nhaCungCap);

        /// <summary>Gửi yêu cầu cập nhật nhà cung cấp theo mã.</summary>
        public void Sua(NhaCungCap nhaCungCap) => ApiHttpClient.Put($"api/nha-cung-cap/{nhaCungCap.MaNhaCungCap}", nhaCungCap);

        /// <summary>Gửi yêu cầu xóa nhà cung cấp theo mã.</summary>
        public void Xoa(int id) => ApiHttpClient.Delete($"api/nha-cung-cap/{id}");
    }

    /// <summary>
    /// Client gọi API quản lý danh mục khách hàng.
    /// </summary>
    public sealed class KhachHangApiClient
    {
        /// <summary>Lấy toàn bộ danh sách khách hàng từ API.</summary>
        public DataTable GetAll() => ApiHttpClient.GetTable("api/khach-hang");

        /// <summary>Gửi yêu cầu thêm khách hàng mới.</summary>
        public void Them(KhachHang khachHang) => ApiHttpClient.Post("api/khach-hang", khachHang);

        /// <summary>Gửi yêu cầu cập nhật khách hàng theo mã.</summary>
        public void Sua(KhachHang khachHang) => ApiHttpClient.Put($"api/khach-hang/{khachHang.MaKhachHang}", khachHang);

        /// <summary>Gửi yêu cầu xóa khách hàng theo mã.</summary>
        public void Xoa(int id) => ApiHttpClient.Delete($"api/khach-hang/{id}");
    }

    /// <summary>
    /// Client gọi API quản lý danh mục nhân viên.
    /// </summary>
    public sealed class NhanVienApiClient
    {
        /// <summary>Lấy toàn bộ danh sách nhân viên từ API.</summary>
        public DataTable GetAll() => ApiHttpClient.GetTable("api/nhan-vien");

        /// <summary>Gửi yêu cầu thêm nhân viên mới.</summary>
        public void Them(NhanVien nhanVien) => ApiHttpClient.Post("api/nhan-vien", nhanVien);

        /// <summary>Gửi yêu cầu cập nhật nhân viên theo mã.</summary>
        public void Sua(NhanVien nhanVien) => ApiHttpClient.Put($"api/nhan-vien/{nhanVien.MaNhanVien}", nhanVien);

        /// <summary>Gửi yêu cầu xóa nhân viên theo mã.</summary>
        public void Xoa(int id) => ApiHttpClient.Delete($"api/nhan-vien/{id}");
    }
}
