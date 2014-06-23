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
                return now.Hour + " o'clock.";
            }
            else if (now.Minute == 1)
            {
                return "1 minute past " + now.Hour + ".";
            }
            else
            {
                return now.Minute + " minutes past " + now.Hour + ".";
            }
        }

        public string getCurrentDate()
        {
            return now.ToLongDateString() + ".";
        }
    }
}
