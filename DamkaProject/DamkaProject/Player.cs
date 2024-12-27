using System;

namespace DamkaProject
{
    class Player
    {
        private String m_PlayerName = null;
        private Piece.m_PieceType m_PlayerPiece;
        private int m_NumberOfPieces = 0;
        private int m_Points = 0;
        private bool m_IsComputer;

        public Player(String i_PlayerName, Piece.m_PieceType i_PlayerPiece, bool i_IsComputer)
        {
            PlayerName = i_PlayerName;
            m_PlayerPiece = i_PlayerPiece;
            m_IsComputer = i_IsComputer;
        }
        public String PlayerName
        {
            get { return m_PlayerName; }
            set { m_PlayerName = value; }
        }
        public Piece.m_PieceType PlayerPiece
        {
            get { return m_PlayerPiece; }
        }
        public static String GetName()
        {
            bool isValid = false;
            String name = null;
            while (!isValid)
            {
                Console.WriteLine("Enter your name: ");
                name = Console.ReadLine();
                if (!isValidName(name))
                {
                    Console.WriteLine("Invalid input! Only letters (no spaces or ant other characters) and must be up to 20 characters");
                }
                else
                {
                    isValid = true;
                }
            }

            return name;
        }
        private static bool isValidName(String i_Name)
        {
            bool isValid = true;
            if (i_Name.Length > 20)
            {
                isValid = false;
            }
            else
            {
                if (!allCharectersIsLetters(i_Name))
                {
                    isValid = false;
                }
            }

            return isValid;
        }
        public void MakeMove(Board i_Board, out bool o_IsJumpMove)
        {
            Point from, to;
            getMoveChoice(out from, out to, i_Board);
            executeMove(from, to, i_Board, out o_IsJumpMove);
        }
        private static bool allCharectersIsLetters(String i_Name)
        {
            bool result = true;
            for (int i = 0; i < i_Name.Length; i++)
            {
                if (!Char.IsLetter(i_Name[i]))
                {
                    result = false;
                    break;
                }
            }

            return result;
        }
        private void getMoveChoice(out Point o_From, out Point o_To, Board i_Board)
        {
            bool isValid = false;
            String choice = null;
            o_To = null;
            o_From = null;
            while (!isValid)
            {
                choice = Console.ReadLine();
                if (!isValidChoice(choice, i_Board))
                {
                    Console.WriteLine("Invalid step! Please try again.");
                }
                else
                {
                    isValid = true;
                    converteLettersToPoint(choice.Split('>'), ref o_From, ref o_To);
                }
            }

        }
        private bool isValidChoice(String i_Choice, Board i_Board)
        {
            bool isValid = false;
            if (timesCharAtString(i_Choice, '>') == 1)
            {
                String[] moves = i_Choice.Split('>');
                if (isValidMove(moves, i_Board))
                {
                    isValid = true;
                }
            }

            return isValid;
        }
        private int timesCharAtString(String i_String, Char i_Char)
        {
            int res = 0;
            for (int i = 0; i < i_String.Length; i++)
            {
                if (i_Char == i_String[i])
                {
                    res++;
                }
            }

            return res;
        }
        private bool isValidMove(String[] i_Move, Board i_Board)
        {
            bool isValid = false;
            Point from = null, to = null;
            if (i_Move.Length == 2)
            {
                if (validLetters(i_Move[0][0], i_Move[0][1], i_Board.Size) && validLetters(i_Move[1][0], i_Move[1][1], i_Board.Size))
                {
                    converteLettersToPoint(i_Move, ref from, ref to);
                    if (isLegalMove(from, to, i_Board))
                    {
                        isValid = true;
                    }
                }
            }

            return isValid;
        }
        private bool validLetters(Char i_FirstLetter, Char i_SecondLetter, int i_BoardSize)
        {
            return i_FirstLetter >= 'A' && i_FirstLetter <= 'A' + i_BoardSize - 1 && i_SecondLetter >= 'a' && i_SecondLetter <= 'a' + i_BoardSize - 1;
        }
        private void converteLettersToPoint(String[] i_Move, ref Point i_From, ref Point i_To) // needs io_...
        {
            i_From = new Point((i_Move[0][0] - 'A'), (i_Move[0][1] - 'a'));
            i_To = new Point((i_Move[1][0] - 'A'), (i_Move[1][1] - 'a'));
        }
        private bool isLegalMove(Point i_From, Point i_To, Board i_Board)
        {
            bool isValid = false;
            if ((i_Board.GameBoard[i_From.X, i_From.Y].PieceType == m_PlayerPiece) && (isMoveDiagonally(i_From, i_To, i_Board) || isJump(i_From, i_To, i_Board)))
            {
                isValid = true;
            }

            return isValid;
        }
        private bool isMoveDiagonally(Point i_From, Point i_To, Board i_Board)
        {
            bool isDiagonally = false;
            Piece.m_DirectionType direction = i_Board.GameBoard[i_From.X, i_From.Y].DirectionType;

            if (i_Board.GameBoard[i_To.X, i_To.Y].PieceType == Piece.m_PieceType.Empty)
            {
                if (direction == Piece.m_DirectionType.Up)
                {
                    if ((i_From.X - 1 == i_To.X && i_From.Y + 1 == i_To.Y) || (i_From.X - 1 == i_To.X && i_From.Y - 1 == i_To.Y))
                    {
                        isDiagonally = true;
                    }
                }
                else if (direction == Piece.m_DirectionType.Down)
                {
                    if ((i_From.X + 1 == i_To.X && i_From.Y - 1 == i_To.Y) || (i_From.X + 1 == i_To.X && i_From.Y + 1 == i_To.Y))
                    {
                        isDiagonally = true;
                    }
                }
                else if (direction == Piece.m_DirectionType.Both)
                {
                    if ((i_From.X - 1 == i_To.X && i_From.Y - 1 == i_To.Y) || (i_From.X + 1 == i_To.X && i_From.Y + 1 == i_To.Y) || (i_From.X + 1 == i_To.X && i_From.Y - 1 == i_To.Y) || (i_From.X - 1 == i_To.X && i_From.Y + 1 == i_To.Y))
                    {
                        isDiagonally = true;
                    }
                }
            }

            return isDiagonally;
        }

