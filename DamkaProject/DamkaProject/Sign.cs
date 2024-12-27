using System;

namespace DamkaProject
{
    class Sign
    {
        public enum m_DirectionType
        {
            Empty,
            Up,
            Down,
            Both
        }
        public enum m_SignType
        {
            Empty,
            X,
            O,
            K,
            Q
        }
        private m_SignType m_Sign;
        private m_DirectionType m_Direction;

        public Sign(Sign.m_SignType i_SignType, Sign.m_DirectionType i_DirectionType)
        {
            m_Sign = i_SignType;
            m_Direction = i_DirectionType;

        }
        public Sign.m_SignType SignType
        {
            get { return m_Sign; }
        }
        public Sign.m_DirectionType DirectionType
        {
            get { return m_Direction; }
        }
    }
}
