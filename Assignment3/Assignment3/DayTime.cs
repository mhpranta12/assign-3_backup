using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3
{
    internal class DayTime
    {
        public int Id { get; set; }
        public string DTCode { get; set; }
        public string Day { get; set; }
        public DateTime Start_time { get; set; }
        public DateTime End_time { get; set; }
        public ScheduleDaytime DaytimesSchedule { get; set; }
    }
}
