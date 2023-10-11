using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtUser.Core.Entities
{
    public abstract class BaseCenter
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Longitude { get; set; }        //boylam
        public float Latitude { get; set; }         //enlem
    }
}
