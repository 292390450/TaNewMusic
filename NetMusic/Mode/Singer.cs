using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMusic.Mode
{
    public class Singer
    {
        public string Id { get; set; }
        public string MId { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public Dictionary<string, string> BasicInfo;
        public Dictionary<string, string> Experience;
        public Singer()
        {
            
        }
    }
}
