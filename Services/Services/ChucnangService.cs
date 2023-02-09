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
    public class ChucnangService : Repository<Chucnang>,IChucnangService
    {
        public ChucnangService(QLShopContext context) : base(context) { }
        public Chucnang DocThongTinChucNang(int chucnangId)
        {
            var chucnang = DocTheoDieuKien(x => x.SanPhamId.Equals(chucnangId)).FirstOrDefault();
            return chucnang;
        }
    }
}
