using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Collections;
using checkers_app.Models;

namespace checkers_app.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public AvaloniaList<AvaloniaList<ICell>> Board { get; set; } = new AvaloniaList<AvaloniaList<ICell>>();

        private BoardPoint currentChecker = new BoardPoint(0, 0);
        private List<BoardPoint> checkersToRemove = new List<BoardPoint>();

        public MainWindowViewModel()
        {
            InitGameBoard();
        }

        public void SelectBoardCell(BoardPoint? point)
        {
            if (point is null) return;

            var cell = Board[point.X][point.Y];


            if (cell is BlackCheckerCell or RedCheckerCell)
            {
                PlacePossibleMoves(point, cell);
                currentChecker = point;
            }

            if (cell is BlackQueenCheckerCell or RedQueenCheckerCell)
            {
                PlaceQueenPossibleMoves(point, cell);
                currentChecker = point;
            }

            if (cell is PossibleMoveCell) MakePossibleMove(point);
        }

        private void PlacePossibleMoves(BoardPoint point, ICell cell)
        {
            ClearPossibleMoves();
            checkersToRemove.Clear();

            var newX = point.X;
            ICell enemyChecker = new RedCheckerCell();

            if (cell is RedCheckerCell)
            {
                newX++;
                enemyChecker = new BlackCheckerCell();
            }
            else
            {
                newX--;
                enemyChecker = new RedCheckerCell();
            }

            if (IsInBoardBounds(newX, point.Y - 1))
            {
                if (Board[newX][point.Y - 1] is EmptyCell) Board[newX][point.Y - 1] = new PossibleMoveCell();

                if (Board[newX][point.Y - 1].GetType() == enemyChecker.GetType())
                {
                    var oldX = newX;
                    if (cell is RedCheckerCell) newX++;
                    else newX--;
                    if (IsInBoardBounds(newX, point.Y - 2))
                        if (Board[newX][point.Y - 1] is EmptyCell)
                        {
                            checkersToRemove.Add(new BoardPoint(oldX, point.Y - 1));
                            Board[newX][point.Y - 2] = new PossibleMoveCell();
                        }

                    newX = oldX;
                }
            }

            if (IsInBoardBounds(newX, point.Y + 1))
            {
                if (Board[newX][point.Y + 1] is EmptyCell) Board[newX][point.Y + 1] = new PossibleMoveCell();
                if (Board[newX][point.Y + 1].GetType() == enemyChecker.GetType())
                {
                    var oldX = newX;
                    if (cell is RedCheckerCell) newX++;
                    else newX--;
                    if (IsInBoardBounds(newX, point.Y + 2))
                        if (Board[newX][point.Y + 1] is EmptyCell)
                        {
                            checkersToRemove.Add(new BoardPoint(oldX, point.Y + 1));
                            Board[newX][point.Y + 2] = new PossibleMoveCell();
                        }

                    newX = oldX;
                }
            }
        }

        private void PlaceQueenPossibleMoves(BoardPoint point, ICell cell)
        {
            ClearPossibleMoves();
            checkersToRemove.Clear();

            ICell enemyChecker = new RedCheckerCell();

            if (cell is RedQueenCheckerCell)
            {
                enemyChecker = new BlackCheckerCell();
            }
            else
            {
                enemyChecker = new RedCheckerCell();
            }

            var x = point.X - 1;
            var y = point.Y - 1;

            while (x >= 0 && y >= 0)
            {
                if (Board[x][y].GetType() != enemyChecker.GetType() && Board[x][y] is not EmptyCell) break;
                if (Board[x][y] is EmptyCell) Board[x][y] = new PossibleMoveCell();
                if (Board[x][y].GetType() == enemyChecker.GetType())
                {
                    if (IsInBoardBounds(x - 1, y - 1))
                    {
                        if (Board[x - 1][y - 1] is EmptyCell)
                        {
                            checkersToRemove.Add(new BoardPoint(x, y));
                            Board[x - 1][y - 1] = new PossibleMoveCell();
                        }
                        else break;
                    }
                }

                x--;
                y--;
            }

            x = point.X - 1;
            y = point.Y + 1;
            while (x >= 0 && y <= 7)
            {
                if (Board[x][y].GetType() != enemyChecker.GetType() && Board[x][y] is not EmptyCell) break;
                if (Board[x][y] is EmptyCell) Board[x][y] = new PossibleMoveCell();
                if (Board[x][y].GetType() == enemyChecker.GetType())
                {
                    if (IsInBoardBounds(x - 1, y + 1))
                    {
                        if (Board[x - 1][y + 1] is EmptyCell)
                        {
                            checkersToRemove.Add(new BoardPoint(x, y));
                            Board[x - 1][y + 1] = new PossibleMoveCell();
                        }
                        else break;
                    }
                }

                x--;
                y++;
            }

            x = point.X + 1;
            y = point.Y - 1;
            while (x <= 7 && y >= 0)
            {
                if (Board[x][y].GetType() != enemyChecker.GetType() && Board[x][y] is not EmptyCell) break;
                if (Board[x][y] is EmptyCell) Board[x][y] = new PossibleMoveCell();
                if (Board[x][y].GetType() == enemyChecker.GetType())
                {
                    if (IsInBoardBounds(x + 1, y - 1))
                    {
                        if (Board[x + 1][y - 1] is EmptyCell)
                        {
                            checkersToRemove.Add(new BoardPoint(x, y));
                            Board[x + 1][y - 1] = new PossibleMoveCell();
                        }
                        else break;
                    }
                }

                x++;
                y--;
            }

            x = point.X + 1;
            y = point.Y + 1;
            while (x <= 7 && y <= 7)
            {
                if (Board[x][y].GetType() != enemyChecker.GetType() && Board[x][y] is not EmptyCell) break;
                if (Board[x][y] is EmptyCell) Board[x][y] = new PossibleMoveCell();
                if (Board[x][y].GetType() == enemyChecker.GetType())
                {
                    if (IsInBoardBounds(x + 1, y + 1))
                    {
                        if (Board[x + 1][y + 1] is EmptyCell)
                        {
                            checkersToRemove.Add(new BoardPoint(x, y));
                            Board[x + 1][y + 1] = new PossibleMoveCell();
                        }
                        else break;
                    }
                }

                x++;
                y++;
            }
        }

        private void ClearPossibleMoves()
        {
            for (var i = 0; i < Board.Count; i++)
            {
                for (var j = 0; j < Board.Count; j++)
                {
                    if (Board[i][j] is PossibleMoveCell) Board[i][j] = new EmptyCell();
                }
            }
        }

        private bool IsInBoardBounds(int x, int y)
        {
            return x is >= 0 and <= 7 && y is >= 0 and <= 7;
        }

        private void MakePossibleMove(BoardPoint point)
        {
            ClearPossibleMoves();
            var selectedCell = Board[currentChecker.X][currentChecker.Y];
            Board[currentChecker.X][currentChecker.Y] = new EmptyCell();
            Board[point.X][point.Y] = selectedCell;

            if (Math.Abs(currentChecker.X - point.X) > 1)
            {
                foreach (var checker in checkersToRemove)
                {
                    Board[checker.X][checker.Y] = new EmptyCell();
                }
            }

            if (selectedCell is RedCheckerCell && point.X == 7)
                Board[point.X][point.Y] = new RedQueenCheckerCell();
            if (selectedCell is BlackCheckerCell && point.X == 0)
                Board[point.X][point.Y] = new BlackQueenCheckerCell();
        }

        private void InitGameBoard()
        {
            ICell currentChecker = new RedCheckerCell();
            for (var i = 0; i < 8; i++)
            {
                var boardRow = new AvaloniaList<ICell>();
                if (i == 4) currentChecker = new BlackCheckerCell();
                for (var j = 0; j < 8; j++)
                {
                    if (i is 3 or 4)
                    {
                        boardRow.Add(new EmptyCell());
                        continue;
                    }

                    if (i % 2 == 0)
                    {
                        boardRow.Add(j % 2 == 0 ? new EmptyCell() : currentChecker);
                    }
                    else
                    {
                        boardRow.Add(j % 2 == 0 ? currentChecker : new EmptyCell());
                    }
                }

                Board.Add(boardRow);
            }
        }
    }
}