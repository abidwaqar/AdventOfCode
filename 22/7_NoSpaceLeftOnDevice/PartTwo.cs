using System.Drawing;

namespace AdventOfCode._22._7_NoSpaceLeftOnDevice;

internal static class PartTwo
{
    // T = O(d + f) | S = O(d + f + log(d))
    // Where d and f are counts of directories and files respectively
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

        int spaceToFreeUp = 30000000 - (70000000 - directorySize(rootDirectory));
        return sizeOfDirectoryToBeDelete(rootDirectory, spaceToFreeUp).sizeOfDirectoryToBeDeleted;
    }

    private static int directorySize(Directory dir)
    {
        int dirSize = dir.files.Sum(x => x.size);
        foreach (Directory childDir in dir.directories.Values)
        {
            dirSize += directorySize(childDir);
        }

        return dirSize;
    }

    private static Result sizeOfDirectoryToBeDelete(Directory dir, int spaceToFreeUp)
    {
        Result result = new Result(dir.files.Sum(x => x.size), -1);
        foreach (Directory childDir in dir.directories.Values)
        {
            Result childResult = sizeOfDirectoryToBeDelete(childDir, spaceToFreeUp);
            result.size += childResult.size;
            if (childResult.sizeOfDirectoryToBeDeleted != -1)
            {
                result.sizeOfDirectoryToBeDeleted = result.sizeOfDirectoryToBeDeleted == -1 ? childResult.sizeOfDirectoryToBeDeleted : Math.Min(result.sizeOfDirectoryToBeDeleted, childResult.sizeOfDirectoryToBeDeleted);
            }
        }

        if (result.sizeOfDirectoryToBeDeleted == -1 && spaceToFreeUp <= result.size)
        {
            result.sizeOfDirectoryToBeDeleted = result.size;
        }

        return result;
    }

    private class Result
    {
        public int size;
        public int sizeOfDirectoryToBeDeleted;

        public Result(int size, int sizeOfDirectoryToBeDeleted)
        {
            this.size = size;
            this.sizeOfDirectoryToBeDeleted = sizeOfDirectoryToBeDeleted;
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
