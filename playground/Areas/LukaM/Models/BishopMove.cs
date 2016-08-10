using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace Playground.Areas.LukaM.Models
{
    public class BishopMove : IMoveBehavior
    {
        public bool Move(string field)
        {
            Trace.WriteLine("pawn moved to" + field);
            return true;
        }
    }
}