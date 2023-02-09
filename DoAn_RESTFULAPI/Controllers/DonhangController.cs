using DoAn_CSC_API.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.IServices;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoAn_CSC_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonhangController : ControllerBase
    {

        private readonly ISanPhamService _iSanphamService;
        private readonly ILoaihangService _iloaihangService;
        private readonly IDonhangService _idonhangService;
        public DonhangController(IDonhangService idonhangService, ISanPhamService iSanphamService, ILoaihangService iloaihangService)
        {
            _iSanphamService = iSanphamService;
            _idonhangService = idonhangService;
            _iloaihangService = iloaihangService;
        }
        [HttpPost("MuaDonHang")]
        public ThongBaoModel MuaDonHang(DonhangModel.Input.MuaDonHang input)
        {
            var tb = new ThongBaoModel { MaSo = 0, NoiDung = "" };
            try
            {
                if (input == null || input.DanhSachDonHang.Count == 0)
                {
                    tb.MaSo = 1;
                    tb.NoiDung = "Thông tin đơn hàng không hợp lệ";
                }
                else
                {
                    var dsDonhang = new List<Donhang>();
                    foreach (var don in input.DanhSachDonHang)
                    {
                        var donmua = new Donhang()
                        {
                            NgayTao = DateTime.Today.Date,
                            Id = don.Id,
                            UserId = don.Id,
                            Ten = don.Ten,
                            SoDienThoai = don.SoDienThoai,
                            DiaChi = don.DiaChi,
                            GiamGia = don.GiamGia,
                            TinhTrangDonHang = 2,
                            TongTien = don.TongTien,
                            NoiDung = don.NoiDung
                        };
                        dsDonhang.Add(donmua);
                    }
                    if (!_idonhangService.MuaDonHang(dsDonhang))
                    {
                        tb.MaSo = 3;
                        tb.NoiDung = "Có lỗi trong quá trình mua! Xin thử lại";
                    };
                }
            }
            catch (Exception ex)
            {
                tb.MaSo =  2;
                tb.NoiDung = "Lỗi mua vé. " + ex.Message;
            }
            return tb;
        }
        [HttpPost("DanhSachDonHangTheoUser")]
        public List<DonhangModel.Output.ThongTinDonHang> DanhSachDonHangTheoUser(DonhangModel.Input.DocDanhSachDonHangTheoUser input)
        {
            var dsDonhang = _idonhangService.DocDanhSachDonHangTheoUser(input.UserId)
                .Select(x => new DonhangModel.Output.ThongTinDonHang()
                {
                    Id = x.Id,
                    Ten = x.Ten,
                    SoDienThoai = x.SoDienThoai,
                    NgayTao = DateTime.Today.Date,

                    UserId = x.Id,
                    DiaChi = x.DiaChi,
                    GiamGia = x.GiamGia,
                    TinhTrangDonHang = 2,
                    TongTien = x.TongTien,
                    NoiDung = x.NoiDung,


                }).ToList();
            return dsDonhang;
        }
        

    }
}
