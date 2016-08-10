using Playground.Areas.LukaM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Playground.Areas.LukaM.Controllers
{
    public class Main
    {
        ChessFigure pawn = new ChessFigure("pawn", new PawnMove());
        ChessFigure bishop = new ChessFigure("bishop", new BishopMove());

        public void StartMoving()
        {
            pawn.Move("C4");
            bishop.Move("D2");
        }

    }
}