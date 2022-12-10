namespace Solutions
{
    internal class Day7 : Solution<int>
    {
        private readonly ElfFsManager _fsManager;
        public Day7() : base(7)
        {
            _fsManager = ElfFsManager.getManager();
            _fsManager.buildDirectoryTree(input.data);
            _fsManager.setTotalSizes(_fsManager.getRoot());
        }

        protected override int partOne()
        {
            var foldersInRange = new List<ElfFolder>();
            _fsManager.selectByRange(100000, foldersInRange, _fsManager.getRoot());
            return foldersInRange.Sum(z => z.totalSize);
        }

        protected override int partTwo()
        {
            var withinLimit = new List<ElfFolder>();
            _fsManager.selectDeletionCandidates(freeSpace: 70000000 - _fsManager.getRoot().totalSize, withinLimit, _fsManager.getRoot());
            return withinLimit.Min(fdr => fdr.totalSize);
        }
    }

    internal sealed class ElfFile
    {
        public int size;
        public string name;

        public ElfFile(int size, string name)
        {
            this.size = size;
            this.name = name;
        }
    }

    internal sealed class ElfFolder
    {
        private readonly string name;
        private readonly List<ElfFile> files;

        public int totalSize { get; set; }
        public ElfFolder? parent { get; init; }
        public List<ElfFolder> folders { get; set; }

        public ElfFolder(ElfFolder? parent, string name)
        {
            folders = new List<ElfFolder>();
            files = new List<ElfFile>();
            this.parent = parent;
            this.name = name;
        }

        public void touch(string name, int size)
        {
            files.Add(new ElfFile(size, name));
            totalSize += size;
        }

        public void mkdir(string name) => folders.Add(new ElfFolder(this, name));

        public ElfFolder cd(string name) => folders.First(f => f.name == name);
    }

    internal sealed class ElfFsManager
    {
        private static ElfFsManager _folderManager;
        private ElfFolder? _root;

        private ElfFsManager() { }

        public static ElfFsManager getManager()
        {
            if (_folderManager == null)
                _folderManager = new ElfFsManager();

            return _folderManager;
        }

        public ElfFolder? getRoot() => _root;

        public int setTotalSizes(ElfFolder currentFolder)
        {
            foreach (var dir in currentFolder.folders)
                currentFolder.totalSize += setTotalSizes(dir);

            return currentFolder.totalSize;
        }

        public void buildDirectoryTree(string[] source)
        {
            var newRoot = new ElfFolder(null, "/");
            var currentFolder = newRoot;

            for (int i = 0; i < source.Length; i++)
            {
                var commandSyntax = source[i].Split(' ');
                if (commandSyntax[0] == "$")
                {
                    if (commandSyntax[1] == "cd")
                    {
                        if (commandSyntax[2] == "..")
                            currentFolder = currentFolder!.parent;
                        else if (commandSyntax[2] == "/")
                            currentFolder = newRoot;
                        else
                            currentFolder = currentFolder.cd(commandSyntax[2]);
                    }
                }
                else
                {
                    if (commandSyntax[0] == "dir")
                        currentFolder.mkdir(commandSyntax[1]);
                    else
                        currentFolder.touch(commandSyntax[1], int.Parse(commandSyntax[0]));
                }
            }
            _root = newRoot;
        }
        public void selectByRange(int limit, List<ElfFolder> selection, ElfFolder currentFolder)
        {
            foreach (var dir in currentFolder.folders)
            {
                selectByRange(limit, selection, dir);
                if (dir.totalSize < limit)
                    selection.Add(dir);
            }
        }

        public void selectDeletionCandidates(int freeSpace, List<ElfFolder> deletable, ElfFolder currentFolder)
        {
            foreach (var dir in currentFolder.folders)
                selectDeletionCandidates(freeSpace, deletable, dir);

            if (freeSpace + currentFolder.totalSize >= 30000000)
                deletable.Add(currentFolder);
        }
    }
}
