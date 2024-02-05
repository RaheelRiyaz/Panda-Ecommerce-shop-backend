using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Domain.Models
{
    public class AppFile : BaseEntity
    {
        public string FilePath { get; set; } = null!;
        public Guid ModuleId { get; set; }
        public Guid EntityId { get; set; }
        public bool IsVideo { get; set; }



        #region Navigational Properties
        [ForeignKey(nameof(ModuleId))]
        public Module Module { get; set; } = null!;
        #endregion Navigational Properties
    }
}
