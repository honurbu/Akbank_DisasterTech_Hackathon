using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATOM.Core.Entities
{
    public abstract class BaseCenter
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [Column(TypeName = "decimal(8,6)")]
        public decimal Longitude { get; set; }        //boylam

        [Column(TypeName = "decimal(8,6)")]
        public decimal Latitude { get; set; }         //enlem
        
        public int? CenterTypeId { get; set; }
        public CenterType CenterType { get; set; }



    }
}
