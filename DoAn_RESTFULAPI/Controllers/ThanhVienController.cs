using DoAn_CSC_API.Common;
using DoAn_CSC_API.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLRapChieuPhimAPI.Auth;
using Services.IServices;
using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Models;

namespace DoAn_CSC_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThanhVienController : ControllerBase
    {
        private readonly IThanhVienService _iThanhVien;
        private readonly IJwtAuthManager _jwtAuthManager;
        public ThanhVienController(IThanhVienService iThanhVien)
        {
            _iThanhVien = iThanhVien;
        }

        [HttpPost("DangNhapAnDanh")]
        public ThanhVienModel.Output.ThongTinThanhVien DangNhapAnDanh()
        {
            var userInfo = new UserInfo
            {
                Id = 0,
                Email = "",
                HoTen = "",
                Username = Guid.NewGuid().ToString(),
                UserAgent = HttpContext.Request.Headers["User-Agent"].ToString()
            };
            ThanhVienModel.Output.ThongTinThanhVien thongTinThanhVien = new()
            {
                Id = 0,
                Email = "",
                DienThoai = "",
                GioiTinh = false,
                HoTen = "Anonymous",
                KichHoat = true,
                MatKhau = "",
                NgaySinh = DateTime.Today.Date,
                SocialLogin = "",
                AccessToken = _jwtAuthManager.CreateToken(userInfo)
            };
            return thongTinThanhVien;
        }

        [HttpPost("DangNhap")]
        public ThanhVienModel.Output.ThongTinThanhVien DangNhap(ThanhVienModel.Input.ThongTinDangNhap input)
        {
            ThanhVienModel.Output.ThongTinThanhVien thongTinThanhVien = new();
            var thanh_vien = _iThanhVien.DangNhap(input.TenDangNhap, input.MatKhau);
            if (thanh_vien != null && thanh_vien.Id > 0)
            {
                Utilities.PropertyCopier<User, ThanhVienModel.Output.ThongTinThanhVien>.Copy(thanh_vien, thongTinThanhVien);
                var userInfo = new UserInfo
                {
                    Id = thanh_vien.Id,
                    Email = thanh_vien.Email,
                    HoTen = thanh_vien.TenLot+ thanh_vien.Ten,
                    Username = thanh_vien.Email,
                    UserAgent = HttpContext.Request.Headers["User-Agent"].ToString()
                };
                thongTinThanhVien.AccessToken = _jwtAuthManager.CreateToken(userInfo);
            }
            
            return thongTinThanhVien;
        }

        [Authorize]
        [HttpPost("DangXuat")]
        public bool Logout(ThanhVienModel.Input.ThongTinDangNhap input)
        {
            //string rawUserId = HttpContext.User.Identity.Name;
            try
            {
                _jwtAuthManager.RemoveTokenByUserName(input.TenDangNhap);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            
        }

        [HttpPost("DangKyThanhVien")]
        public ThongBaoModel DangKyThanhVien(ThanhVienModel.Input.ThongTinThanhVienMoi thanh_vien)
        {
            ThongBaoModel tb = new ThongBaoModel { MaSo = 1 };
            try
            {
                var chuoiTB = "";
                if (string.IsNullOrEmpty(thanh_vien.Email))
                    chuoiTB = "Email phải khác rỗng";
                else
                {
                    var thanhvien = _iThanhVien.TimThanhVien(thanh_vien.Email);
                    if (thanhvien != null)
                        chuoiTB = "Email này đã được sử dụng rồi.";
                }
                if (string.IsNullOrEmpty(thanh_vien.HoTen))
                    chuoiTB = "Họ tên phải khác rỗng";
                if (string.IsNullOrEmpty(thanh_vien.DienThoai))
                    chuoiTB = "Điện thoại phải khác rỗng";
                if (string.IsNullOrEmpty(chuoiTB))
                {
                    var thanh_vien_moi = new User();
                    Utilities.PropertyCopier<ThanhVienModel.Input.ThongTinThanhVienMoi, User>.Copy(thanh_vien, thanh_vien_moi);
                    var tb_ = _iThanhVien.DangKyThanhVien(thanh_vien_moi);
                    tb.MaSo = tb_.MaSo;
                    chuoiTB = tb_.NoiDung;
                }
                tb.NoiDung = chuoiTB;
            }
            catch (Exception ex)
            {
                tb.NoiDung = "Lỗi: " + ex.Message;
            }
            return tb;
        }
        [HttpPost("ThayDoiMatKhau")]
        public ThongBaoModel ThayDoiMatKhau(ThanhVienModel.Input.ThongTinThayDoiMatKhau thanh_vien)
        {
            ThongBaoModel tb = new ThongBaoModel();
            var tb_ = _iThanhVien.ThayDoiMatKhau(thanh_vien.Id, thanh_vien.Username, thanh_vien.MatKhauCu, thanh_vien.MatKhauMoi);
            tb.MaSo = tb_.MaSo;
            tb.NoiDung = tb_.NoiDung;
            return tb;
        }
        [HttpPost("ThongTinThanhVien")]
        public ThanhVienModel.Output.ThongTinThanhVien ThongTinThanhVien(ThanhVienModel.Input.ThongTinThanhVien input)
        {
            var thanh_vien = _iThanhVien.DocThongTinThanhVien(input.Id);
            var thanh_vien_ = new ThanhVienModel.Output.ThongTinThanhVien();
            Utilities.PropertyCopier<User, ThanhVienModel.Output.ThongTinThanhVien>.Copy(thanh_vien, thanh_vien_);
            return thanh_vien_;
        }
        [HttpPost("ThemThanhVien")]
        public ThongBaoModel ThemThanhVien(ThanhVienModel.Input.ThongTinThanhVienMoi thanh_vien_moi)
        {
            ThongBaoModel tb = new ThongBaoModel();
            User thanh_vien = new();
            Utilities.PropertyCopier<ThanhVienModel.Input.ThongTinThanhVienMoi, User>.Copy(thanh_vien_moi, thanh_vien);
            var tb_ = _iThanhVien.ThemThanhVien(thanh_vien);
            tb.MaSo = tb_.MaSo;
            tb.NoiDung = tb_.NoiDung;

            return tb;
        }
        [HttpPost("KichHoatTaiKhoan")]
        public ThongBaoModel KichHoatTaiKhoan(ThanhVienModel.Input.KichHoatTaiKhoan input)
        {
            ThongBaoModel tb = new ThongBaoModel();
            var tb_ = _iThanhVien.KichHoatTaiKhoan(input.Email);
            tb.MaSo = tb_.MaSo;
            tb.NoiDung = tb_.NoiDung;

            return tb;
        }



        //[HttpPost("DangNhap")]
        //public ThanhVienModel.Output.DangNhap DangNhap(ThanhVienModel.Input.DangNhap input)
        //{
        //    ThanhVienModel.Output.DangNhap thongTinThanhVien = new();
        //    var tv = _iThanhVien.DangNhap(input.Email, input.MatKhau);

        //    if (tv != null && tv.Id > 0)
        //    {
        //        //thongTinThanhVien = new ThanhVienModel.Output.DangNhap
        //        //{
        //        //    HoTen = tv.HoTen,
        //        //    DienThoai = tv.DienThoai,
        //        //    Email = tv.Email,
        //        //    GioiTinh = tv.GioiTinh,
        //        //    Id = tv.Id,
        //        //    KichHoat = tv.KichHoat,
        //        //    MatKhau = tv.MatKhau,
        //        //    NgaySinh = tv.NgaySinh,
        //        //    SocialLogin = tv.SocialLogin,
        //        //    ThongBao = tv.SocialLogin
        //        //};
        //        Utilities.PropertyCopier<ThanhVien, ThanhVienModel.Output.DangNhap>.Copy(tv, thongTinThanhVien);
        //        // dùng hàm
        //    }
        //    return thongTinThanhVien;
        //}
        //[HttpPost("DangKyThanhVien")]
        //public ThongBaoModel DangKyThanhVien(ThanhVienModel.Input.DangKyThanhVien input)
        //{
        //    ThongBaoModel tb = new ThongBaoModel { Maso = 0, Noidung = "" };
        //    try
        //    {
        //        //var thanhvienmoi = new Service.Models.ThanhVien
        //        //{
        //        //    Email = input.Email,
        //        //    MatKhau = input.MatKhau,
        //        //    DienThoai = input.DienThoai,
        //        //    GioiTinh = input.GioiTinh,
        //        //    HoTen = input.HoTen,
        //        //    KichHoat = false,
        //        //    NgaySinh = input.NgaySinh,
        //        //    SocialLogin = input.SocialLogin
        //        //};
        //        var thanhvienmoi = new Service.Models.ThanhVien();
        //        Common.Utilities.PropertyCopier<ThanhVienModel.Input.DangKyThanhVien, Service.Models.ThanhVien>.Copy(input, thanhvienmoi);
        //        var ketqua = _iThanhVien.DangKyThanhVien(thanhvienmoi);
        //        if (ketqua != null)
        //        {
        //            tb.Maso = 0;
        //            tb.Noidung = ketqua.Id.ToString();
        //        }
        //        else
        //        {
        //            tb.Maso = 1;
        //            tb.Noidung = "Đăng ký không thành công vui lòng thử lại";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        tb.Noidung = "Lỗi đăng ký: " +ex.Message;
        //        throw;
        //    }
        //    return tb;
        //}

        //[HttpPost("KichHoatTaiKhoan")]
        //public ThongBaoModel KichHoatTaiKhoan(ThanhVienModel.Input.KichHoatTaiKhoan input)
        //{
        //    ThongBaoModel tb = new ThongBaoModel { Maso = 0, Noidung = "" };
        //    try
        //    {
        //        var ketqua = _iThanhVien.KickHoatTaiKhoan(input.Email);
        //        if (ketqua == true)
        //        {
        //            tb.Maso = 0;
        //            tb.Noidung = "Kích hoạt thành viên mới thành công.";
        //        }
        //        else
        //        {
        //            tb.Maso = 1;
        //            tb.Noidung = "Kích hoạt không thành công vui lòng thử lại";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        tb.Noidung = "Lỗi kích hoạt: " + ex.Message;
        //        throw;
        //    }
        //    return tb;
        //}

        //[HttpPost("DoiMatKhau")]
        //public ThongBaoModel DoiMatKhau(ThanhVienModel.Input.ThayDoiMatKhau input)
        //{
        //    ThongBaoModel tb = new ThongBaoModel { Maso = 0, Noidung = "" };
        //    try
        //    {
        //        var tv = _iThanhVien.DocThongTin(input.Id);

        //        if (tv != null && tv.MatKhau == input.MatKhauCu)
        //        {
        //            tv.MatKhau = input.MatKhauMoi;
        //            var ketqua = _iThanhVien.ThayDoiMatKhau(tv);
        //            if (ketqua == true)
        //            {
        //                tb.Maso = 0;
        //                tb.Noidung = "Thay đổi mật khẩu thành công";
        //            }
        //            else
        //            {
        //                tb.Maso = 1;
        //                tb.Noidung = "Thay đổi mật khẩu không thành công";
        //            }
        //        }
        //        else
        //        {
        //            tb.Maso = 2;
        //            tb.Noidung = "thông tin tài khoản khôn hợp lệ";
        //        }
        //    }
        //    catch (Exception)
        //    {
                
        //        throw;
        //    }
        //    return tb;
        //}
    }
}
