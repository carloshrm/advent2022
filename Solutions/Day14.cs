using Solutions;

namespace Advent2022
{
    internal class Day14 : Solution<int, int>
    {
        private readonly int[] sandHole = { 500, 0 };
        private int bottom = 0;
        private List<Rock> cave;
        private List<Sand> sand;

        public Day14() : base(14)
        {
            Console.CursorVisible = false;
            cave = new List<Rock>();
            sand = new List<Sand>();
            //input = (input.example, input.example);
            //for (int i = 0; i < 10; i++)
            //{
            //    for (int j = 0; j < 20; j++)
            //    {
            //        Console.Write(".");
            //    }
            //    Console.WriteLine();
            //}
        }

        protected override int partOne()
        {
            foreach (var ln in input.data)
            {
                var scanInfo = ln.Split("->");

                for (int i = 0; i < scanInfo.Length - 1; i++)
                {
                    var firstPoint = scanInfo[i].Split(",").Select(a => int.Parse(a));
                    var secondPoint = scanInfo[i + 1].Split(",").Select(a => int.Parse(a));

                    int xOffset = firstPoint.First() - secondPoint.First();
                    int yOffset = firstPoint.Last() - secondPoint.Last();

                    while (xOffset != 0 || yOffset != 0)
                    {
                        int x = secondPoint.First() + xOffset;
                        int y = secondPoint.Last() + yOffset;
                        cave.Add(new Rock(y, x));
                        xOffset += xOffset == 0 ? 0 : xOffset > 0 ? -1 : 1;
                        yOffset += yOffset == 0 ? 0 : yOffset > 0 ? -1 : 1;
                    }
                    cave.Add(new Rock(secondPoint.Last(), secondPoint.First()));
                    if (secondPoint.Last() > bottom)
                        bottom = secondPoint.Last();
                }
            }
            Console.SetCursorPosition(500 - 400, 0);
            Console.Write("*");
            cave.ForEach(r => r.draw());
            dropSand();
            return sand.Count;
        }


        private void dropSand()
        {
            var newSand = new Sand();
            newSand.draw();
            int dropState = 1;
            while (dropState != -1)
            {
                foreach (var obj in new List<ITwoDimObj>().Concat(cave).Concat(sand))
                {
                    dropState = newSand.checkSurroundings(obj);
                    if (dropState != 1) break;
                }
                if (dropState == 1)
                {
                    newSand.drop();
                }
                else if (dropState == -1)
                {
                    break;
                }
                if (newSand.row > bottom)
                    return;

            }
            sand.Add(newSand);
            dropSand();
        }

        protected override int partTwo() => base.partTwo();
    }

    internal interface ITwoDimObj
    {
        int row { get; set; }
        int col { get; set; }
    }

    internal class Rock : ITwoDimObj
    {
        public int row { get; set; }
        public int col { get; set; }

        public Rock(int r, int c)
        {
            row = r;
            col = c;
        }

        public void draw()
        {
            Console.SetCursorPosition(col - 400, row);
            Console.Write('#');
        }
    }

    internal class Sand : ITwoDimObj
    {
        public int row { get; set; }
        public int col { get; set; }

        public (int row, int col) fallingDir;
        public bool dropping;

        public Sand()
        {
            row = 0;
            col = 500;
            fallingDir = (1, 0);
            dropping = true;
        }

        public void draw()
        {
            Console.SetCursorPosition(col - 400, row);
            Console.Write('o');
        }

        public void erase()
        {
            Console.SetCursorPosition(col - 400, row);
            Console.Write(' ');
        }

        public void drop()
        {
            erase();
            row += fallingDir.row;
            col += fallingDir.col;
            fallingDir = (1, 0);
            draw();
        }

        private bool switchDirection()
        {
            if (fallingDir == (1, 0))
                fallingDir = (1, -1);
            else if (fallingDir == (1, -1))
                fallingDir = (1, 1);
            else
                return false;

            return true;
        }

        public int checkSurroundings(ITwoDimObj r)
        {
            if (row + fallingDir.row == r.row && col + fallingDir.col == r.col)
            {
                if (switchDirection())
                    return 0;
                else
                {
                    dropping = false;
                    return -1;
                }
            }
            else
                return 1;
        }
    }
}