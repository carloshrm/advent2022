using Solutions;

using Spectre.Console;

namespace Advent2022
{
    internal class Day14 : Solution<int, int>
    {
        private int bottomEdge = 0;
        private int sideEdge = 0;
        private List<Rock> cave;
        private List<Sand> sand;

        public Day14() : base(14)
        {
            //input = (input.example, input.example);
            //Console.CursorVisible = false;
            //Console.SetBufferSize(2048, 2048);
            cave = new List<Rock>();
            sand = new List<Sand>();
            buildCave();

        }

        protected override int partOne()
        {
            int count = 0;
            while (dropSand())
                count++;
            return count;
        }

        protected override int partTwo()
        {
            int count = 0;
            //sand.ForEach(s => s.erase());
            sand.Clear();
            for (int i = 1; i < sideEdge * 2; i++)
            {
                cave.Add(new Rock(bottomEdge + 2, i));
                //cave.Last().draw();
            }
            while (dropSand())
                count++;
            return count + 1;
        }

        private void buildCave()
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

                    if (secondPoint.Last() > bottomEdge)
                        bottomEdge = secondPoint.Last();
                    if (secondPoint.First() > sideEdge)
                        sideEdge = secondPoint.First();
                }
            }
            //Console.SetCursorPosition(500, 0);
            //Console.Write("*");
            //cave.ForEach(r => r.draw());
        }

        private bool dropSand()
        {
            var newSand = new Sand();
            int dropState = 1;
            while (dropState != -1)
            {
                foreach (var obj in new List<IStuffOnAGrid>().Concat(cave.Where(c => c.row == newSand.row + 1)).Concat(sand.Where(s => s.row + 1 > newSand.row)))
                {
                    dropState = newSand.checkSurroundings(obj);
                    if (dropState != 1)
                        break;
                }

                if (dropState == 1)
                    newSand.drop();
                else if (dropState == -1)
                    break;

                if (newSand.row > bottomEdge + 2)
                    return false;
            }
            //newSand.draw();
            sand.Add(newSand);
            sand.RemoveAll(s => s.row == newSand.row + 2 && s.col == newSand.col);
            if (newSand.row <= 0)
                return false;
            return true;
        }

    }

    internal interface IStuffOnAGrid
    {
        int row { get; set; }
        int col { get; set; }
    }

    internal class Rock : IStuffOnAGrid
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
            Console.SetCursorPosition(col, row);
            AnsiConsole.Markup($"[bold white]#[/]");
        }
    }

    internal class Sand : IStuffOnAGrid
    {
        public int row { get; set; }
        public int col { get; set; }

        public (int row, int col) fallingDir;
        public bool dropping;
        public bool packed;

        public Sand()
        {
            row = 0;
            col = 500;
            fallingDir = (1, 0);
            dropping = true;
            packed = false;
        }

        public void draw()
        {
            Console.SetCursorPosition(col, row);
            AnsiConsole.Markup($"[bold sandybrown]*[/]");
        }

        public void pack() =>
            //Console.SetCursorPosition(col, row);
            //AnsiConsole.Markup($"[bold red]*[/]");
            packed = true;

        public void erase()
        {
            Console.SetCursorPosition(col, row);
            Console.Write(' ');
        }

        public void drop()
        {
            //erase();
            row += fallingDir.row;
            col += fallingDir.col;
            fallingDir = (1, 0);
            //draw();
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

        public int checkSurroundings(IStuffOnAGrid r)
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