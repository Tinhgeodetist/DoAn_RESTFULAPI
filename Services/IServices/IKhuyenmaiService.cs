

using Services.Models;
using Services.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IServices
{
    public interface IKhuyenmaiService : IRepository<Khuyenmai>
    {
        List<Khuyenmai> DocDanhSachKhuyenMaiDangCo(DateTime ngaydienra);
        Khuyenmai DocThongTinKhuyenMai(int Id);
        List<Khuyenmai> DocDanhSachKhuyenMaiTheoLoaihang(int loaihangId);
    }
}
