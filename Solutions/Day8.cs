namespace Solutions
{
    internal class Day8 : Solution<int>
    {
        private ForestTree[][] _forest { get; init; }

        public Day8() : base(8) => _forest = setTrees();

        private bool isEdge(int x, int y) => x == 0 || y == 0 || x == input.data.Length - 1 || y == input.data.First().Length - 1;

        protected override int partOne()
        {
            int totalVisible = (_forest.Length * _forest.First().Length) - ((_forest.Length - 2) * (_forest.First().Length - 2));
            for (int i = 1; i < _forest.Length - 1; i++)
            {
                for (int j = 1; j < _forest[i].Length - 1; j++)
                {
                    (bool up, bool right, bool left, bool down)
                        visibility = (
                        checkVisibility(i, j, 0, 1),
                        checkVisibility(i, j, 1, 0),
                        checkVisibility(i, j, -1, 0),
                        checkVisibility(i, j, 0, -1)
                        );

                    if (visibility.up || visibility.left || visibility.right || visibility.down)
                    {
                        totalVisible++;
                    }
                }
            }
            return totalVisible;
        }

        protected override int partTwo() => _forest.Max(f => f.Max(tr => tr.scenicScore));

        private bool checkVisibility(int i, int j, int iX, int jX)
        {
            int s = 0;
            while ((i + iX) < _forest.Length && i + iX >= 0 && j + jX < _forest[i].Length && j + jX >= 0)
            {
                s++;
                if (_forest[i][j].height <= _forest[i + iX][j + jX].height)
                {
                    _forest[i][j].scenicScore *= s;
                    return false;
                }
                if (iX != 0) iX += iX > 0 ? 1 : -1;
                if (jX != 0) jX += jX > 0 ? 1 : -1;
            }
            _forest[i][j].scenicScore *= s;
            _forest[i][j].isVisible = true;
            return true;
        }

        private ForestTree[][] setTrees()
        {
            var newForest = new ForestTree[input.data.Length][].Select(_ => new ForestTree[input.data[0].Length]).ToArray();
            for (int i = 0; i < input.data.Length; i++)
            {
                for (int j = 0; j < input.data[i].Length; j++)
                {
                    newForest[i][j] = new ForestTree(input.data[i][j]);
                    if (isEdge(i, j))
                        newForest[i][j].isVisible = true;
                }
            }
            return newForest;
        }

        private sealed class ForestTree
        {
            public bool isVisible { get; set; }
            public int height { get; init; }
            public int scenicScore { get; set; }

            public ForestTree(char val)
            {
                isVisible = false;
                height = (int)char.GetNumericValue(val);
                scenicScore = 1;
            }
        }
    }
}