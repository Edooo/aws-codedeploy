using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playground.Areas.MilanV.Models
{
	public interface MoveBehaviour
	{
		bool Move( Coordinates coordinates, Coordinates moveCoordinates);
	}
}
