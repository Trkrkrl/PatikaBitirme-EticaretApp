using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class ProductDetailDto:IDto
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public string ProductName { get; set; }
        public short UnitsInStock { get; set; }
        public decimal UnitPrice { get; set; }
        public string Status { get; set; } = "Sıfır";//kullanılmış, sıfır, az kullanılmış gibi
        public bool IsOfferable { get; set; }
        public bool IsSold { get; set; }
        //--
        public List<string> ImagePath { get; set; }
        public string Description { get; set; }
        public int ColorId { get; set; }
        public string CategoryName { get; set; }
        public string ColorName { get; set; }
    }
}
