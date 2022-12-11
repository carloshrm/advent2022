using Solutions;

namespace Solution
{
    internal class Day9 : Solution<int>
    {
        private readonly IReadOnlyDictionary<string, (int, int)> directionKey = new Dictionary<string, (int, int)>
        {
            {"L", (0, -1) },
            {"R", (0, 1) },
            {"U", (-1, 0) },
            {"D", (1, 0) },
        };

        public Day9() : base(9) { }

        protected override int partOne()
        {
            var rope = new ElfRope(2);
            foreach (var line in input.data)
            {
                var instruction = line.Split(' ');
                int.TryParse(instruction[1], out int count);
                while (count-- > 0)
                    rope.moveRope(directionKey[instruction[0]]);
            }
            return rope.tailHistory.Count();
        }

        protected override int partTwo()
        {
            var rope = new ElfRope(10);
            foreach (var line in input.data)
            {
                var instruction = line.Split(' ');
                int.TryParse(instruction[1], out int count);
                while (count-- > 0)
                    rope.moveRope(directionKey[instruction[0]]);
            }
            return rope.tailHistory.Count();
        }
    }

    internal class ElfRope
    {
        private Knot head;

        public HashSet<(int row, int col)> tailHistory { get; init; }

        public ElfRope(int knotCount)
        {
            head = new Knot(0, 0);
            Knot currentKnot = head;
            while (--knotCount > 0)
            {
                currentKnot.next = new Knot(0, 0);
                currentKnot = currentKnot.next;
            }
            tailHistory = new HashSet<(int row, int col)>();
        }

        public void moveRope((int r, int c) direction)
        {
            head.move(direction);
            tailHistory.Add(Knot.follow(head, 0).getCoords());
        }
    }

    internal class Knot
    {
        public int row;
        public int col;
        public Knot? next;

        public Knot(int r, int c)
        {
            row = r;
            col = c;
            next = null;
        }

        public (int, int) getCoords() => (row, col);

        public void move((int r, int c) direction)
        {
            row += direction.r;
            col += direction.c;
            //Console.SetCursorPosition(0, 0);
            //for (int i = 0; i < 40; i++)
            //{
            //    for (int j = 0; j < 40; j++)
            //    {
            //        Console.Write(" ");
            //    }
            //    Console.WriteLine("");
            //}
            //Console.SetCursorPosition(col + 20, row + 20);
            //Console.Write("#");
            //Thread.Sleep(100);
        }

        public static Knot follow(Knot hd, int n)
        {
            if (hd.next != null)
            {
                var vertDifference = hd.row - hd.next.row;
                var horzDifference = hd.col - hd.next.col;

                if (Math.Abs(vertDifference) > 1 || Math.Abs(horzDifference) > 1)
                {
                    if (vertDifference != 0)
                        hd.next.row += vertDifference > 0 ? 1 : -1;
                    if (horzDifference != 0)
                        hd.next.col += horzDifference > 0 ? 1 : -1;
                }
                //Console.SetCursorPosition(hd.next.col + 20, hd.next.row + 20);
                //Console.Write("" + n);
                //Thread.Sleep(50);
                return follow(hd.next, n + 1);
            }
            else
                return hd;
        }
    }
}