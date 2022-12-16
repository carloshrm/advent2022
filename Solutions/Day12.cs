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

        protected override int partOne()
        {
            var explorationDue = new Queue<ElfNode<char>>();

            var startingPosition = _map.First(r => r.Any(y => y.height == 'S')).First();
            startingPosition.height = 'a';
            explorationDue.Enqueue(startingPosition);

            while (explorationDue.Any())
            {
                var current = explorationDue.Dequeue();

                if (current.wasVisited)
                    continue;
                current.wasVisited = true;

                char height = current.height;

                foreach (var direction in new (int r, int c)[] { (-1, 0), (1, 0), (0, -1), (0, 1) })
                {
                    (int nr, int nc) = (current.row + direction.r, current.col + direction.c);
                    if (nr >= 0 && nr < _map.Length && nc >= 0 && nc < _map[nr].Length)
                    {
                        if (_map[nr][nc].height == 'E' && height == 'z')
                            return current.step + 1;

                        if (current.height - _map[nr][nc].height >= -1)
                        {
                            _map[nr][nc].step = current.step + 1;
                            explorationDue.Enqueue(_map[nr][nc]);
                        }
                    }
                }

            }
            return 0;
        }

        protected override int partTwo() => 0;

    }
}
