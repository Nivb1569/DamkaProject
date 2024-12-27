using System;

namespace DamkaProject
{
    class Player
    {
        String m_PlayerName;
        Sign.m_SignType m_PlayerSign;
        bool m_IsComputer;

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
        public static void GetName()
        {
            bool isValid = false;
            String name;
            while (!isValid)
            {
                Console.WriteLine("Enter your name: ");
                name = Console.ReadLine();
                if(!isValidName(name))
                {
                    Console.WriteLine("Invalid input! Only letters (no spaces or ant other characters) and must be up to 20 characters");
                }
            }
        }
        private bool isValidName(String i_Name)
        {
            bool isValid = true;
            if (i_Name.Length > 20)
            {
                isValid = false;
            }
            else
            {
                for (int i = 0; i < i_Name.Length; i++)
                {

                }
            }
        }
    }
}
