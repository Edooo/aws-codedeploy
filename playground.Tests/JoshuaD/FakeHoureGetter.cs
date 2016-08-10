using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Playground.Areas.JoshuaD.Models;

namespace Playground.Tests.JoshuaD
{
    class FakeHoureGetter
        : iHourGetter
    {
        /// <summary>
        /// Hour property
        /// </summary>
        public int HourValue
        {
            get;
            set;
        }        
        /// <summary>
        /// Get the hour of the DateTime Value.
        /// </summary>
        /// <returns></returns>
        public int GetHour()
        {
            return HourValue;
        }
    }
}
