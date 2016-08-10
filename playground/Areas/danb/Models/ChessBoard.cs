using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace Playground.Areas.Danb.Models
{
    public class ChessBoard
    {
        private ChessPiece[] board;  // entire board, each cell holds 1 ChessPiece (or null)

        public ChessBoard()
        {
            board = new ChessPiece[8 * 8];
        }

        public void SetBoardToStartingPosition()
        {
            board = new ChessPiece[8 * 8];
            ChessPiece piece;
            for(int i=0; i<8; i++)
            {
                piece = ChessPiece.CreatePawn();
                piece.isStartedAtTop = true;
                piece.position = new Point(i, 1);
                setPieceAt(piece.position, piece);

                piece = ChessPiece.CreatePawn();
                piece.isStartedAtTop = false;
                piece.position = new Point(i, 6);
                setPieceAt(piece.position, piece);
            }
        }

        public ChessPiece pieceAt(Point pnt)
        {
            if (isValidPosition(pnt))
            {
                return board[pnt.Y * 8 + pnt.X];
            }
            return null;
        }

        public void setPieceAt(Point pnt, ChessPiece piece)
        {
            if (isValidPosition(pnt))
            {
                piece.position = pnt;
                board[pnt.Y * 8 + pnt.X] = piece;
            }
        }

        public bool isValidPosition(Point pnt)
        {
            return pnt.X >= 0 && pnt.X < 8 && pnt.Y >= 0 && pnt.Y < 8;
        }
    }
}