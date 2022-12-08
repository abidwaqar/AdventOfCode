namespace AdventOfCode._22._7_NoSpaceLeftOnDevice;

internal static class PartTwo
{
    // T = O(n) | S = O(n * log(n))
    public static int Solve()
    {
        Directory rootDirectory = new Directory("/", null);
        Directory currentDirectory = rootDirectory;
        foreach (var input in System.IO.File.ReadAllLines("../../../input.txt"))
        {
            if (input == "$ cd /" || input == "$ ls")
            {
                continue;
            }

            if (input.StartsWith("dir "))
            {
                string childDirectoryName = input.Split("dir ", StringSplitOptions.RemoveEmptyEntries).Single();
                if (!currentDirectory.directories.ContainsKey(childDirectoryName))
                {
                    currentDirectory.directories.Add(childDirectoryName, new Directory(childDirectoryName, currentDirectory));
                }
            }
            else if (input.StartsWith("$ cd "))
            {
                string nextDirectoryName = input.Split("$ cd ", StringSplitOptions.RemoveEmptyEntries).Single();
                if (nextDirectoryName == "..")
                {
                    if (currentDirectory.parent != null)
                    {
                        currentDirectory = currentDirectory.parent;
                    }
                }
                else
                {
                    currentDirectory = currentDirectory.directories[nextDirectoryName];
                }
            }
            else
            {
                string[] fileInfo = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                currentDirectory.files.Add(new File(fileInfo[1], Convert.ToInt32(fileInfo[0])));
            }
        }

        PriorityQueue<int, int> dirSizes = new PriorityQueue<int, int>();
        int spaceToFreeUp = 30000000 - (70000000 - dfs(rootDirectory, dirSizes));
        while (dirSizes.Peek() < spaceToFreeUp)
        {
            dirSizes.Dequeue();
        }

        return dirSizes.Dequeue();
    }

    private static int dfs(Directory dir, PriorityQueue<int, int> dirSizes)
    {
        int dirSize = dir.files.Sum(x => x.size);
        foreach (Directory childDir in dir.directories.Values)
        {
            dirSize += dfs(childDir, dirSizes);
        }

        dirSizes.Enqueue(dirSize, dirSize);

        return dirSize;
    }

    private class Directory
    {
        public string name;
        public IList<File> files;
        public IDictionary<string, Directory> directories;
        public Directory? parent;

        public Directory(string name, Directory? parent)
        {
            this.name = name;
            this.files = new List<File>();
            this.directories = new Dictionary<string, Directory>();
            this.parent = parent;
        }
    }

    private class File
    {
        public string name;
        public int size;

        public File(string name, int size)
        {
            this.name = name;
            this.size = size;
        }
    }
}