        private bool isJump(Point i_From, Point i_To, Board i_Board)
        {
            bool makeJump = false;
            Piece.m_DirectionType direction = i_Board.GameBoard[i_From.X, i_From.Y].DirectionType;
            if (i_Board.GameBoard[i_To.X, i_To.Y].PieceType == Piece.m_PieceType.Empty)
            {
                if (direction == Piece.m_DirectionType.Up)
                {
                    if ((i_From.X - 2 == i_To.X && i_From.Y + 2 == i_To.Y && 
                        (i_Board.GameBoard[i_From.X - 1, i_From.Y + 1].PieceType == Piece.m_PieceType.O ||
                        i_Board.GameBoard[i_From.X - 1, i_From.Y + 1].PieceType == Piece.m_PieceType.U))
                        || 
                       (i_From.X - 2 == i_To.X && i_From.Y - 2 == i_To.Y && 
                        (i_Board.GameBoard[i_From.X - 1, i_From.Y - 1].PieceType == Piece.m_PieceType.O ||
                        i_Board.GameBoard[i_From.X - 1, i_From.Y - 1].PieceType == Piece.m_PieceType.U)))
                    {
                        makeJump = true;
                    }
                }
                else if (direction == Piece.m_DirectionType.Down)
                {
                    if ((i_From.X + 2 == i_To.X && i_From.Y - 2 == i_To.Y &&
                        (i_Board.GameBoard[i_From.X + 1, i_From.Y - 1].PieceType == Piece.m_PieceType.X ||
                         i_Board.GameBoard[i_From.X + 1, i_From.Y - 1].PieceType == Piece.m_PieceType.K))
                         ||
                        (i_From.X + 2 == i_To.X && i_From.Y + 2 == i_To.Y &&
                         (i_Board.GameBoard[i_From.X + 1, i_From.Y + 1].PieceType == Piece.m_PieceType.X ||
                         i_Board.GameBoard[i_From.X + 1, i_From.Y + 1].PieceType == Piece.m_PieceType.K)))
                    {
                        makeJump = true;
                    }
                }
                else if (direction == Piece.m_DirectionType.Both && m_PlayerPiece == Piece.m_PieceType.O)
                {
                    if ((i_From.X - 2 == i_To.X && i_From.Y - 2 == i_To.Y &&
                        (i_Board.GameBoard[i_From.X - 1, i_From.Y - 1].PieceType == Piece.m_PieceType.X ||
                         i_Board.GameBoard[i_From.X - 1, i_From.Y - 1].PieceType == Piece.m_PieceType.K))
                        ||
                        (i_From.X + 2 == i_To.X && i_From.Y + 2 == i_To.Y &&
                        (i_Board.GameBoard[i_From.X + 1, i_From.Y + 1].PieceType == Piece.m_PieceType.X ||
                         i_Board.GameBoard[i_From.X + 1, i_From.Y + 1].PieceType == Piece.m_PieceType.K)) ||
                        (i_From.X + 2 == i_To.X && i_From.Y - 2 == i_To.Y && 
                        (i_Board.GameBoard[i_From.X + 1, i_From.Y - 1].PieceType == Piece.m_PieceType.X ||
                         i_Board.GameBoard[i_From.X + 1, i_From.Y - 1].PieceType == Piece.m_PieceType.K)) || 
                        (i_From.X - 2 == i_To.X && i_From.Y + 2 == i_To.Y &&
                        (i_Board.GameBoard[i_From.X - 1, i_From.Y - 1].PieceType == Piece.m_PieceType.X ||
                         i_Board.GameBoard[i_From.X - 1, i_From.Y - 1].PieceType == Piece.m_PieceType.K)))
                    {
                        makeJump = true;
                    }
                }
                else if (direction == Piece.m_DirectionType.Both && m_PlayerPiece == Piece.m_PieceType.X)
                {
                    if ((i_From.X - 2 == i_To.X && i_From.Y - 2 == i_To.Y &&
                        (i_Board.GameBoard[i_From.X - 1, i_From.Y - 1].PieceType == Piece.m_PieceType.O ||
                         i_Board.GameBoard[i_From.X - 1, i_From.Y - 1].PieceType == Piece.m_PieceType.K))
                        ||
                        (i_From.X + 2 == i_To.X && i_From.Y + 2 == i_To.Y &&
                        (i_Board.GameBoard[i_From.X + 1, i_From.Y + 1].PieceType == Piece.m_PieceType.O ||
                         i_Board.GameBoard[i_From.X + 1, i_From.Y + 1].PieceType == Piece.m_PieceType.K)) ||
                        (i_From.X + 2 == i_To.X && i_From.Y - 2 == i_To.Y &&
                        (i_Board.GameBoard[i_From.X + 1, i_From.Y - 1].PieceType == Piece.m_PieceType.O ||
                         i_Board.GameBoard[i_From.X + 1, i_From.Y - 1].PieceType == Piece.m_PieceType.K)) ||
                        (i_From.X - 2 == i_To.X && i_From.Y + 2 == i_To.Y &&
                        (i_Board.GameBoard[i_From.X - 1, i_From.Y - 1].PieceType == Piece.m_PieceType.O ||
                         i_Board.GameBoard[i_From.X - 1, i_From.Y - 1].PieceType == Piece.m_PieceType.K)))
                    {
                        makeJump = true;
                    }
                }
            }

            return makeJump;
        }
        private void executeMove(Point i_From, Point i_To, Board i_Board, out bool o_IsJumpMove)
        {
            if (!isJump(i_From, i_To, i_Board))
            {
                i_Board.GameBoard[i_To.X, i_To.Y].PieceType = i_Board.GameBoard[i_From.X, i_From.Y].PieceType;
                i_Board.GameBoard[i_To.X, i_To.Y].DirectionType = i_Board.GameBoard[i_From.X, i_From.Y].DirectionType;
                i_Board.GameBoard[i_From.X, i_From.Y].PieceType = Piece.m_PieceType.Empty;
                i_Board.GameBoard[i_From.X, i_From.Y].DirectionType = Piece.m_DirectionType.Empty;
                o_IsJumpMove = false;
            }
            else
            {
                if (fromDownToUpRight(i_From, i_To))
                {
                    i_Board.GameBoard[i_From.X - 1, i_From.Y + 1].PieceType = Piece.m_PieceType.Empty;
                    i_Board.GameBoard[i_From.X - 1, i_From.Y + 1].DirectionType = Piece.m_DirectionType.Empty;
                }
                else if (fromDownToUpLeft(i_From, i_To))
                {
                    i_Board.GameBoard[i_From.X - 1, i_From.Y - 1].PieceType = Piece.m_PieceType.Empty;
                    i_Board.GameBoard[i_From.X - 1, i_From.Y - 1].DirectionType = Piece.m_DirectionType.Empty;

                }
                else if (fromUpToDownLeft(i_From, i_To))
                {
                    i_Board.GameBoard[i_From.X + 1, i_From.Y - 1].PieceType = Piece.m_PieceType.Empty;
                    i_Board.GameBoard[i_From.X + 1, i_From.Y - 1].DirectionType = Piece.m_DirectionType.Empty;
                }
                else
                {
                    i_Board.GameBoard[i_From.X + 1, i_From.Y + 1].PieceType = Piece.m_PieceType.Empty;
                    i_Board.GameBoard[i_From.X + 1, i_From.Y + 1].DirectionType = Piece.m_DirectionType.Empty;

                }
                i_Board.GameBoard[i_To.X, i_To.Y].PieceType = i_Board.GameBoard[i_From.X, i_From.Y].PieceType;
                i_Board.GameBoard[i_To.X, i_To.Y].DirectionType = i_Board.GameBoard[i_From.X, i_From.Y].DirectionType;
                i_Board.GameBoard[i_From.X, i_From.Y].PieceType = Piece.m_PieceType.Empty;
                i_Board.GameBoard[i_From.X, i_From.Y].DirectionType = Piece.m_DirectionType.Empty;
                o_IsJumpMove = true;
            }
        }
        private bool fromDownToUpLeft(Point i_From, Point i_To)
        {
            return i_From.X - 2 == i_To.X && i_From.Y - 2 == i_To.Y;
        }
        private bool fromDownToUpRight(Point i_From, Point i_To)
        {
            return i_From.X - 2 == i_To.X && i_From.Y + 2 == i_To.Y;
        }
        private bool fromUpToDownLeft(Point i_From, Point i_To)
        {
            return i_From.X + 2 == i_To.X && i_From.Y - 2 == i_To.Y;
        }
        public int NumberOfPieces
        {
            get { return m_NumberOfPieces; }
            set { m_NumberOfPieces = value; }
        }
    }
}
