using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class BrandModelDto
    {
        public IEnumerable<string> BrandNames { get; set; }
        public IEnumerable<string> ModelNames { get; set; }
    }
}
