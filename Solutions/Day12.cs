namespace Solutions
{
    internal class ElfNode<T>
    {
        public T height;
        public int row;
        public int col;
        public bool wasVisited = false;
        public int step;

        public ElfNode(T v, int row, int col)
        {
            height = v;
            this.row = row;
            this.col = col;
        }
    }

    internal class Day12 : Solution<int, int>
    {
        private ElfNode<char>[][] _map;

        public Day12() : base(12)
        {
            _map = input.data.Select((l, i) => l.Select((c, j) => new ElfNode<char>(c, i, j)).ToArray()).ToArray();
        }

        protected override int partOne() => traverseMap('S', 'E');

        protected override int partTwo() => traverseMap('E', 'a');

        private int traverseMap(char from, char to)
        {
            resetMap();
            var explorationDue = new Queue<ElfNode<char>>();
            var startingPosition = _map.SelectMany(r => r).First(y => y.height == from);
            from = from == 'S' ? 'a' : 'z';
            startingPosition.height = from;
            explorationDue.Enqueue(startingPosition);

            while (explorationDue.Count != 0)
            {
                var current = explorationDue.Dequeue();
                if (current.wasVisited)
                    continue;
                current.wasVisited = true;

                foreach (var direction in new (int r, int c)[] { (-1, 0), (1, 0), (0, -1), (0, 1) })
                {
                    (int nr, int nc) = (current.row + direction.r, current.col + direction.c);

                    if (nr >= 0 && nr < _map.Length && nc >= 0 && nc < _map[nr].Length)
                    {
                        (var nxt, var crr) = (_map[nr][nc].height, current.height);

                        if (nxt == to && crr == (to == 'E' ? 'z' : to + 1))
                            return current.step + 1;

                        if (from > (to == 'E' ? 'z' : to + 1))
                            (nxt, crr) = (crr, nxt);
                        if (nxt - crr <= 1)
                        {
                            _map[nr][nc].step = current.step + 1;
                            explorationDue.Enqueue(_map[nr][nc]);
                        }
                    }
                }

            }
            return -1;
        }
        private void resetMap()
        {
            foreach (var ln in _map)
            {
                foreach (var n in ln)
                {
                    n.step = 0;
                    n.wasVisited = false;
                }
            }
        }

    }
}
