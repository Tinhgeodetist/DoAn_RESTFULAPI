﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Services.Models
{
    public partial class Thuonghieu
    {
        public Thuonghieu()
        {
            Sanphams = new HashSet<Sanpham>();
        }

        public int Id { get; set; }
        public string Nuoc { get; set; }
        public string TenThuongHieu { get; set; }

        public virtual ICollection<Sanpham> Sanphams { get; set; }
    }
}
