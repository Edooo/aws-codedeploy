using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Web;

using Antlr.Runtime;

namespace Playground.Areas.MilanV.Models
{
	public class ChessBoard
	{
		public List<ChessFigure> WhiteFigures { get; set; } 
		public List<ChessFigure> BlackFigures { get; set; }

		public void InitializeGame()
		{
			WhiteFigures = new List<ChessFigure>() {
				#region Pawns
				new ChessFigure() {
					Board = this,
					Coordinates = new Coordinates( 1,2 ),
					Image = new Bitmap( "" ),
					MoveBehaviour = new Pawn()
				},
					new ChessFigure() {
					Board = this,
					Coordinates = new Coordinates( 2,2 ),
					Image = new Bitmap( "" ),
					MoveBehaviour = new Pawn()
				},
						new ChessFigure() {
					Board = this,
					Coordinates = new Coordinates( 3,2 ),
					Image = new Bitmap( "" ),
					MoveBehaviour = new Pawn()
				},
							new ChessFigure() {
					Board = this,
					Coordinates = new Coordinates( 4,2 ),
					Image = new Bitmap( "" ),
					MoveBehaviour = new Pawn()
				},
								new ChessFigure() {
					Board = this,
					Coordinates = new Coordinates( 5,2 ),
					Image = new Bitmap( "" ),
					MoveBehaviour = new Pawn()
				},
									new ChessFigure() {
					Board = this,
					Coordinates = new Coordinates( 6,2 ),
					Image = new Bitmap( "" ),
					MoveBehaviour = new Pawn()
				},
										new ChessFigure() {
					Board = this,
					Coordinates = new Coordinates( 7,2 ),
					Image = new Bitmap( "" ),
					MoveBehaviour = new Pawn()
				},
											new ChessFigure() {
					Board = this,
					Coordinates = new Coordinates( 8,2 ),
					Image = new Bitmap( "" ),
					MoveBehaviour = new Pawn()
				}
				#endregion

			};
			BlackFigures = new List<ChessFigure>() {
				#region Pawns
				new ChessFigure() {
					Board = this,
					Coordinates = new Coordinates( 1,7 ),
					Image = new Bitmap( "" ),
					MoveBehaviour = new Pawn()
				},
					new ChessFigure() {
					Board = this,
					Coordinates = new Coordinates( 2,7 ),
					Image = new Bitmap( "" ),
					MoveBehaviour = new Pawn()
				},
						new ChessFigure() {
					Board = this,
					Coordinates = new Coordinates( 3,7 ),
					Image = new Bitmap( "" ),
					MoveBehaviour = new Pawn()
				},
							new ChessFigure() {
					Board = this,
					Coordinates = new Coordinates( 4,7 ),
					Image = new Bitmap( "" ),
					MoveBehaviour = new Pawn()
				},
								new ChessFigure() {
					Board = this,
					Coordinates = new Coordinates( 5,7 ),
					Image = new Bitmap( "" ),
					MoveBehaviour = new Pawn()
				},
									new ChessFigure() {
					Board = this,
					Coordinates = new Coordinates( 6,7 ),
					Image = new Bitmap( "" ),
					MoveBehaviour = new Pawn()
				},
										new ChessFigure() {
					Board = this,
					Coordinates = new Coordinates( 7,7 ),
					Image = new Bitmap( "" ),
					MoveBehaviour = new Pawn()
				},
											new ChessFigure() {
					Board = this,
					Coordinates = new Coordinates( 8,2 ),
					Image = new Bitmap( "" ),
					MoveBehaviour = new Pawn()
				}
				#endregion

			};
		}

	}
}