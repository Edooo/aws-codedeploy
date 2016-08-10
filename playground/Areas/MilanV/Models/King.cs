using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Playground.Areas.MilanV.Models
{
	public class King:MoveBehaviour
	{


		public bool Move(Coordinates currCoordinates, Coordinates moveCoordinates)
		{
			List<Coordinates> moves = new List<Coordinates>() {

				new Coordinates( currCoordinates.X++, currCoordinates.Y++ ),
				new Coordinates( currCoordinates.X++, currCoordinates.Y-- ),
				new Coordinates( currCoordinates.X, currCoordinates.Y-- ),
				new Coordinates( currCoordinates.X--, currCoordinates.Y-- ),
				new Coordinates( currCoordinates.X--, currCoordinates.Y ),
				new Coordinates( currCoordinates.X--, currCoordinates.Y++ ),
				new Coordinates( currCoordinates.X, currCoordinates.Y++ ),
				new Coordinates( currCoordinates.X++, currCoordinates.Y++ ),


			};
			for ( int i = 0; i <= moves.Count; i++ ) {
				if ( moves[i].X > 8 || moves[ i ].X < 0 || moves[ i ].Y > 8 || moves[ i ].Y < 0 )
					moves.RemoveAt( i );
				i--;
			}

			if(moves.Contains( moveCoordinates ))
				return true;
			return false;

		}
	}
}