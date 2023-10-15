using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATOM.Core.DTOs.Response
{
    public class NearGatheringCenterDto
    {
        public string Name { get; set; }
        public float Longitude { get; set; }        //boylam
        public float Latitude { get; set; }         //enlem

        public float Distance { get; set; }
        public CategoryDto Category { get; set; }
    }
}
