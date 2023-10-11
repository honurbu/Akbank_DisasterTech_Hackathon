using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace JwtUser.Core.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }

        [JsonIgnore]
        public ICollection<HelpDemand> HelpDemands { get; set; }



        [JsonIgnore]
        public ICollection<HelpCenterCategories> HelpCenterCategories { get; set; }


    }
}
