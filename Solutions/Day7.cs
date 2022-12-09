namespace Namespace
{
    internal class Day7 : Solution<int>
    {
        private ElfFolder fileSystem;

        public Day7() : base(7)
        {
            buildDirectoryTree();
            setTotalSizes(fileSystem);
        }

        protected override int partOne()
        {
            var foldersInRange = new List<ElfFolder>();
            selectByRange(100000, fileSystem, foldersInRange);
            return foldersInRange.Sum(z => z.totalSize);
        }

        protected override int partTwo()
        {
            var withinLimit = new List<ElfFolder>();
            selectDeletionCandidates(70000000 - fileSystem.totalSize, fileSystem, withinLimit);
            return withinLimit.Min(fdr => fdr.totalSize);
        }

        private void buildDirectoryTree()
        {
            fileSystem = new ElfFolder(null, "/");
            var currentFolder = fileSystem;

            for (int i = 0; i < input.data.Length; i++)
            {
                var commandSyntax = input.data[i].Split(' ');
                if (commandSyntax[0] == "$")
                {
                    if (commandSyntax[1] == "cd")
                    {
                        if (commandSyntax[2] == "..")
                            currentFolder = currentFolder.parent;
                        else if (commandSyntax[2] == "/")
                            currentFolder = fileSystem;
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
        }

        private void selectByRange(int limit, ElfFolder currentFolder, List<ElfFolder> selection)
        {
            foreach (var dir in currentFolder.folders)
            {
                selectByRange(limit, dir, selection);
                if (dir.totalSize < limit)
                    selection.Add(dir);
            }
        }

        private int setTotalSizes(ElfFolder currentFolder)
        {
            foreach (var dir in currentFolder.folders)
                currentFolder.totalSize += setTotalSizes(dir);
            return currentFolder.totalSize;
        }

        private void selectDeletionCandidates(int freeSpace, ElfFolder currentFolder, List<ElfFolder> deletable)
        {
            foreach (var dir in currentFolder.folders)
                selectDeletionCandidates(freeSpace, dir, deletable);
            if (freeSpace + currentFolder.totalSize >= 30000000)
                deletable.Add(currentFolder);
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
        public string name;
        public ElfFolder? parent;
        public int totalSize;
        public List<ElfFolder> folders { get; set; }
        public List<ElfFile> files { get; set; }

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

        public void mkdir(string name)
        {
            folders.Add(new ElfFolder(this, name));
        }

        public ElfFile getFile(string name)
        {
            return files.First(f => f.name == name);
        }

        public ElfFolder cd(string name)
        {
            return folders.First(f => f.name == name);
        }
    }
}
