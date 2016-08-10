using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playground.Areas.LukaM.Models
{
    public interface IMoveBehavior
    {
        bool Move(string field);
    }

}
