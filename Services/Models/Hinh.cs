using System;
using System.Collections.Generic;

#nullable disable

namespace Services.Models
{
    public partial class Hinh
    {
        public int HinhId { get; set; }
        public string Thumbnails { get; set; }
        public string Carousel { get; set; }

        public virtual Sanpham HinhNavigation { get; set; }
    }
}
