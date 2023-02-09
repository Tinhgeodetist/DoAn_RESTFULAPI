using DoAn_CSC_API.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoAn_CSC_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HinhController : ControllerBase
    {

        private readonly IHinhService _ihinhService;
        public HinhController(IHinhService ihinhService)
        {
            _ihinhService = ihinhService;
        }
        [HttpPost("DanhSachHinh")]
        public List<HinhModel.Output.ThongTinHinh> DanhSachHinh(HinhModel.Input.DocDanhSachHinh input)
        {
            var hinhs = _ihinhService.DocDanhSachHinh(input.QuanTri);
            var dshinh = hinhs.Select(x => new HinhModel.Output.ThongTinHinh()
            {
                HinhId = x.HinhId,
                Carousel = x.Carousel,
                Thumbnails = x.Thumbnails
            }).ToList();
            return dshinh;
        }
        [HttpPost("ThemHinh")]
        public ThongBaoModel ThemHinhMoi(HinhModel.Input.ThongTinHinh input)
        {
            var thongbao = new ThongBaoModel { MaSo = 1, NoiDung = "" };
            try
            {
                var hinh = _ihinhService.Them(new Services.Models.Hinh()
                {
                    HinhId = input.HinhId,
                    Carousel = input.Carousel,
                    Thumbnails = input.Thumbnails
                });
                if (hinh != null)
                {
                    thongbao.NoiDung = hinh.HinhId.ToString();
                }
                else
                {
                    thongbao.MaSo = 1;
                    thongbao.NoiDung = "Không thêm được hình";
                }

            }
            catch (Exception)
            {
                thongbao.MaSo = 1;
                thongbao.NoiDung = "Không thêm được Hình";
            }
            return thongbao;
        }
        [HttpPost("ThongTinHinh")]
        public HinhModel.Output.ThongTinHinh ThongTinHinh(HinhModel.Input.ThongTinHinh input)
        {
            var Hinh = _ihinhService.DocThongTinHinh(input.HinhId);
            HinhModel.Output.ThongTinHinh thongtinHinh = new HinhModel.Output.ThongTinHinh()
            {
                HinhId = input.HinhId,
                Carousel = input.Carousel,
                Thumbnails = input.Thumbnails,
                KichHoat = input.KichHoat,
            };
            return thongtinHinh;
        }
        [HttpPost("CapNhatHinh")]
        public ThongBaoModel CapNhatHinh(HinhModel.Input.ThongTinHinh input)
        {
            var tb = new ThongBaoModel { MaSo = 1, NoiDung = "" };
            try
            {
                Services.Models.Hinh hinh = new()
                {
                    HinhId = input.HinhId,
                    Carousel = input.Carousel,
                    Thumbnails = input.Thumbnails
                };
                if (!_ihinhService.Sua(hinh))
                {
                    tb.MaSo = 1;
                    tb.NoiDung = "Không cập nhật đươc Hình";

                }

            }
            catch(Exception )
            {
                tb.MaSo = 1;
                tb.NoiDung = "Không cập nhật đươc Hình";
            }
            return tb;
        }
        [HttpPost("XoaHinh")]
        public ThongBaoModel XoaHinh(HinhModel.Input.XoaHinh input)
        {
            var tb = new ThongBaoModel { MaSo = 1, NoiDung = "" };
            if (input.Id > 0)
            {
                var Hinh = _ihinhService.DocThongTinHinh(input.Id);
                if (Hinh != null && _ihinhService.Xoa(Hinh))
                    tb.NoiDung = Hinh.ToString();
                else
                {
                    tb.MaSo = 1;
                    tb.NoiDung = "Không xóa được Hình";
                }
                
            }
            else
            {
                tb.MaSo = 1;
                tb.NoiDung = "Không xóa được Hình";
            }
            return tb;
        }

    }
}
