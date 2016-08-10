using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playground.Areas.JoshuaD.Models
{
    /// <summary>
    /// Interface for accessing the current hour.
    /// </summary>
    public interface iHourGetter
    {        
        /// <summary>
        /// Get the Hour of the Day.
        /// </summary>
        /// <returns>Hour of the Day.</returns>
        int GetHour();
    }
}
