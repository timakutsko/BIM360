using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameParsing
{
    struct JsonData
    {
        public string Email { get; set; }
        
        public string Password { get; set; }
        
        public string URL { get; set; }
        
        public string DIR { get; set; }
        
        public double EthrnSensivity { get; set; }
        
        public List<string> Extensions { get; set; }
    }
}
