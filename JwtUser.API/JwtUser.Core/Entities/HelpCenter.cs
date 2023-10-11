using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace JwtUser.Core.Entities
{
    public class HelpCenter : BaseCenter
    {

      

        [JsonIgnore]
        public ICollection<HelpCenterCategories> HelpCenterCategories { get; set; }

    }
}
