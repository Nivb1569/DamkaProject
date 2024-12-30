using System;
using System.Collections.Generic;


namespace DamkaProject
{
    class Player
    {
        private String m_PlayerName = null;
        private Piece.e_PieceType m_PlayerPiece;
        private int m_NumberOfPieces = 0;
        private int m_Points = 0;
        private bool m_IsComputer;

        public Player(String i_PlayerName, Piece.e_PieceType i_PlayerPiece, bool i_IsComputer)
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
        public Piece.e_PieceType PlayerPiece
        {
            get { return m_PlayerPiece; }
        }
        public static String GetName(String i_FirstName = null)
        {
            bool isValid = false;
            String name = null;
            while (!isValid)
            {
                Console.WriteLine("Enter your name: ");
                name = Console.ReadLine();
                if (!isValidName(name))
                {
                    Console.WriteLine("Invalid input! Only letters (no spaces or any other characters) and must be up to 20 characters");
                }
                else if (isTheSameNameAsTheOtherPlayer(name, i_FirstName))
                {
                    Console.WriteLine("Invalid input! You must Choose a diffrent.");
                }
                else
                {
                    isValid = true;
                }
            }

            return name;
        }
        private static bool isTheSameNameAsTheOtherPlayer(String i_Name, String i_FirstName)
        {
            return i_Name == i_FirstName || i_Name == "Computer";
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
        public void MakeMove(Board i_Board, out bool o_IsJumpMove, out bool o_IsQuitInput)
        {
            Point from, to;

            if (m_IsComputer)
            {
                o_IsQuitInput = false;
                generateMove(out from, out to, i_Board);
            }
            else
            {
                getMoveChoice(out from, out to, i_Board, out o_IsQuitInput);
            }

            if (!o_IsQuitInput)
            {
                executeMove(from, to, i_Board, out o_IsJumpMove);
            }
            else
            {
                o_IsJumpMove = false;
            }
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
        private void getMoveChoice(out Point o_From, out Point o_To, Board i_Board, out bool o_IsQuitInput)
        {
            bool isValid = false;
            String choice;
            o_To = null;
            o_From = null;
            o_IsQuitInput = false;
            while (!isValid)
            {
                choice = Console.ReadLine();
                if (choice == "Q")
                {
                    o_IsQuitInput = true;
                    return;
                }
                else
                {
                    o_IsQuitInput = false;
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
            if ((theMoveIsFromThePlayerSquare(i_Board.GameBoard[i_From.X, i_From.Y].PieceType)) && (isMoveDiagonally(i_From, i_To, i_Board) || isJump(i_From, i_To, i_Board)))
            {
                isValid = true;
            }

            return isValid;
        }
        private bool theMoveIsFromThePlayerSquare(Piece.e_PieceType i_PeiceType)
        {
            bool result = i_PeiceType == m_PlayerPiece;
            if (result == false)
            {
                if (m_PlayerPiece == Piece.e_PieceType.O && i_PeiceType == Piece.e_PieceType.U
                    || m_PlayerPiece == Piece.e_PieceType.X && i_PeiceType == Piece.e_PieceType.K)
                {
                    result = true;
                }
            }
            return result;
        }
        private bool isMoveDiagonally(Point i_From, Point i_To, Board i_Board)
        {
            bool isDiagonally = false;
            Piece.e_DirectionType direction = i_Board.GameBoard[i_From.X, i_From.Y].DirectionType;

            if (i_Board.GameBoard[i_To.X, i_To.Y].PieceType == Piece.e_PieceType.Empty)
            {
                if (direction == Piece.e_DirectionType.Up)
                {
                    if ((i_From.X - 1 == i_To.X && i_From.Y + 1 == i_To.Y) || (i_From.X - 1 == i_To.X && i_From.Y - 1 == i_To.Y))
                    {
                        isDiagonally = true;
                    }
                }
                else if (direction == Piece.e_DirectionType.Down)
                {
                    if ((i_From.X + 1 == i_To.X && i_From.Y - 1 == i_To.Y) || (i_From.X + 1 == i_To.X && i_From.Y + 1 == i_To.Y))
                    {
                        isDiagonally = true;
                    }
                }
                else if (direction == Piece.e_DirectionType.Both)
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
            Piece.e_DirectionType direction = i_Board.GameBoard[i_From.X, i_From.Y].DirectionType;
            if (i_Board.GameBoard[i_To.X, i_To.Y].PieceType == Piece.e_PieceType.Empty)
            {
                if (direction == Piece.e_DirectionType.Up)
                {
                    if ((i_From.X - 2 == i_To.X && i_From.Y + 2 == i_To.Y &&
                        (i_Board.GameBoard[i_From.X - 1, i_From.Y + 1].PieceType == Piece.e_PieceType.O ||
                        i_Board.GameBoard[i_From.X - 1, i_From.Y + 1].PieceType == Piece.e_PieceType.U))
                        ||
                       (i_From.X - 2 == i_To.X && i_From.Y - 2 == i_To.Y &&
                        (i_Board.GameBoard[i_From.X - 1, i_From.Y - 1].PieceType == Piece.e_PieceType.O ||
                        i_Board.GameBoard[i_From.X - 1, i_From.Y - 1].PieceType == Piece.e_PieceType.U)))
                    {
                        makeJump = true;
                    }
                }
                else if (direction == Piece.e_DirectionType.Down)
                {
                    if ((i_From.X + 2 == i_To.X && i_From.Y - 2 == i_To.Y &&
                        (i_Board.GameBoard[i_From.X + 1, i_From.Y - 1].PieceType == Piece.e_PieceType.X ||
                         i_Board.GameBoard[i_From.X + 1, i_From.Y - 1].PieceType == Piece.e_PieceType.K))
                         ||
                        (i_From.X + 2 == i_To.X && i_From.Y + 2 == i_To.Y &&
                         (i_Board.GameBoard[i_From.X + 1, i_From.Y + 1].PieceType == Piece.e_PieceType.X ||
                         i_Board.GameBoard[i_From.X + 1, i_From.Y + 1].PieceType == Piece.e_PieceType.K)))
                    {
                        makeJump = true;
                    }
                }
                else if (direction == Piece.e_DirectionType.Both && m_PlayerPiece == Piece.e_PieceType.O)
                {
                    if ((i_From.X - 2 == i_To.X && i_From.Y - 2 == i_To.Y &&
                        (i_Board.GameBoard[i_From.X - 1, i_From.Y - 1].PieceType == Piece.e_PieceType.X ||
                         i_Board.GameBoard[i_From.X - 1, i_From.Y - 1].PieceType == Piece.e_PieceType.K))
                        ||
                        (i_From.X + 2 == i_To.X && i_From.Y + 2 == i_To.Y &&
                        (i_Board.GameBoard[i_From.X + 1, i_From.Y + 1].PieceType == Piece.e_PieceType.X ||
                         i_Board.GameBoard[i_From.X + 1, i_From.Y + 1].PieceType == Piece.e_PieceType.K)) ||
                        (i_From.X + 2 == i_To.X && i_From.Y - 2 == i_To.Y &&
                        (i_Board.GameBoard[i_From.X + 1, i_From.Y - 1].PieceType == Piece.e_PieceType.X ||
                         i_Board.GameBoard[i_From.X + 1, i_From.Y - 1].PieceType == Piece.e_PieceType.K)) ||
                        (i_From.X - 2 == i_To.X && i_From.Y + 2 == i_To.Y &&
                        (i_Board.GameBoard[i_From.X - 1, i_From.Y + 1].PieceType == Piece.e_PieceType.X ||
                         i_Board.GameBoard[i_From.X - 1, i_From.Y + 1].PieceType == Piece.e_PieceType.K)))
                    {
                        makeJump = true;
                    }
                }
                else if (direction == Piece.e_DirectionType.Both && m_PlayerPiece == Piece.e_PieceType.X)
                {
                    if ((i_From.X - 2 == i_To.X && i_From.Y - 2 == i_To.Y &&
                        (i_Board.GameBoard[i_From.X - 1, i_From.Y - 1].PieceType == Piece.e_PieceType.O ||
                         i_Board.GameBoard[i_From.X - 1, i_From.Y - 1].PieceType == Piece.e_PieceType.U))
                        ||
                        (i_From.X + 2 == i_To.X && i_From.Y + 2 == i_To.Y &&
                        (i_Board.GameBoard[i_From.X + 1, i_From.Y + 1].PieceType == Piece.e_PieceType.O ||
                         i_Board.GameBoard[i_From.X + 1, i_From.Y + 1].PieceType == Piece.e_PieceType.U)) ||
                        (i_From.X + 2 == i_To.X && i_From.Y - 2 == i_To.Y &&
                        (i_Board.GameBoard[i_From.X + 1, i_From.Y - 1].PieceType == Piece.e_PieceType.O ||
                         i_Board.GameBoard[i_From.X + 1, i_From.Y - 1].PieceType == Piece.e_PieceType.U)) ||
                        (i_From.X - 2 == i_To.X && i_From.Y + 2 == i_To.Y &&
                        (i_Board.GameBoard[i_From.X - 1, i_From.Y + 1].PieceType == Piece.e_PieceType.O ||
                         i_Board.GameBoard[i_From.X - 1, i_From.Y + 1].PieceType == Piece.e_PieceType.U)))
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
                i_Board.GameBoard[i_From.X, i_From.Y].PieceType = Piece.e_PieceType.Empty;
                i_Board.GameBoard[i_From.X, i_From.Y].DirectionType = Piece.e_DirectionType.Empty;
                o_IsJumpMove = false;
            }
            else
            {
                if (fromDownToUpRight(i_From, i_To))
                {
                    i_Board.GameBoard[i_From.X - 1, i_From.Y + 1].PieceType = Piece.e_PieceType.Empty;
                    i_Board.GameBoard[i_From.X - 1, i_From.Y + 1].DirectionType = Piece.e_DirectionType.Empty;
                }
                else if (fromDownToUpLeft(i_From, i_To))
                {
                    i_Board.GameBoard[i_From.X - 1, i_From.Y - 1].PieceType = Piece.e_PieceType.Empty;
                    i_Board.GameBoard[i_From.X - 1, i_From.Y - 1].DirectionType = Piece.e_DirectionType.Empty;

                }
                else if (fromUpToDownLeft(i_From, i_To))
                {
                    i_Board.GameBoard[i_From.X + 1, i_From.Y - 1].PieceType = Piece.e_PieceType.Empty;
                    i_Board.GameBoard[i_From.X + 1, i_From.Y - 1].DirectionType = Piece.e_DirectionType.Empty;
                }
                else
                {
                    i_Board.GameBoard[i_From.X + 1, i_From.Y + 1].PieceType = Piece.e_PieceType.Empty;
                    i_Board.GameBoard[i_From.X + 1, i_From.Y + 1].DirectionType = Piece.e_DirectionType.Empty;

                }
                i_Board.GameBoard[i_To.X, i_To.Y].PieceType = i_Board.GameBoard[i_From.X, i_From.Y].PieceType;
                i_Board.GameBoard[i_To.X, i_To.Y].DirectionType = i_Board.GameBoard[i_From.X, i_From.Y].DirectionType;
                i_Board.GameBoard[i_From.X, i_From.Y].PieceType = Piece.e_PieceType.Empty;
                i_Board.GameBoard[i_From.X, i_From.Y].DirectionType = Piece.e_DirectionType.Empty;
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

        private void generateMove(out Point o_From, out Point o_To, Board i_Board)
        {
            o_From = null;
            o_To = null;

            List<Point[]> possiblePositions = new List<Point[]>();
            possiblePositions = createPossiblePositionsList(i_Board);
            choseNextMove(possiblePositions, out o_From, out o_To);
        }

        private List<Point[]> createPossiblePositionsList(Board i_Board)
        {

            List<Point[]> possiblePositions = new List<Point[]>();

            for (int i = 0; i < i_Board.Size; i++)
            {
                for (int j = 0; j < i_Board.Size; j++)
                {
                    Point currentFrom = new Point(i, j);
                    if (i_Board.GameBoard[i, j].PieceType == Piece.e_PieceType.O || i_Board.GameBoard[i, j].PieceType == Piece.e_PieceType.U)
                    {
                        checkAndAddPossiblePositions(i_Board, currentFrom, possiblePositions, i_Board.GameBoard[i,j].PieceType);
                    }
                }
            }

            return possiblePositions;
        }
        private void checkAndAddPossiblePositions(Board i_Board, Point i_CurrentFrom, List<Point[]> io_PossiblePositions, Piece.e_PieceType i_PieceType)
        {
            Point currentTo = new Point(i_CurrentFrom.X, i_CurrentFrom.Y);

                currentTo.X -= 1;
                currentTo.Y += 1;
                checkLegalAndAddToList(i_Board, i_CurrentFrom, currentTo, io_PossiblePositions);
                currentTo.X += 1;
                currentTo.Y += 1;
                checkLegalAndAddToList(i_Board, i_CurrentFrom, currentTo, io_PossiblePositions);
                currentTo.X -= 2;
                currentTo.Y += 2;
                checkLegalAndAddToList(i_Board, i_CurrentFrom, currentTo, io_PossiblePositions);
                currentTo.X += 2;
                currentTo.Y += 2;
                checkLegalAndAddToList(i_Board, i_CurrentFrom, currentTo, io_PossiblePositions);

            if (i_PieceType == Piece.e_PieceType.U)
            {
                currentTo.X -= 1;
                currentTo.Y -= 1;
                checkLegalAndAddToList(i_Board, i_CurrentFrom, currentTo, io_PossiblePositions);
                currentTo.X += 1;
                currentTo.Y -= 1;
                checkLegalAndAddToList(i_Board, i_CurrentFrom, currentTo, io_PossiblePositions);
                currentTo.X -= 2;
                currentTo.Y -= 2;
                checkLegalAndAddToList(i_Board, i_CurrentFrom, currentTo, io_PossiblePositions);
                currentTo.X += 2;
                currentTo.Y -= 2;
                checkLegalAndAddToList(i_Board, i_CurrentFrom, currentTo, io_PossiblePositions);
            }

        }
        private void checkLegalAndAddToList(Board i_Board, Point i_CurrentFrom, Point i_CurrentTo, List<Point[]> io_PossiblePositions)
        {
            if (isLegalMove(i_CurrentFrom, i_CurrentTo, i_Board))
            {
                io_PossiblePositions.Add(new Point[] { i_CurrentFrom, i_CurrentTo });
            }
        }
        private void choseNextMove(List<Point[]> possoblePositions, out Point o_From, out Point o_To)
        {
            o_From = null;
            o_To = null;
        }

        public bool NoMovesLeft(Board i_Board)
        {
            bool isTie = true;
            for (int i = 0; i < i_Board.Size - 1; i++)
            {
                for (int j = 0; j < i_Board.Size - 1; j++)
                {
                    if ((isPlayerPiece(i_Board.GameBoard[i, j].PieceType)) && !isStuckPiece(new Point(i, j), i_Board))
                    {
                        isTie = false;
                        break;
                    }
                }

                if (!isTie)
                {
                    break;
                }
            }

            return isTie;
        }
        private bool isPlayerPiece(Piece.e_PieceType i_TypePeice)
        {
            return (i_TypePeice == m_PlayerPiece) ||
                ((m_PlayerPiece == Piece.e_PieceType.O && i_TypePeice == Piece.e_PieceType.U) ||
                (m_PlayerPiece == Piece.e_PieceType.X && i_TypePeice == Piece.e_PieceType.K));
        }
        private bool isStuckPiece(Point i_From, Board i_Board)
        {
            bool isStuckPiece = true;
            if (isMoveDiagonallyWithValidation(i_From, new Point(i_From.X - 1, i_From.Y - 1), i_Board) ||
                isMoveDiagonallyWithValidation(i_From, new Point(i_From.X - 1, i_From.Y + 1), i_Board) ||
                isMoveDiagonallyWithValidation(i_From, new Point(i_From.X + 1, i_From.Y - 1), i_Board) ||
                isMoveDiagonallyWithValidation(i_From, new Point(i_From.X + 1, i_From.Y + 1), i_Board) ||
                isJumpWithValidation(i_From, new Point(i_From.X - 2, i_From.Y - 2), i_Board)           ||
                isJumpWithValidation(i_From, new Point(i_From.X - 2, i_From.Y + 2), i_Board)           ||
                isJumpWithValidation(i_From, new Point(i_From.X + 2, i_From.Y - 2), i_Board)           ||
                isJumpWithValidation(i_From, new Point(i_From.X + 2, i_From.Y + 2), i_Board))
            {
                isStuckPiece = false;
            }

            return isStuckPiece;
        }
        private bool isLegalPoint(Point i_From, Point i_To, Board i_Board)
        {
            return i_To.X >= 0 && i_To.X < i_Board.Size && i_To.Y >= 0 && i_To.Y < i_Board.Size;
        }
        private bool isMoveDiagonallyWithValidation(Point i_From, Point i_To, Board i_Board)
        {
            bool result = false;
            if (isLegalPoint(i_From, i_To, i_Board))
            {
                result = isMoveDiagonally(i_From, i_To, i_Board);
            }

            return result;
        }
        private bool isJumpWithValidation(Point i_From, Point i_To, Board i_Board)
        {
            bool result = false;
            if (isLegalPoint(i_From, i_To, i_Board))
            {
                result = isJump(i_From, i_To, i_Board);
            }

            return result;
        }
    }

}
