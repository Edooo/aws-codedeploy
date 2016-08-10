using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Playground.Areas.LukaM.Models
{
    public class ChessFigure
    {
        private string Name;
        private IMoveBehavior MoveBehavior;

        public ChessFigure(string name, IMoveBehavior moveBehavior)
        {
            Name = name;
            MoveBehavior = moveBehavior;
        }

        public bool Move(string field)
        {
            return MoveBehavior.Move(field);
        }

    }
}
