namespace AdventOfCode._22._7_NoSpaceLeftOnDevice;

internal static class PartOne
{
    // T = O(d + f) | S = O(d + f + log(d))
    // Where d is the count of directories and f is the count of files
    public static int Solve()
    {
        Directory rootDirectory = new Directory("/", null);
        Directory currentDirectory = rootDirectory;
        foreach (var input in System.IO.File.ReadAllLines("../../../Input/prod.txt"))
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
            } else if (input.StartsWith("$ cd "))
            {
                string nextDirectoryName = input.Split("$ cd ", StringSplitOptions.RemoveEmptyEntries).Single();
                if (nextDirectoryName == "..")
                {
                    if (currentDirectory.parent != null)
                    {
                        currentDirectory = currentDirectory.parent;
                    }
                } else
                {
                    currentDirectory = currentDirectory.directories[nextDirectoryName];
                }
            } else
            {
                string[] fileInfo = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                currentDirectory.files.Add(new File(fileInfo[1], Convert.ToInt32(fileInfo[0])));
            }
        }

        return sumOfTotalSizes(rootDirectory, 100000).sumOfTotalSizes;
    }
    
    private static Result sumOfTotalSizes(Directory dir, int upperLimit)
    {
        Result dirResult = new Result(dir.files.Sum(x => x.size), 0);
        foreach (Directory childDir in dir.directories.Values)
        {
            Result childResult = sumOfTotalSizes(childDir, upperLimit);
            dirResult.size += childResult.size;
            dirResult.sumOfTotalSizes += childResult.sumOfTotalSizes;
        }

        if (dirResult.size <= upperLimit)
        {
            dirResult.sumOfTotalSizes += dirResult.size;
        }

        return dirResult;
    }

    private class Result
    {
        public int size;
        public int sumOfTotalSizes;

        public Result(int size, int sumOfTotalSizes)
        {
            this.size = size;
            this.sumOfTotalSizes = sumOfTotalSizes;
        }
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
