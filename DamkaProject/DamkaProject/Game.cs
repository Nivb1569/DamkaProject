﻿using System;

namespace DamkaProject
{
    class Game
    {
        private Board m_Board;
        private Player m_FirstPlayer = null;
        private Player m_SecondPlayer = null;
        private const int k_HumenPlayer = 0;
        private const int k_ComputerPlayer = 1;

        public void run()
        {
            m_FirstPlayer = new Player(Player.GetName(), Sign.m_SignType.X, false);
            Board board = new Board(Board.GetSize());
            GetSecondPlayer();
            board.PrintBoard();
        }
        private static int GameWithPlayerOrComputer()
        {
            bool isValid = false;
            int choice = -1;
            while (!isValid)
            {
                Console.WriteLine("For playing against another player enter 0.");
                Console.WriteLine("For playing against the computer enter 1.");
                String input = Console.ReadLine();
                if (int.TryParse(input, out choice))
                {
                    if (!isValidChoice(choice))
                    {
                        Console.WriteLine("Invalid input! Only 0 or 1 are valid choices.");
                    }
                    else
                    {
                        isValid = true;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input! Only 0 or 1 are valid choices.");
                }
            }

            return choice;
        }
        private static bool isValidChoice(int i_Choice)
        {
            return i_Choice == 0 || i_Choice == 1;
        }
        private void GetSecondPlayer()
        {
            int choice = GameWithPlayerOrComputer();
            if (choice == k_HumenPlayer)
            {
                m_SecondPlayer = new Player(Player.GetName(), Sign.m_SignType.O, false);
            }
            else
            {
                m_SecondPlayer = new Player("Computer", Sign.m_SignType.O, true);
            }
        }
    }
}
