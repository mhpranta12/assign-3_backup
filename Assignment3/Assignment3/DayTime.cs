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
        public int Stime { get; set; }
        public int Etime { get; set; }
        public string Meridiam { get; set; }
        public ScheduleDaytime DaytimesSchedule { get; set; }
    }
}
