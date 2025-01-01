using System;
using System.Collections.Generic;
using Ex02;

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
        private bool m_GameOver = false;
        private Player m_Winner = null;
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
                m_SecondPlayer = new Player(Player.GetName(m_FirstPlayer.PlayerName), Piece.e_PieceType.O, false);
            }
            else
            {
                m_SecondPlayer = new Player("Computer", Piece.e_PieceType.O, true);
            }
        }
        private void startToPlay()
        {
            bool isJumpMove, isQuitInput, anotherJump = false, samePlayer = false, wantToPlay = true;
            Point o_FromPrev = null;
            Point o_ToPrev = null;
            List<Point[]> o_NextJumpMove = null;
            while (wantToPlay) 
            {
                o_FromPrev = null;
                o_ToPrev = null;
                o_NextJumpMove = null;
                anotherJump = false;
                samePlayer = false;
                init();
                while (!GameOver) 
                {
                    Ex02.ConsoleUtils.Screen.Clear();
                    m_Board.PrintBoard();
                    printPreviousMove(m_CurrentPlayerTurn, o_FromPrev, o_ToPrev, samePlayer);
                    printCurrentTurn(m_CurrentPlayerTurn);
                    m_CurrentPlayerTurn.MakeMove(m_Board, out isJumpMove, out isQuitInput, out o_FromPrev, out o_ToPrev, anotherJump, o_NextJumpMove);
                    anotherJump = false;
                    if (isQuitInput)
                    {
                        updateWinnerPlayer();
                        GameOver = true;
                    }
                    if (isJumpMove)
                    {
                        updateNumberOfPices();
                        anotherJump = m_CurrentPlayerTurn.checkIfCanJumpAndMakeList(m_Board, o_ToPrev, out o_NextJumpMove);
                        if(anotherJump)
                            samePlayer = true;
                    }                    
                    m_Board.UpdateKingCase(m_CurrentPlayerTurn.PlayerPiece);
                    if (!anotherJump) 
                    {
                         changeTurn();
                         samePlayer = false;
                    }
                    checkGameStatus();
                }
                updatePlayerPoints();
                printSummaryOfTheGame();
                wantToPlay = doYouWantToPlayAnotherRound();
            }
            gameIsOverMessage();
        }
        private void updatePlayerPoints()
        {
            if (m_Winner != null)
            {
                if(m_Winner == m_FirstPlayer)
                {
                    m_FirstPlayer.addPoints(calcPoints(m_FirstPlayer) - calcPoints(m_SecondPlayer));
                }
                else
                {
                    m_SecondPlayer.addPoints(calcPoints(m_SecondPlayer) - calcPoints(m_FirstPlayer));
                }
            }
        }
        private int calcPoints(Player i_Player)
        {
            int counterPoints = 0;
            for (int i = 0; i < m_Board.Size; i++)
            {
                for (int j = 0; j < m_Board.Size; j++)
                {
                    if (m_Board.GameBoard[i, j].PieceType == i_Player.PlayerPiece)
                    {
                        counterPoints++;
                    }
                    else if (m_Board.GameBoard[i, j].PieceType == Piece.e_PieceType.U && i_Player.PlayerPiece == Piece.e_PieceType.O)
                    {
                        counterPoints += 4;
                    }
                    else if (m_Board.GameBoard[i, j].PieceType == Piece.e_PieceType.K && i_Player.PlayerPiece == Piece.e_PieceType.X)
                    {
                        counterPoints += 4;
                    }

                }
            }

            return counterPoints;
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
        private void printSummaryOfTheGame()
        {
            if (m_Winner == null)
            {
                Console.WriteLine("The game is over with tie.");
            }
            else
            {
                Console.WriteLine($"Good job {m_Winner.PlayerName}, you are the winner!");
            }
            Console.WriteLine($"{m_FirstPlayer.PlayerName} points is: {m_FirstPlayer.Points}");
            Console.WriteLine($"{m_SecondPlayer.PlayerName} points is: {m_SecondPlayer.Points}");
        }
        private bool doYouWantToPlayAnotherRound()
        {
            bool isValid = false;
            bool wantToPlay = false;
            int choice = -1;
            while (!isValid)
            {
                Console.WriteLine("For playing another round enter 0.");
                Console.WriteLine("For finish the game and exit enter 1.");
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
                        if (choice == 0)
                        {
                            wantToPlay = true;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input! Only 0 or 1 are valid choices.");
                }
            }

            return wantToPlay;
        }
        private void gameIsOverMessage()
        {
            Console.WriteLine("Thank you for playing with us, have a nice day!");
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
        public bool GameOver
        {
            get { return m_GameOver; }
            set { m_GameOver = value; }
        }
        private void updateWinnerPlayer()
        {
            if (m_CurrentPlayerTurn.PlayerName == m_FirstPlayer.PlayerName)
            {
                m_Winner = m_SecondPlayer;
            }
            else
            {
                m_Winner = m_FirstPlayer; 
            }
        }
        private void checkGameStatus() 
        {
            if (m_FirstPlayer.NumberOfPieces == 0 || m_SecondPlayer.NumberOfPieces == 0)
            {
                GameOver = true;
                m_Winner = m_FirstPlayer.NumberOfPieces == 0? m_SecondPlayer:m_FirstPlayer;
            }
            else if (m_FirstPlayer.NoMovesLeft(m_Board) && m_FirstPlayer == m_CurrentPlayerTurn)
            {
                GameOver = true;
                m_Winner = m_SecondPlayer;
            }
            else if (m_SecondPlayer.NoMovesLeft(m_Board) && m_SecondPlayer == m_CurrentPlayerTurn)
            {
                GameOver = true;
                m_Winner = m_FirstPlayer;
            }
            else if (m_FirstPlayer.NoMovesLeft(m_Board) && m_SecondPlayer.NoMovesLeft(m_Board))
            {
                GameOver = true;
            }

            if (GameOver)
            {
                m_Board.PrintBoard();
            }
        }
        private void printPreviousMove(Player i_currentPlayer, Point i_From, Point i_To, bool samePlayer)
        {
            Player previousPlayer = null;
            if (samePlayer)
            {
                previousPlayer = i_currentPlayer;
            }
            else
            {
                if (i_currentPlayer == m_FirstPlayer)
                {
                    previousPlayer = m_SecondPlayer;
                }
                else
                {
                    previousPlayer = m_FirstPlayer;
                }
                
            }
            if(i_From!=null && i_To!=null)
            {
                string previuosMove = Point.convertPointsToString(i_From, i_To);

                Console.WriteLine(previousPlayer.PlayerName + "'s move was (" + previousPlayer.PlayerPiece + "): " + previuosMove);
            }


        }

        private void printCurrentTurn(Player i_currentPlayer)
        {
            Console.Write(m_CurrentPlayerTurn.PlayerName + "'s turn (" + m_CurrentPlayerTurn.PlayerPiece + ") : ");
            if(m_CurrentPlayerTurn.PlayerName == "Computer")
            {
                Console.Write("(press 'enter' to see it's move)");
                Console.ReadLine();
            }

        }
        private void init()
        {
            m_Board = new Board(m_Board.Size);
            setTheNumberOfPicesToThePlayers();
            m_CurrentPlayerTurn = m_FirstPlayer;
            m_Winner = null;
            m_GameOver = false;
        }


    }
}
