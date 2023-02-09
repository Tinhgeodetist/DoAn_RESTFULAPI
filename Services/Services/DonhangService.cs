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
    public class DonhangService:Repository<Donhang>, IDonhangService
    {

        public DonhangService(QLShopContext context ):base(context) { }
        public List<Donhang> DocDanhSachDonHangTheoUser(int userId)
        {
            var dsDonHang = DocTheoDieuKien(x => x.UserId.Equals(userId),x=>x.User).ToList();
            return dsDonHang;
        }
        public List<Donhang> DocDanhSachDonHangDangBan()
        {
            return DocTheoDieuKien(x => x.NgayTao.Equals(DateTime.Now), x => x.User).ToList();

        }

        public Donhang DocThongTinDonHang(int id)
        {
            return DocTheoDieuKien(x => x.Id.Equals(id), x => x.User).FirstOrDefault();
        }

        public bool MuaDonHang(List<Donhang> dsDonhang)
        {
            throw new NotImplementedException();
        }
    }
}
