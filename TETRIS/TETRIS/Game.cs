namespace TETRIS
{
    public class Game
    {
        public int currentX;
        public int currentY;
        public int[,] grid;
        public Shape currentShape;
        public Shape nextShape;
        public int[,] backup;
        public int width = 10;
        public int height = 20;
        public bool ended = false;
        public int score = 0;
        private readonly int[] scores = { 100, 200, 500, 800 };
        public Game()
        {
            grid = new int[width, height];
            currentShape = GetNewShape();
            System.Threading.Thread.Sleep(100);
            nextShape = GetNewShape();
            if (currentShape.Color == "#ffff00")
            {
                currentX = 4;
            }
        }

        public Shape GetNewShape()
        {
            Shape shape = Shapes.GetRandomShape();
            currentX = 3;
            currentY = -1;

            return shape;
        }

        public bool CanMove(int posX, int posY)
        {
            int newX = posX + currentX;
            int newY = posY + currentY;
            if (newX < 0 || newX + currentShape.Width > width || newY + currentShape.Height > height)
            {
                return false;
            }
            for (int i = 0; i < currentShape.Width; i++)
            {
                for (int j = 0; j < currentShape.Height; j++)
                {
                    if (newY + j > 0 && grid[newX + i, newY + j] != 0 && currentShape.Piece[j, i] != 0)
                    {
                        return false;
                    }

                }
            }
            currentX = newX;
            currentY = newY;
            return true;
        }

        public bool UpdateArray()
        {
            for (int i = 0; i < currentShape.Width; i++)
            {
                for (int j = 0; j < currentShape.Height; j++)
                {
                    if (currentShape.Piece[j, i] != 0)
                    {
                        if(currentX + i <0 || currentX + i > width || currentY + j < 0 || currentY + j > height)
                        {
                            return true;
                        }
                        grid[currentX + i, currentY + j] = currentShape.Piece[j,i];
                    }
                }
            }
            return false;
        }
        public void Rotate()
        {
            backup = currentShape.Piece;
            currentShape.Piece = new int[currentShape.Width, currentShape.Height];
            for (int i = 0; i < currentShape.Width; i++)
            {
                for (int j = 0; j < currentShape.Height; j++)
                {
                    currentShape.Piece[i, j] = backup[currentShape.Height - 1 - j, i];
                }
            }
            int temp = currentShape.Width;
            currentShape.Width = currentShape.Height;
            currentShape.Height = temp;
        }

        public void ReverseRotate()
        {
            backup = currentShape.Piece;
            currentShape.Piece = new int[currentShape.Width, currentShape.Height];
            for (int i = 0; i < currentShape.Width; i++)
            {
                for (int j = 0; j < currentShape.Height; j++)
                {
                    currentShape.Piece[i, j] = backup[j, currentShape.Width-1-i];
                }
            }
            int temp = currentShape.Width;
            currentShape.Width = currentShape.Height;
            currentShape.Height = temp;
        }
        public void Rollback()
        {
            currentShape.Piece = backup;
            int temp = currentShape.Width;
            currentShape.Width = currentShape.Height;
            currentShape.Height = temp;
        }

        public bool ClearRows()
        {
            int nb_lines = 0;
            for (int i = 1; i < height; i++)
            {
                int empty_case = 0;
                for (int j = 0; j <width; j++)
                {
                    if (grid[j,i] == 0)
                    {
                        empty_case++;
                    }
                }
                if (empty_case == 0)
                {
                    nb_lines++;
                    for (int k = i; k > 1; k--)
                    {
                        for(int l = 0; l < width; l++)
                        {
                            grid[l,k] = grid[l,k-1];
                        }
                    }
                   
                }
            }
            if (nb_lines != 0)
            {
                score += scores[nb_lines - 1];
                return true;
            }
            return false;
        }
    }

}