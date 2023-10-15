using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATOM.Core.Entities
{
    public class HelpCenterCategories : BaseEntity
    {
        public int HelpCenterId { get; set; }
        public int CategoryId { get; set; }
    }
}
