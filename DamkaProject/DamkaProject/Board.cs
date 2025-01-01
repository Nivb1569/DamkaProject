using System;
using System.Security.Cryptography;

namespace Ex02
{
    class Board
    {
        private int m_Size;
        private Piece[,] m_GameBoard;

        public Board(int i_Size)
        {
            m_Size = i_Size;
            m_GameBoard = new Piece[m_Size, m_Size];
            initializeBoard(Piece.e_PieceType.X, Piece.e_PieceType.O);
        }
        public Piece[,] GameBoard
        {
            get { return m_GameBoard; }
        }
        public int Size
        {
            get { return m_Size;}
        }
        public static int GetSize()
        {
            bool isValid = false;
            int size = 0; //need to delete
            while (!isValid)
            {
                Console.WriteLine("Enter the board size (6, 8 or 10): ");
                String input = Console.ReadLine();
                if (int.TryParse(input, out size))
                {
                    if (!isValidSize(size))
                    {
                        Console.WriteLine("Invalid input! Only 6, 8 or 10 are valid sizes.");
                    }
                    else
                    {
                        isValid = true;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input! Only 6, 8 or 10 are valid sizes.");
                }
            }

            return size;
        }
        private static bool isValidSize(int i_Size)
        {
            return i_Size == 6 || i_Size == 8 || i_Size == 10;
        }
        private void initializeBoard(Piece.e_PieceType i_FirstPiece, Piece.e_PieceType i_SecondPiece)
        {
            makeEmptyBoard();
            for (int i = m_Size - 1; i > m_Size - (m_Size / 2); i--)
            {
                for (int j = 0; j < m_Size; j++)
                {
                    if ((i + j + 1) % 2 == 0)
                        m_GameBoard[i, j] = new Piece(i_FirstPiece, Piece.e_DirectionType.Up);
                }
            }
            for (int i = 0; i < (m_Size / 2) - 1; i++)
            {
                for (int j = 0; j < m_Size; j++)
                {
                    if ((i + j + 1) % 2 == 0)
                    {
                        m_GameBoard[i, j] = new Piece(i_SecondPiece, Piece.e_DirectionType.Down);
                    }
                }
            }
        }
        private void makeEmptyBoard()
        {
            for (int i = 0; i < m_Size; i++)
            {
                for (int j = 0; j < m_Size; j++)
                {
                    m_GameBoard[i, j] = new Piece(Piece.e_PieceType.Empty, Piece.e_DirectionType.Empty);
                }
            }
        }
        public void PrintBoard()
        {
            char rows = 'A';
            printHeader();

            for (int i = 0; i < m_Size; i++)
            {
                printSep();
                Console.Write(rows++);  

                for (int j = 0; j < m_Size; j++)
                {
                    printCell(m_GameBoard[i, j].PieceType);
                }
                Console.Write("|\n");
            }
            printSep();
        }
        private void printSep()
        {
            for (int i = 0; i < m_Size * 4 + 2; i++)
            {
                Console.Write("=");
            }
            Console.WriteLine();
        }
        private void printHeader()
        {
            char columns = 'a';
            Console.WriteLine();

            for (int i = 0; i < m_Size; i++)
            {
                if (i == 0)
                {
                    Console.Write(" ");
                }
                Console.Write("  " + columns++ + " ");
            }
            Console.WriteLine();
        }
        private void printCell(Piece.e_PieceType i_Cell)
        {
            if (i_Cell == Piece.e_PieceType.Empty)
            {
                Console.Write("|   ");
            }
            else if (i_Cell == Piece.e_PieceType.X)
            {
                Console.Write("| X ");
            }
            else if (i_Cell == Piece.e_PieceType.O)
            {
                Console.Write("| O ");
            }
            else if (i_Cell == Piece.e_PieceType.K)
            {
                Console.Write("| K ");
            }
            else if (i_Cell == Piece.e_PieceType.U)
            {
                Console.Write("| U ");
            }
        }
        public void UpdateKingCase(Piece.e_PieceType i_PlayerPiece)
        {
            if (i_PlayerPiece == Piece.e_PieceType.X)
            {
                for (int i = 0; i < m_Size; i++)
                {
                    if (m_GameBoard[0, i].PieceType == Piece.e_PieceType.X)
                    {
                        m_GameBoard[0, i].PieceType = Piece.e_PieceType.K;
                        m_GameBoard[0, i].DirectionType = Piece.e_DirectionType.Both;
                    }
                }
            }
            else
            {
                for (int i = 0; i < m_Size; i++)
                {
                    if (m_GameBoard[m_Size - 1, i].PieceType == Piece.e_PieceType.O)
                    {
                        m_GameBoard[m_Size - 1, i].PieceType = Piece.e_PieceType.U;
                        m_GameBoard[m_Size - 1, i].DirectionType = Piece.e_DirectionType.Both;
                    }
                }
            }
        }

    }
}
