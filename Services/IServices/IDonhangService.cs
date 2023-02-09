using Services.Models;
using Services.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IServices
{
    public interface IDonhangService:IRepository<Donhang>
    {
        List<Donhang> DocDanhSachDonHangTheoUser(int userId);
        List<Donhang> DocDanhSachDonHangDangBan();
        Donhang DocThongTinDonHang(int id);
        bool MuaDonHang(List<Donhang> dsDonhang);
    }
}
