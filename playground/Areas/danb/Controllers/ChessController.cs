using Playground.Areas.Danb.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;

namespace Playground.Areas.Danb.Controllers
{
    public class ChessController : Controller
    {
        // GET: Danb/Chess
        public ActionResult Index()
        {
            RunTests();
            return View();
        }

        private void RunTests()
        {
            ChessBoard brd = new ChessBoard();
            ChessPiece p = ChessPiece.CreatePawn();
            p.isStartedAtTop = true;
            brd.setPieceAt(new Point(3, 3), p);
            if (p.checkIfCanMoveTo(new Point(3, 3), brd)) Debug.WriteLine("FAILED");
            if (p.checkIfCanMoveTo(new Point(3, 2), brd)) Debug.WriteLine("FAILED");
            if (p.checkIfCanMoveTo(new Point(2, 3), brd)) Debug.WriteLine("FAILED");
            if (p.checkIfCanMoveTo(new Point(4, 3), brd)) Debug.WriteLine("FAILED");
            if (p.checkIfCanMoveTo(new Point(2, 4), brd)) Debug.WriteLine("FAILED");
            if (p.checkIfCanMoveTo(new Point(4, 4), brd)) Debug.WriteLine("FAILED");

            if (!p.checkIfCanMoveTo(new Point(3, 4), brd)) Debug.WriteLine("FAILED");

            ChessPiece p2 = ChessPiece.CreatePawn();
            p2.isStartedAtTop = true;
            brd.setPieceAt(new Point(2, 4), p2);
            if (p.checkIfCanMoveTo(new Point(2, 4), brd)) Debug.WriteLine("FAILED");
            p2.isStartedAtTop = false;
            if (!p.checkIfCanMoveTo(new Point(2, 4), brd)) Debug.WriteLine("FAILED");
        }
    }
}