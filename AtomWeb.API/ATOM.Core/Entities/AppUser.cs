using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATOM.Core.Entities
{
    public class AppUser : IdentityUser
    {
        [Column(TypeName = "decimal(8,6)")]
        public decimal? Longitude { get; set; }        //boylam

        [Column(TypeName = "decimal(8,6)")]
        public decimal? Latitude { get; set; }         //enlem
    }
}
