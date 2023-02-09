using Services.Models;
using Services.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IServices
{
   public interface ILoaihangService: IRepository<Loaihang>
    {
        List<Loaihang> DocDanhSachLoaiHang();
        Loaihang DocThongTinLoaiHang(int id);
    }
}
