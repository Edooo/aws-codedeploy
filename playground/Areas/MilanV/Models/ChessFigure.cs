using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;

namespace Playground.Areas.MilanV.Models
{
	public class Coordinates
	{

		public int X
		{
			get;
			set;
		} = 0;
		public int Y
		{
			get;
			set;
		} = 0;
		public Coordinates(int x, int y)
		{
			this.X =  x;
			this.Y = y;
		}
	}
	public class ChessFigure
	{

		public Image Image { get; set; }

		public Coordinates Coordinates { get; set; }

		public MoveBehaviour MoveBehaviour { get; set; }

		public ChessBoard Board { get; set; }


	}
}