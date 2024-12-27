using System;
using System.Security.Cryptography;

namespace DamkaProject
{
    class Board
    {
        private int m_Size;
        private Sign.m_SignType[,] m_GameBoard;

        public Board(int i_Size)
        {
            m_Size = i_Size;
            m_GameBoard = new Sign.m_SignType[m_Size, m_Size];
            InitializeBoard(Sign.m_SignType.X, Sign.m_SignType.O);
        }
        public Sign.m_SignType[,] GameBoard
        {
            get { return m_GameBoard; }
        }
        public static int GetSize()
        {
            bool isValid = false;
            int size = 0;
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
        private void InitializeBoard(Sign.m_SignType i_FirstSign, Sign.m_SignType i_SecondSign)
        {
            makeEmptyBoard();
            for (int i = m_Size - 1; i > m_Size - (m_Size / 2); i--)
            {
                for (int j = 0; j < m_Size; j++)
                {
                    if ((i + j + 1) % 2 == 0)
                        m_GameBoard[i,j] = i_FirstSign;
                }
            }
            for (int i = 0; i < (m_Size / 2) - 1; i++)
            {
                for (int j = 0; j < m_Size; j++)
                {
                    if ((i + j + 1) % 2 == 0)
                    {
                        m_GameBoard[i, j] = i_SecondSign;
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
                    m_GameBoard[i, j] = Sign.m_SignType.Empty;
                }
            }
        }
        public void PrintBoard()
        {
            char rows = 'a';
            printHeader();

            for (int i = 0; i < m_Size; i++)
            {
                printSep();
                Console.Write(rows++);  

                for (int j = 0; j < m_Size; j++)
                {
                    printCell(m_GameBoard[i, j]);
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
            char columns = 'A';
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
        private void printCell(Sign.m_SignType i_Cell)
        {
            if (i_Cell == Sign.m_SignType.Empty)
            {
                Console.Write("|   ");
            }
            else if (i_Cell == Sign.m_SignType.X)
            {
                Console.Write("| X ");
            }
            else if (i_Cell == Sign.m_SignType.O)
            {
                Console.Write("| O ");
            }
            else if (i_Cell == Sign.m_SignType.K)
            {
                Console.Write("| K ");
            }
            else if (i_Cell == Sign.m_SignType.Q)
            {
                Console.Write("| Q ");
            }
        }

    }
}
