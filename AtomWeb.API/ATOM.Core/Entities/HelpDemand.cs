using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATOM.Core.Entities
{
    public class HelpDemand : AbstractDemand   //Talep
    {

        public int CategoryId { get; set; }

        public int? GatheringCenterId { get; set; }
        public int? HelpCenterId { get; set; }
    }
}
