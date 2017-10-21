using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMusic.Mode
{
    public class Song
    {
        public string Name { get; set; }
        /// <summary>
        /// 活跃度
        /// </summary>
        public long Alert { get; set; }

        public string Time { get; set; }
        public string Id { get; set; }
        public string MId { get; set; }
        public string PubTime { get; set; }
        public Singer Singer { get; set; }
        public Album Album { get; set; }

        public Song()
        {
            
        }
    }
}
