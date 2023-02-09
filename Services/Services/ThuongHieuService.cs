using Services.IServices;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repository
{
    public class ThuongHieuService:Repository<Thuonghieu>,IThuonghieuService
    {
        public ThuongHieuService(QLShopContext context) : base(context) { }
        public List<Thuonghieu> DocDanhSachThuonghieu()
        {
            var dsThuongHieu = DocDanhSach().ToList();
            return dsThuongHieu;
        }
       
    }
}
