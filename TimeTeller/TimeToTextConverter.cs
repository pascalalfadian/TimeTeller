using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeTeller
{
    class TimeToTextConverter
    {
        DateTime now;
        public TimeToTextConverter(DateTime now)
        {
            this.now = now;
        }

        public string getCurrentTime()
        {
            if (now.Minute == 0)
            {
                return "Now is " + now.Hour + " o'clock.";
            }
            else if (now.Minute == 1)
            {
                return "Now is 1 minute past " + now.Hour + ".";
            }
            else
            {
                return "Now is " + now.Minute + " minutes past " + now.Hour + ".";
            }
        }
    }
}
