﻿using Services.IServices;
using Services.Models;
using Services.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class LoaihangService : Repository<Loaihang>,ILoaihangService
    {
        public LoaihangService(QLShopContext context) : base(context) { }

        public  List<Loaihang> DocDanhSachLoaiHang()
        {
            var dsLoaihang = DocDanhSach().ToList();
            return dsLoaihang;
        }
        public Loaihang DocThongTinLoaiHang(int id)
        {
            var loaihang = DocTheoDieuKien(x=>x.Id.Equals(id)).FirstOrDefault();
            return loaihang;
        }
        public new bool Xoa(Loaihang entity)
        {
            try
            {
                var loaihangId = entity.Id;
                var SP = _context.Sanphams
                                   .FirstOrDefault(x => x.LoaiHangId.Equals(loaihangId));
                if (SP != null) return false;
                base.Xoa(entity);
                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
