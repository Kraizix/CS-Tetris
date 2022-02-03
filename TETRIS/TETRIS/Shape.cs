namespace TETRIS
{
    public class Shape
    {
        public int Width;
        public int Height;
        public int[,] Piece;
        public string Color;

        public Shape(int[,] piece, string color)
        {
            Piece = piece;
            Color = color;
            Height = piece.GetLength(0);
            Width = piece.GetLength(1);
        }
    }

}