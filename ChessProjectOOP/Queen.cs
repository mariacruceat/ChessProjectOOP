﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessProjectOOP
{
    class Queen : Piece, IDisposable
    {
        public Queen(OwnerTypes owner, PiecePosition position) : base(owner,position)
        {
            Type = PieceTypes.Queen;
            name = "Queen";

            if (owner == OwnerTypes.Black)
                Picture = new Bitmap(Properties.Resources.BlackQueen);
            else
                Picture = new Bitmap(Properties.Resources.WhiteQueen);

        }
        public override void Dispose()
        {
            Picture.Dispose();
        }

        public override List<PiecePosition> GetPossibileMoves(ChessTableSquare[,] table)
        {
            List<PiecePosition> moves = new List<PiecePosition>();

            for (int i = Position.Row + 1; i < table.GetLength(1); i++)
            {
                try
                {
                    var newPos = new PiecePosition(Position.Column, i);
                    ValidateMove(newPos, table, 1);
                    moves.Add(newPos);
                }
                catch { }
            }

            for (int i = Position.Row - 1; i > 0; i--)
            {
                try
                {
                    var newPos = new PiecePosition(Position.Column, i);
                    ValidateMove(newPos, table, 2);
                    moves.Add(newPos);
                }
                catch { }
            }

            for (int i = (int)Position.Column + 1; i < table.GetLength(0); i++)
            {
                try
                {
                    var newPos = new PiecePosition((EColumn)i, Position.Row);
                    ValidateMove(newPos, table, 3);
                    moves.Add(newPos);
                }
                catch { }
            }

            for (int i = (int)Position.Column - 1; i > 0; i--)
            {
                try
                {
                    var newPos = new PiecePosition((EColumn)i, Position.Row);
                    ValidateMove(newPos, table, 4);
                    moves.Add(newPos);
                }
                catch { }
            }

            for (int i = (int)Position.Column + 1, j = (int)Position.Row + 1; i < 8 & j < 8; i++, j++)
            {
                try
                {
                    var newPos = new PiecePosition(i, j);
                    ValidateMove(newPos, table, 5);
                    moves.Add(newPos);
                }
                catch { }
            }

            for (int i = (int)Position.Column + 1, j = (int)Position.Row + 1; i < 8 & j < 8; i--, j--)
            {
                try
                {
                    var newPos = new PiecePosition(i, j);
                    ValidateMove(newPos, table, 6);
                    moves.Add(newPos);
                }
                catch { }
            }

            for (int i = (int)Position.Column + 1, j = (int)Position.Row + 1; i < 8 & j < 8; i++, j--)
            {
                try
                {
                    var newPos = new PiecePosition(i, j);
                    ValidateMove(newPos, table, 7);
                    moves.Add(newPos);
                }
                catch { }
            }

            for (int i = (int)Position.Column + 1, j = (int)Position.Row + 1; i < 8 & j < 8; i--, j++)
            {
                try
                {
                    var newPos = new PiecePosition(i, j);
                    ValidateMove(newPos, table, 8);
                    moves.Add(newPos);
                }
                catch { }
            }
            return moves;
        }

        public override void ValidateMove(PiecePosition newPosition, ChessTableSquare[,] table, int direction = 0)
        {
            base.ValidateMove(newPosition, table, direction);


            if (direction == 0 && Position.Row != newPosition.Row && Position.Column != newPosition.Column)
                throw new IllegalMoveException(this, String.Format("Can not move from {0} to {1}", Position.ToString(), newPosition.ToString()));

            //Colision rules
            if ((direction == 0 || direction == 1) && newPosition.Row > Position.Row)
            {
                for (int i = Position.Row + 1; i < newPosition.Row; i++) //up-down
                    if (!table[(int)Position.Column - 1, i - 1].IsEmpty)
                        throw new IllegalMoveException(this, String.Format("Can not move from {0} to {1}", Position.ToString(), newPosition.ToString()));
            }
            if ((direction == 0 || direction == 2) && newPosition.Row < Position.Row)
            {
                for (int i = Position.Row - 1; i > newPosition.Row; i--) //up-down
                    if (!table[(int)Position.Column - 1, i - 1].IsEmpty)
                        throw new IllegalMoveException(this, String.Format("Can not move from {0} to {1}", Position.ToString(), newPosition.ToString()));
            }

            if ((direction == 0 || direction == 3) && (int)newPosition.Column > (int)Position.Column)
            {
                for (int i = (int)Position.Column + 1; i < (int)newPosition.Column; i++)
                    if (!table[i - 1, Position.Row - 1].IsEmpty)
                        throw new IllegalMoveException(this, String.Format("Can not move from {0} to {1}", Position.ToString(), newPosition.ToString()));
            }
            if ((direction == 0 || direction == 4) && (int)newPosition.Column < (int)Position.Column)
            {
                for (int i = (int)Position.Column - 1; i > (int)newPosition.Column; i--)
                    if (!table[i - 1, Position.Row - 1].IsEmpty)
                        throw new IllegalMoveException(this, String.Format("Can not move from {0} to {1}", Position.ToString(), newPosition.ToString()));
            }

            if ((direction == 0 || direction == 5) && (int)newPosition.Column > (int)Position.Column && (int)newPosition.Row > (int)Position.Row)
            {
                for (int i = (int)Position.Column + 1, j = (int)Position.Row + 1; i < 8, j < 8; i++)
                    if (!table[i - 1, Position.Row - 1].IsEmpty)
                        throw new IllegalMoveException(this, String.Format("Can not move from {0} to {1}", Position.ToString(), newPosition.ToString()));
            }
        }

        public override void Move(PiecePosition newPosition, ChessTableSquare[,] table)
        {
            if (Math.Abs(newPosition.Column - Position.Column) == Math.Abs(newPosition.Row - Position.Row))
            {
                Position = newPosition;
                return;
            }
            if (newPosition.Row == Position.Row && newPosition.Column != Position.Column)
            {
                Position = newPosition;
                return;
            }

            if (newPosition.Column == Position.Column && newPosition.Row != Position.Row)
            {
                Position = newPosition;
                return;
            }

            throw new IllegalMoveException(this, String.Format("Can not move from {0} to {1}", Position.ToString(), newPosition.ToString()));
        }
    }
}
