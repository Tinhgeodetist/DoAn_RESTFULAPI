using System;
using System.Collections.Generic;

#nullable disable

namespace Services.Models
{
    public partial class Donhang
    {
        public Donhang()
        {
            Chitietdonhangs = new HashSet<Chitietdonhang>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public short TinhTrangDonHang { get; set; }
        public double GiamGia { get; set; }
        public double PhiShip { get; set; }
        public double TongTien { get; set; }
        public string MaGiamGia { get; set; }
        public string Ten { get; set; }
        public string SoDienThoai { get; set; }
        public string Email { get; set; }
        public string DiaChi { get; set; }
        public DateTime NgayTao { get; set; }
        public DateTime NgayCapNhat { get; set; }
        public string NoiDung { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Chitietdonhang> Chitietdonhangs { get; set; }
    }
}
