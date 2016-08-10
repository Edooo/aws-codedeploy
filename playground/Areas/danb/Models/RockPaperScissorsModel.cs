using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace playground.Areas.danb.Models
{
	public enum RockPaperScissors
	{
		Rock,
		Paper,
		Scissors
	}
	public class RockPaperScissorsModel
	{
		public int RPS(RockPaperScissors p1, RockPaperScissors p2)
		{
			switch ( p1 ) {
			case RockPaperScissors.Rock:
				switch ( p2 ) {
				case RockPaperScissors.Rock:		return 0;			// Rock v Rock
				case RockPaperScissors.Paper:		return 2;			// Rock v Paper
				case RockPaperScissors.Scissors:	return 1;			// Rock v Scissors
				}
				break;
			case RockPaperScissors.Paper:
				switch ( p2 ) {
				case RockPaperScissors.Rock:		return 1;			// Paper v Rock
				case RockPaperScissors.Paper:		return 0;			// Paper v Paper
				case RockPaperScissors.Scissors:	return 2;			// Paper v Scissors
				}
				break;
			case RockPaperScissors.Scissors:
				switch ( p2 ) {
				case RockPaperScissors.Rock:		return 2;			// Scissors v Rock
				case RockPaperScissors.Paper:		return 1;			// Scissors v Paper
				case RockPaperScissors.Scissors:	return 0;			// Scissors v Scissors
				}
				break;
			}
			return -1; // FAIL
		}
	}
}

