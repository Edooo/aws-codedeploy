using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace Playground.Areas.Danb.Models
{
    public class ChessPiece
    {
        public Point position;      // (x,y) position on the board
        public bool isStartedAtTop; // true means this piece start at the top of the board, and moving down
        // public pieceImage
        private Func<ChessPiece, ChessBoard, List<Point>> movesStrategy;

        public ChessPiece(Func<ChessPiece, ChessBoard, List<Point>> getMovesStrategy)
        {
            movesStrategy = getMovesStrategy;
        }

        public bool checkIfCanMoveTo(Point newPosition, ChessBoard board)
        {
            List<Point> okMoves = movesStrategy(this, board);
            if (okMoves.Contains(newPosition))
            {
                return true;
            }
            return false;
        }

        // // // // // // // // // // // // // // // // // // // // // // 
        // // // // // // // // // // // // // // // // // // // // // // 
        // //
        // //        CHESS PIECES FACTORY METHODS
        // //

        public static ChessPiece CreatePawn()
        {
            return new ChessPiece(strategyPawn);
        }
        public static ChessPiece CreateKnight()
        {
            return new ChessPiece(strategyKnight);
        }
        public static ChessPiece CreateRook()
        {
            return new ChessPiece(strategyRook);
        }
        public static ChessPiece CreateBishop()
        {
            return new ChessPiece(strategyBishop);
        }
        public static ChessPiece CreateQueen()
        {
            return new ChessPiece(strategyQueen);
        }


        // // // // // // // // // // // // // // // // // // // // // // 
        // // // // // // // // // // // // // // // // // // // // // // 
        // //
        // //        CHESS PIECE POSSIBLE MOVES -- STRATEGIES
        // //

        static List<Point> strategyPawn(ChessPiece piece, ChessBoard board)
        {
            List<Point> okMoves = new List<Point>();
            int forward = (piece.isStartedAtTop ? 1 : -1);
            int pawnStartingRow = (piece.isStartedAtTop ? 1 : 6);

            // CHECK: moving 1 space foward
            Point p = new Point(piece.position.X, piece.position.Y + forward);
            if (board.pieceAt(p) == null)
            {
                okMoves.Add(p);
                // ALSO CHECK: moving 2 spaces forward
                if (piece.position.Y == pawnStartingRow)
                {
                    p = new Point(piece.position.X, piece.position.Y + forward * 2);
                    if (board.pieceAt(p) == null)
                    {
                        okMoves.Add(p);
                    }
                }
            }
            // CHECK: taking piece forward/next-to
            p = new Point(piece.position.X-1, piece.position.Y + forward);
            if (board.isValidPosition(p))
            {
                ChessPiece pieceTake = board.pieceAt(p);
                if (pieceTake != null && pieceTake.isStartedAtTop != piece.isStartedAtTop)
                {
                    okMoves.Add(p);
                }
            }
            p = new Point(piece.position.X + 1, piece.position.Y + forward);
            if (board.isValidPosition(p))
            {
                ChessPiece pieceTake = board.pieceAt(p);
                if (pieceTake != null && pieceTake.isStartedAtTop != piece.isStartedAtTop)
                {
                    okMoves.Add(p);
                }
            }

            return okMoves;
        }

        static List<Point> strategyKnight(ChessPiece piece, ChessBoard board)
        {
            List<Point> okMoves = new List<Point>();
            Point[] deltas = {
                new Point(-1,-2),new Point(1,-2),
                new Point(-1,2),new Point(1,2),
                new Point(-2,-1),new Point(-2,1),
                new Point(2,-1),new Point(2,1)};
            foreach(Point delta in deltas)
            {
                tryOneMove(piece, board, okMoves, delta.X, delta.Y);
            }
            return okMoves;
        }

        static List<Point> strategyKing(ChessPiece piece, ChessBoard board)
        {
            List<Point> okMoves = new List<Point>();
            for(int dx=-1; dx<=1; dx++)
            {
                for (int dy = -1; dy <= 1; dy++)
                {
                    tryOneMove(piece, board, okMoves, dx, dy);
                }
            }
            return okMoves;
        }

        static List<Point> strategyRook(ChessPiece piece, ChessBoard board)
        {
            List<Point> okMoves = new List<Point>();
            for(int dx=1; ; dx++)
            {
                if (!tryStraightLineMove(piece, board, okMoves, dx, 0)) break;
            }
            for (int dx=-1; ; dx--)
            {
                if (!tryStraightLineMove(piece, board, okMoves, dx, 0)) break;
            }
            for (int dy = 1; ; dy++)
            {
                if (!tryStraightLineMove(piece, board, okMoves, 0, dy)) break;
            }
            for (int dy = -1; ; dy--)
            {
                if (!tryStraightLineMove(piece, board, okMoves, 0, dy)) break;
            }
            return okMoves;
        }
        static List<Point> strategyBishop(ChessPiece piece, ChessBoard board)
        {
            List<Point> okMoves = new List<Point>();
            for (int dx = 1; ; dx++)
            {
                if (!tryStraightLineMove(piece, board, okMoves, dx, dx)) break;
            }
            for (int dx = 1; ; dx++)
            {
                if (!tryStraightLineMove(piece, board, okMoves, -dx, -dx)) break;
            }
            for (int dx = 1; ; dx++)
            {
                if (!tryStraightLineMove(piece, board, okMoves, dx, -dx)) break;
            }
            for (int dx = 1; ; dx++)
            {
                if (!tryStraightLineMove(piece, board, okMoves, -dx, dx)) break;
            }
            return okMoves;
        }
        static List<Point> strategyQueen(ChessPiece piece, ChessBoard board)
        {
            List<Point> okMoves = strategyRook(piece, board);
            List<Point> okMovesBishop = strategyBishop(piece, board);
            okMoves.AddRange(okMovesBishop);
            return okMoves;
        }


        private static bool tryStraightLineMove(ChessPiece piece, ChessBoard board, List<Point> okMoves, int dx, int dy)
        {
            Point newPosition = new Point(piece.position.X + dx, piece.position.Y + dy);
            if (!board.isValidPosition(newPosition))
            {
                return false; // done
            }
            ChessPiece pieceTake = board.pieceAt(newPosition);
            if (pieceTake == null || (pieceTake.isStartedAtTop != piece.isStartedAtTop))
            {
                okMoves.Add(newPosition);
                if (pieceTake != null) return false; // done
            }
            return true; // continue
        }

        private static bool tryOneMove(ChessPiece piece, ChessBoard board, List<Point> okMoves, int dx, int dy)
        {
            Point newPosition = new Point(piece.position.X + dx, piece.position.Y + dy);
            if (board.isValidPosition(newPosition))
            {
                ChessPiece pieceTake = board.pieceAt(newPosition);
                if (pieceTake == null || (pieceTake.isStartedAtTop != piece.isStartedAtTop))
                {
                    okMoves.Add(newPosition);
                    return true;
                }
            }
            return false;
        }

    }
}