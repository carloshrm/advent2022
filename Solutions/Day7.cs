namespace Solutions
{
    internal class Day7 : Solution<int, int>
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
            _fsManager.selectByRange(limit: 100000, foldersInRange);
            return foldersInRange.Sum(fdr => fdr.totalSize);
        }

        protected override int partTwo()
        {
            var deletables = new List<ElfFolder>();
            _fsManager.selectDeletionCandidates(freeSpace: 70000000 - _fsManager.getRoot().totalSize, deletables);
            return deletables.Min(fdr => fdr.totalSize);
        }
    }

    internal class ElfFile
    {
        public int size;
        public string name;

        public ElfFile(int size, string name)
        {
            this.size = size;
            this.name = name;
        }
    }

    internal class ElfFolder
    {
        private readonly string name;
        public List<ElfFolder> folders { get; }
        public List<ElfFile> files { get; }
        public ElfFolder? parent { get; init; }
        public int totalSize { get; set; }

        public ElfFolder(ElfFolder? parent, string name)
        {
            folders = new List<ElfFolder>();
            files = new List<ElfFile>();
            this.parent = parent;
            this.name = name;
        }

        public void cat(string name, int size) => files.Add(new ElfFile(size, name));

        public void mkdir(string name) => folders.Add(new ElfFolder(this, name));

        public ElfFolder cd(string name) => folders.First(f => f.name == name);
    }

    internal class ElfFsManager
    {
        private static ElfFsManager? _folderManager;
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
            currentFolder.totalSize += currentFolder.files.Sum(fl => fl.size);
            foreach (var dir in currentFolder.folders)
                currentFolder.totalSize += setTotalSizes(dir);
            return currentFolder.totalSize;
        }

        public void buildDirectoryTree(string[] source)
        {
            _root = new ElfFolder(null, "/");
            var currentFolder = _root;
            foreach (var cmd in source.Select(ln => ln.Split(' ')))
            {
                if (cmd[0] == "$" && cmd[1] == "cd")
                {
                    if (cmd[2] == "..")
                        currentFolder = currentFolder!.parent;
                    else if (cmd[2] == "/")
                        currentFolder = _root;
                    else
                        currentFolder = currentFolder!.cd(cmd[2]);
                }
                else if (cmd[0] != "$")
                {
                    if (cmd[0] == "dir")
                        currentFolder!.mkdir(cmd[1]);
                    else
                        currentFolder!.cat(cmd[1], int.Parse(cmd[0]));
                }
            }
        }

        public void selectByRange(int limit, List<ElfFolder> selection, ElfFolder? currentFolder = null)
        {
            if (currentFolder == null)
                currentFolder = _root;

            foreach (var dir in currentFolder!.folders)
            {
                selectByRange(limit, selection, dir);
                if (dir.totalSize < limit)
                    selection.Add(dir);
            }
        }

        public void selectDeletionCandidates(int freeSpace, List<ElfFolder> deletable, ElfFolder? currentFolder = null)
        {
            if (currentFolder == null)
                currentFolder = _root;

            foreach (var dir in currentFolder!.folders)
                selectDeletionCandidates(freeSpace, deletable, dir);

            if (freeSpace + currentFolder.totalSize >= 30000000)
                deletable.Add(currentFolder);
        }
    }
}
