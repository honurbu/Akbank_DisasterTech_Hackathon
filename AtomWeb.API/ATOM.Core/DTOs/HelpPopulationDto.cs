using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATOM.Core.DTOs
{
    public class HelpPopulationDto
    {
        [Column(TypeName = "decimal(8,6)")]
        public decimal Longitude { get; set; }        //boylam

        [Column(TypeName = "decimal(8,6)")]
        public decimal Latitude { get; set; }         //enlem
        public string DistrictName { get; set; }
        public int CategoryId { get; set; }
    }
}
