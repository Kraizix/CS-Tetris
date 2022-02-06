    using System;

    namespace TETRIS
    {
        static class Shapes
        {
            private static Shape[] shapesArray;

            public static Shape GetRandomShape()
            {
                shapesArray = new Shape[]
                        {
                            new Shape(new int[,]{{ 1, 1 },{ 1, 1 }},"#ffff00"),
                            new Shape(new int[,]{{ 2, 2, 2, 2 }},"#00ffff"),
                            new Shape(new int[,]{{ 0, 3, 0 },{ 3, 3, 3 }},"#800080"),
                            new Shape(new int[,]{{ 0, 0, 4 },{ 4, 4, 4 }},"#ff7f00"),
                            new Shape(new int[,]{{ 5, 0, 0 },{ 5, 5, 5 }},"#0000ff"),
                            new Shape(new int[,]{{ 6, 6, 0 },{ 0, 6, 6 }},"#ff0000"),
                            new Shape(new int[,]{{ 0, 7, 7 },{ 7, 7, 0 }},"#00ff00")
                        };
                Random random = new Random();
                Shape shape = shapesArray[random.Next(shapesArray.Length)];
                return shape;
            }
        }
    }
