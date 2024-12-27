using System;
using System.Management.Instrumentation;

namespace DamkaProject
{
    class Player
    {
        private String m_PlayerName = null;
        private Sign.m_SignType m_PlayerSign;
        private int m_Points = 0;
        private bool m_IsComputer;

        public Player(String i_PlayerName, Sign.m_SignType i_PlayerSign, bool i_IsComputer)
        {
            PlayerName = i_PlayerName;
            m_PlayerSign = i_PlayerSign;
            m_IsComputer = i_IsComputer;
        }
        public String PlayerName
        {
            get { return m_PlayerName;  }
            set { m_PlayerName = value; }
        }
        public Sign.m_SignType PlayerSign
        {
            get { return m_PlayerSign; }
        }
        public static String GetName()
        {
            bool isValid = false;
            String name = null;
            while (!isValid)
            {
                Console.WriteLine("Enter your name: ");
                name = Console.ReadLine();
                if(!isValidName(name))
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
        private void makeMove(Board i_Board)
        {
            Point from, to;
            getMoveChoice(out from, out to, i_Board);
            //executeMove();
        }
        private static bool allCharectersIsLetters(String i_Name)
        {
            bool result = true;
            for (int i = 0; i < i_Name.Length; i++)
            {
                if (!Char.IsLetter(i_Name[i]))
                {
                    result =  false;
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
                    converteLettersToPoint(choice.Split('>'), o_From, o_To);
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
            for(int i = 0; i < i_String.Length; i++)
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
                    converteLettersToPoint(i_Move, from, to);
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
            return i_FirstLetter >= 'A' && i_FirstLetter <= 'A' + i_BoardSize - 1 && i_FirstLetter >= 'a' && i_FirstLetter <= 'a' + i_BoardSize - 1;
        }
        private void converteLettersToPoint(String[] i_Move, Point o_From, Point o_To)
        {
            o_From = new Point((i_Move[0][0] - 'A'), (i_Move[0][1] - 'a'));
            o_To  = new Point((i_Move[1][0] - 'A'), (i_Move[1][1] - 'a'));
        }
        private bool isLegalMove(Point i_From, Point i_To, Board i_Board)
        {
            bool isValid = false;
            if (i_Board.GameBoard[i_From.X, i_From.Y].SignType == m_PlayerSign && isMoveDiagonally(i_From, i_To))
            {
                if (i_Board.GameBoard[i_From.X, i_From.Y].SignType == Sign.m_SignType.Empty || isJump())
                {
                    isValid = true;
                }
            }

            return isValid;
        }
        private bool isMoveDiagonally(Point i_From, Point i_To)
        {

        }
    }
}
