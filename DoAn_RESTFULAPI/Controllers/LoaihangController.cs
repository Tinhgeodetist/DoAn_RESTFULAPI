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
    public class LoaihangController : ControllerBase
    {
        private readonly ILoaihangService _iLoaihangService;
        public LoaihangController(ILoaihangService iLoaiHangService)
        {
            _iLoaihangService = iLoaiHangService;
        }
        [HttpPost("DanhSachLoaiHang")]
        public List<LoaihangModel.Output.ThongTinLoaiHang> DanhSachLoaiHang()
        {
            var dsLoaihang = _iLoaihangService.DocDanhSachLoaiHang();
            return dsLoaihang.Select(lh => new LoaihangModel.Output.ThongTinLoaiHang
            {
                Id = lh.Id,
                Ten = lh.TenLoaiHang
            }).ToList();
        }
        [HttpGet]
        public List<LoaihangModel.Output.ThongTinLoaiHang> DanhSach()
        {
            var dsLh = _iLoaihangService.DocDanhSachLoaiHang();
            return dsLh.Select(lh => new LoaihangModel.Output.ThongTinLoaiHang()
            {
                Id = lh.Id,
                Ten = lh.TenLoaiHang
            }).ToList();
        }
    }
}
