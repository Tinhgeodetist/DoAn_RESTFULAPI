﻿using DoAn_CSC_API.DTO;
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
    public class ThuonghieuController : ControllerBase
    {
        private readonly IThuonghieuService _ithuonghieuService;
        public ThuonghieuController(IThuonghieuService ithuonghieuService)
        {
            _ithuonghieuService = ithuonghieuService;
        }
        [HttpGet("DanhSachThuongHieu")]
        public List<ThuongHieuModel.Output.ThongTinThuongHieu> DanhSachThuongHieu()
        {
            var dsThuonghieu= _ithuonghieuService.DocDanhSachThuonghieu();
            return dsThuonghieu.Select(th => new ThuongHieuModel.Output.ThongTinThuongHieu
            {
                Id  = th.Id,
                TenThuongHieu = th.TenThuongHieu,
                Nuoc = th.Nuoc

            }).ToList();
        }
        [HttpGet]
        public List<ThuongHieuModel.Output.ThongTinThuongHieu> DanhSach()
        {
            var dsLh = _ithuonghieuService.DocDanhSachThuonghieu();
            return dsLh.Select(th => new ThuongHieuModel.Output.ThongTinThuongHieu()
            {
                Id = th.Id,
                TenThuongHieu = th.TenThuongHieu,
                Nuoc = th.Nuoc
            }).ToList();
        }
    }
}
