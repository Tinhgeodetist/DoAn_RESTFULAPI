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
    public class KhuyenmaiController : ControllerBase
    {
        private readonly IKhuyenmaiService _iKhuyenmaiservice;
        public KhuyenmaiController(IKhuyenmaiService ikhuyenmaiservice)
        {
            _iKhuyenmaiservice = ikhuyenmaiservice;
        }
        [HttpGet("DanhSachKhuyenMaiTheoLoaiHang/{input}")]
        public List<KhuyenmaiModel.Output.ThongTinKhuyenMai> DanhSachKhuyenMaiTheoLoaiHang(KhuyenmaiModel.Input.DanhSachKhuyenMaiTheoLoaiHang input)
        {
            var dsKM = _iKhuyenmaiservice.DocDanhSachKhuyenMaiTheoLoaihang(input.LoaihangId)
                .Select(p => new KhuyenmaiModel.Output.ThongTinKhuyenMai()
                {
                    Id = p.Id,
                    NgayBatDau = p.NgayBatDau,
                    NgayKetThuc = p.NgayKetThuc,
                    PhanTramGiam = p.PhanTramGiam,
                    QuaTangKem = p.QuaTangKem,
                    VoucherTangKem = p.VoucherTangKem,

                }).ToList();
            return dsKM;
        }
        [HttpGet("ThongTinKhuyenMai/{input}")]
        public KhuyenmaiModel.Output.ThongTinKhuyenMai DocThongTinKhuyenMai(KhuyenmaiModel.Input.DanhSachKhuyenMaiTheoLoaiHang input)
        {
            var KM = _iKhuyenmaiservice.DocThongTinKhuyenMai(input.LoaihangId);
            if (KM == null) return new KhuyenmaiModel.Output.ThongTinKhuyenMai();
            var thongtinKM = new KhuyenmaiModel.Output.ThongTinKhuyenMai
            {
                Id = KM.Id,
                NgayBatDau = KM.NgayBatDau,
                NgayKetThuc = KM.NgayKetThuc,
                PhanTramGiam = KM.PhanTramGiam,
                QuaTangKem = KM.QuaTangKem,
                VoucherTangKem = KM.VoucherTangKem,

            };
            return thongtinKM;
        }
        // Thêm sửa xóa
    }
}
