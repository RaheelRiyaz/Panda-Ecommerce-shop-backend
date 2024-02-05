using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Application.RRModels
{
    public class AppFileResponse
    {
        public Guid Id { get; set; }
        public bool IsVideo { get; set; }
        public string FilePath { get; set; } = null!;
    }

}
