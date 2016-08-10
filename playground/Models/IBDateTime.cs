using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Playground.Models
{
    public class IBDateTime
    {
        public static DateTime? ForcedNow = null;
        public static DateTime Now
        {
            get
            {
                return ForcedNow ?? DateTime.Now;
            }
        }
    }
}
