using System;

namespace DamkaProject
{
    class Game
    {
        private Board m_Board;
        private Player m_FirstPlayer = null;
        private Player m_SecondPlayer = null;
        private Player m_CurrentPlayerTurn = null;
        private const int k_HumenPlayer = 0;
        private const int k_ComputerPlayer = 1;

        public void Run()
        {
            m_FirstPlayer = new Player(Player.GetName(), Piece.e_PieceType.X, false);
            m_CurrentPlayerTurn = m_FirstPlayer;
            m_Board = new Board(Board.GetSize());
            getSecondPlayer();
            setTheNumberOfPicesToThePlayers();
            startToPlay();
        }
        private static int gameWithPlayerOrComputer()
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
        private void getSecondPlayer()
        {
            int choice = gameWithPlayerOrComputer();
            if (choice == k_HumenPlayer)
            {
                m_SecondPlayer = new Player(Player.GetName(), Piece.e_PieceType.O, false);
            }
            else
            {
                m_SecondPlayer = new Player("Computer", Piece.e_PieceType.O, true);
            }
        }
        private void startToPlay()
        {
            bool isJumpMove, isQuitInput;
            while (true) // !GameOver
            {
                m_Board.PrintBoard();
                Console.WriteLine(m_CurrentPlayerTurn.PlayerName + "'s turn:");
                m_CurrentPlayerTurn.MakeMove(m_Board, out isJumpMove, out isQuitInput);
                if (isQuitInput)
                {
                    break;
                }
                if (isJumpMove)
                {
                    updateNumberOfPices();
                }
                m_Board.UpdateKingCase(m_CurrentPlayerTurn.PlayerPiece);
                changeTurn();
            }
            //סיכום של השחק והניקוד
        }
        private void changeTurn()
        {
            if (m_CurrentPlayerTurn.PlayerName == m_FirstPlayer.PlayerName)
            {
                m_CurrentPlayerTurn = m_SecondPlayer;
            }
            else
            {
                m_CurrentPlayerTurn = m_FirstPlayer;
            }
        }
        private void updateNumberOfPices()
        {
            if (m_FirstPlayer.PlayerName == m_CurrentPlayerTurn.PlayerName)
            {
                m_SecondPlayer.NumberOfPieces = m_SecondPlayer.NumberOfPieces - 1;
            }
            else
            {
                m_FirstPlayer.NumberOfPieces = m_FirstPlayer.NumberOfPieces - 1;
            }
        }
        private void setTheNumberOfPicesToThePlayers()
        {
            if (m_Board.Size == 6)
            {
                m_FirstPlayer.NumberOfPieces = 6;
                m_SecondPlayer.NumberOfPieces = 6;
            }
            else if (m_Board.Size == 8)
            {
                m_FirstPlayer.NumberOfPieces = 12;
                m_SecondPlayer.NumberOfPieces = 12;
            }
            else
            {
                m_FirstPlayer.NumberOfPieces = 20;
                m_SecondPlayer.NumberOfPieces = 20;
            }
        }
    }
}
