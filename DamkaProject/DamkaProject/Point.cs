using System;

namespace DamkaProject
{
    class Point
    {
        private int m_X;
        private int m_Y;
        public Point(int i_X, int i_Y)
        {
            m_X = i_X;
            m_Y = i_Y;
        }
        public int X
        {
            get { return m_X; }
            set { m_X = value; }
        }
        public int Y
        {
            get { return m_Y; }
            set { m_Y = value; }
        }

        public static String convertPointsToString(Point p1, Point p2)
        {
            char xP1 = (char)('A' + p1.X);
            char yP1 = (char)('a' + p1.Y);
            char xP2 = (char)('A' + p2.X);
            char yP2 = (char)('a' + p2.Y);

            return string.Format("{0}{1}>{2}{3}",xP1,yP1,xP2,yP2);
            
        }

    }
}
