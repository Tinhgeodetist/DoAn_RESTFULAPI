using Service.Models;
using Services.IServices;
using Services.Models;
using Services.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class ThanhVienService : Repository<User>, IThanhVienService
    {
        public ThanhVienService(QLShopContext context) : base(context) { }
        public ThongBao DangKyThanhVien(User thanh_vien_moi)
        {
            var tb = new ThongBao { MaSo = 1 };
            try
            {
                var thanh_vien = base.Them(thanh_vien_moi);

                if (thanh_vien != null)
                {
                    tb.MaSo = 0;
                    tb.NoiDung = thanh_vien.Id.ToString();
                }
                else
                    tb.NoiDung = "Lưu thông tin thành viên mới không thành công";
            }
            catch (Exception ex)
            {
                tb.NoiDung = "Lỗi: " + ex.Message;
            }
            return tb;
        }

        public ThongTinThanhVien DangNhap(string TenDangNhap, string MatKhau)
        {
            ThongTinThanhVien thongtinThanhVien = new();
            if (!string.IsNullOrEmpty(TenDangNhap) && !string.IsNullOrEmpty(MatKhau))
            {
                try
                {
                    var thanh_vien = DocTheoDieuKien(x => x.Email.Equals(TenDangNhap)
                                        && x.Password.Equals(MatKhau)).FirstOrDefault();
                    if (thanh_vien != null)
                    {
                        thongtinThanhVien = new ThongTinThanhVien
                        {
                            Id = thanh_vien.Id,
                            Ten = thanh_vien.Ten,
                            TenLot = thanh_vien.TenLot,
                            Diachi = thanh_vien.Diachi,
                            SoDienThoai = thanh_vien.SoDienThoai,
                            Password = thanh_vien.Password,
                            NgayTao = thanh_vien.NgayTao
                        };
                    }
                    else
                    {
                        thongtinThanhVien.ThongBao = "Tên đăng nhập hoặc mật khẩu không hợp lệ";
                    }
                }
                catch (Exception ex)
                {
                    thongtinThanhVien.ThongBao = "Lỗi đăng nhập: " + ex.Message;
                }
            }
            else
            {
                thongtinThanhVien.ThongBao = "Tên đăng nhập và mật khẩu phải khác rỗng.";
            }
            return thongtinThanhVien;
        }

        public User DocThongTinThanhVien(int Id)
        {
            User thongtinThanhVien = new();
            if (Id > 0)
            {
                try
                {
                    thongtinThanhVien = DocTheoDieuKien(x => x.Id.Equals(Id)).FirstOrDefault();
                }
                catch (Exception)
                {

                }
            }
            return thongtinThanhVien;
        }

        public ThongBao KichHoatTaiKhoan(string email)
        {
            ThongBao tb = new ThongBao { MaSo = 1 };
            try
            {
                var thanhVienCapNhat = DocTheoDieuKien(p => p.Email.Equals(email)).FirstOrDefault();
                if (thanhVienCapNhat != null)
                {
                    thanhVienCapNhat.KichHoat = true;
                    if (base.Sua(thanhVienCapNhat))
                    {
                        tb.MaSo = 0;
                        tb.NoiDung = "Kích hoạt tài khoản thành công";
                    }
                    else
                        tb.NoiDung = "Kích hoạt tài khoản không thành công";
                }
            }
            catch (Exception ex)
            {
                tb.NoiDung = "Lỗi: " + ex.Message;
            }
            return tb;
        }

        public ThongBao ThayDoiMatKhau(int Id, string Username, string MatKhauCu, string MatKhauMoi)
        {
            ThongBao tb = new ThongBao { MaSo = 1 };
            try
            {
                var thanhVienCapNhat = DocTheoDieuKien(p => p.Id.Equals(Id) && p.Email.Equals(Username)).FirstOrDefault();
                if (thanhVienCapNhat != null)
                {
                    if (thanhVienCapNhat.Password == MatKhauCu)
                    {
                        thanhVienCapNhat.Password = MatKhauMoi;
                        if (base.Sua(thanhVienCapNhat))
                        {
                            tb.MaSo = 0;
                            tb.NoiDung = "Thay đổi mật khẩu thành công";
                        }
                        else
                            tb.NoiDung = "Thay đổi mật khẩu không thành công";
                    }
                    else
                        tb.NoiDung = "Mật khẩu cũ không đúng.";
                }
            }
            catch (Exception ex)
            {
                tb.NoiDung = "Lỗi: " + ex.Message;
            }
            return tb;
        }

        public ThongBao ThemThanhVien(User thanh_vien_moi)
        {
            ThongBao tb = new ThongBao { MaSo = 1 };
            try
            {
                var chuoiTB = "";
                if (string.IsNullOrEmpty(thanh_vien_moi.Email))
                    chuoiTB = "Email phải khác rỗng";
                else
                {
                    var thanhvien = DocDanhSach(x => x.Email.Equals(thanh_vien_moi.Email)).FirstOrDefault();
                    if (thanhvien != null)
                        chuoiTB = "Email này đã được sử dụng rồi.";
                }
                if (string.IsNullOrEmpty(thanh_vien_moi.Ten)&& string.IsNullOrEmpty( thanh_vien_moi.TenLot))
                    chuoiTB = "Họ tên phải khác rỗng";
                if (string.IsNullOrEmpty(thanh_vien_moi.SoDienThoai))
                    chuoiTB = "Điện thoại phải khác rỗng";
                if (string.IsNullOrEmpty(chuoiTB))
                {
                    var thanh_vien = base.Them(thanh_vien_moi);
                    if (thanh_vien != null)
                    {
                        tb.MaSo = 0;
                        chuoiTB = thanh_vien.Id.ToString();
                    }
                    else
                        chuoiTB = "Lưu thông tin thành viên mới không thành công";
                }
                tb.NoiDung = chuoiTB;
            }
            catch (Exception ex)
            {
                tb.NoiDung = "Lỗi: " + ex.Message;
            }
            return tb;
        }

        public User TimThanhVien(string Email)
        {
            return DocTheoDieuKien(x => x.Email.Equals(Email)).FirstOrDefault();
        }

    }
}
