namespace Solutions
{
    internal class Day8 : Solution<int, int>
    {
        private ForestTree[][] _forest { get; init; }

        public Day8() : base(8) => _forest = setTrees();

        protected override int partOne() => _forest.Sum(r => r.Count(tr => tr.isVisible));

        protected override int partTwo() => _forest.Max(f => f.Max(tr => tr.scenicScore));

        private bool isEdge(int x, int y) => x == 0 || y == 0 || x == input.data.Length - 1 || y == input.data.First().Length - 1;

        private void processVisibility(ForestTree tree, int i, int j, int iX, int jX)
        {
            int s = 0;
            while ((i + iX) < input.data.Length && i + iX >= 0 && j + jX < input.data[i].Length && j + jX >= 0)
            {
                s++;
                if (tree.height <= (int)char.GetNumericValue(input.data[i + iX][j + jX]))
                {
                    tree.scenicScore *= s;
                    return;
                }
                if (iX != 0) iX += iX > 0 ? 1 : -1;
                if (jX != 0) jX += jX > 0 ? 1 : -1;
            }
            tree.scenicScore *= s;
            tree.isVisible = true;
        }

        private ForestTree[][] setTrees()
        {
            var newForest = new ForestTree[input.data.Length][].Select(_ => new ForestTree[input.data[0].Length]).ToArray();
            for (int i = 0; i < input.data.Length; i++)
            {
                for (int j = 0; j < input.data[i].Length; j++)
                {
                    var newTree = new ForestTree(input.data[i][j]);
                    if (isEdge(i, j))
                        newTree.isVisible = true;
                    else
                    {
                        processVisibility(newTree, i, j, 0, 1);
                        processVisibility(newTree, i, j, 1, 0);
                        processVisibility(newTree, i, j, -1, 0);
                        processVisibility(newTree, i, j, 0, -1);
                    }
                    newForest[i][j] = newTree;
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