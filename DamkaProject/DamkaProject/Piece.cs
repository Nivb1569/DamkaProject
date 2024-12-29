using System;

namespace DamkaProject
{
    class Piece
    {
        public enum m_DirectionType //change name to e_DirectionType
        {
            Empty,
            Up,
            Down,
            Both
        }
        public enum m_PieceType //change name to e_PieceType
        {
            Empty,
            X,
            O,
            U, // King of O
            K, // King of X
        }
        private m_PieceType m_Piece;
        private m_DirectionType m_Direction;

        public Piece(Piece.m_PieceType i_PieceType, Piece.m_DirectionType i_DirectionType)
        {
            m_Piece = i_PieceType;
            m_Direction = i_DirectionType;

        }
        public Piece.m_PieceType PieceType
        {
            get { return m_Piece; }
            set { m_Piece = value; }
        }
        public Piece.m_DirectionType DirectionType
        {
            get { return m_Direction; }
            set { m_Direction = value; }
        }
    }
}
