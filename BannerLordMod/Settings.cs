using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterTime
{
    public class Settings
    {
        public float fast_forward_speed { get; set; }
        public float extra_fast_forward_speed { get; set; }
        public float ctrl_space_speed { get; set; }

        public Settings()
        {
            fast_forward_speed = 4f;
            extra_fast_forward_speed = 8f;
            ctrl_space_speed = 12f;
        }
    }
}
