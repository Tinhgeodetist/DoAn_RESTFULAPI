using System;
using System.Collections.Generic;

#nullable disable

namespace Services.Models
{
    public partial class Chitietdonhang
    {
        public int Id { get; set; }
        public int DonHangId { get; set; }
        public int SanPhamId { get; set; }
        public int UserId { get; set; }
        public string SoDienThoai { get; set; }
        public short TinhTrangDonHang { get; set; }
        public string Diachi { get; set; }
        public double PhiShip { get; set; }
        public int KhuyenMaiId { get; set; }
        public string TenNguoiNhan { get; set; }
        public double TongTien { get; set; }

        public virtual Donhang DonHang { get; set; }
        public virtual Sanpham SanPham { get; set; }
    }
}
