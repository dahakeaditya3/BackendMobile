using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sale.Models.DTOs
{
    public class ProductUpdateDto
    {
        public int ProductId { get; set; }
        public bool IsActive { get; set; }
    }
}
