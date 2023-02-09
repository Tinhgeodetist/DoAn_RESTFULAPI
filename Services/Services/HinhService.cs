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
    public class HinhService:Repository<Hinh>, IHinhService
    {
        public HinhService(QLShopContext context) : base(context) { }

        public List<Hinh> DocDanhSachHinh(bool quantri = true)
        {
            throw new NotImplementedException();
        }

        //public List<Hinh> DocDanhSachHinh(bool quantri = true)
        //{
        //    List<Hinh> Hinhs;
        //    if (quantri)
        //        Hinhs = DocDanhSach().ToList();
        //    else
        //        //Hinhs = DocTheoDieuKien().ToList();
        //    return Hinhs;                

        //}
        public Hinh DocThongTinHinh(int Hinhid)
        {
            var Hinhs = DocTheoDieuKien(x => x.HinhId.Equals(Hinhid)).FirstOrDefault();
            return Hinhs;
        }
    }
}
