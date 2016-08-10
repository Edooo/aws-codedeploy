using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Playground.Areas.JoshuaD.Models
{
    /// <summary>
    /// Get the DateTime method.
    /// </summary>
    public class CurrentDateTime
        : iHourGetter
    {
        /// <summary>
        /// Get the Current DateTime hour.
        /// </summary>
        /// <returns>The current dateTime hour.</returns>
        public int GetHour()
        {
            return DateTime.Now.Hour;
        }
    }
}