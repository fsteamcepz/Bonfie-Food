using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BonfieFood
{
    public class Country
    {
        public string Name { get; set; }    // країни
        public string Code { get; set; }    // ISO-код країни
        public List<string> City { get; set; } = new List<string>();  // міста
    }
}