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

        private int traverseMap(char source, char destination)
        {
            resetMap();
            var explorationDue = new Queue<ElfNode<char>>();
            var startingPosition = _map.SelectMany(r => r).First(y => y.height == source);
            source = source == 'S' ? 'a' : 'z';
            startingPosition.height = source;
            explorationDue.Enqueue(startingPosition);

            while (explorationDue.Count != 0)
            {
                var current = explorationDue.Dequeue();
                if (current.wasVisited)
                    continue;
                current.wasVisited = true;

                foreach (var direction in new (int r, int c)[] { (-1, 0), (1, 0), (0, -1), (0, 1) })
                {
                    (int nextRow, int nextCol) = (current.row + direction.r, current.col + direction.c);

                    if (nextRow >= 0 && nextRow < _map.Length && nextCol >= 0 && nextCol < _map[nextRow].Length)
                    {
                        (var nextNode, var currentNode) = (_map[nextRow][nextCol].height, current.height);

                        if (nextNode == destination && currentNode == (destination == 'E' ? 'z' : destination + 1))
                            return current.step + 1;

                        if (source > (destination == 'E' ? 'z' : destination + 1))
                            (nextNode, currentNode) = (currentNode, nextNode);
                        if (nextNode - currentNode <= 1)
                        {
                            _map[nextRow][nextCol].step = current.step + 1;
                            explorationDue.Enqueue(_map[nextRow][nextCol]);
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
