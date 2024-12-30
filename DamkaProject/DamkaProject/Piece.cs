using System;

namespace DamkaProject
{
    class Piece
    {
        public enum e_DirectionType 
        {
            Empty,
            Up,
            Down,
            Both
        }
        public enum e_PieceType 
        {
            Empty,
            X,
            O,
            U, // King of O
            K, // King of X
        }
        private e_PieceType m_Piece;
        private e_DirectionType m_Direction;

        public Piece(Piece.e_PieceType i_PieceType, Piece.e_DirectionType i_DirectionType)
        {
            m_Piece = i_PieceType;
            m_Direction = i_DirectionType;

        }
        public Piece.e_PieceType PieceType
        {
            get { return m_Piece; }
            set { m_Piece = value; }
        }
        public Piece.e_DirectionType DirectionType
        {
            get { return m_Direction; }
            set { m_Direction = value; }
        }
    }
}
