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
            foreach (var dir in readInstructions())
                rope.moveRope(dir);
            return rope.tailHistory.Count;
        }

        protected override int partTwo()
        {
            var rope = new ElfRope(10);
            foreach (var dir in readInstructions())
                rope.moveRope(dir);
            return rope.tailHistory.Count;
        }

        private IEnumerable<(int, int)> readInstructions()
        {
            foreach (var line in input.data)
            {
                string[] syntax = line.Split(" ");
                int.TryParse(syntax[1], out int count);
                while (count-- > 0)
                    yield return directionKey[syntax[0]];
            }
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
                return follow(hd.next, n + 1);
            }
            else
                return hd;
        }
    }
}