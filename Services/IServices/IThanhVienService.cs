using Service.Models;
using Services.Models;
using Services.Repository;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IServices
{
    public interface IThanhVienService : IRepository<User> 
    {
        ThongTinThanhVien DangNhap(string TenDangNhap, string MatKhau);
        ThongBao DangKyThanhVien(User thanh_vien_moi);
        User TimThanhVien(string Email);
        ThongBao ThayDoiMatKhau(int Id, string Username, string MatKhauCu, string MatKhauMoi);
        User DocThongTinThanhVien(int Id);
        ThongBao ThemThanhVien(User thanh_vien_moi);
        ThongBao KichHoatTaiKhoan(string email);
    }
}
