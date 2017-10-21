using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMusic.Mode
{
    public enum MusicianType
    {
        [Description("华语男")]
        cn_man,
        [Description("华语女")]
        cn_woman,
        [Description("华语组合")]
        cn_team,
        [Description("全部")]
        all_all

    }
}
