using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.DirectoryServices;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farm_Group_Project
{
    public static class ResourceManager
    {
        public static string ResourceDirectory = "";
        static int max_attempts = 5;
        static readonly string APPLICATION_RESOURCES = "ApplicationResources";

        public static string? GetImageDirectory(string imageName)
        {
            if (!Path.Exists(ResourceDirectory)) SetResourceDirectory();
            if (!Path.Exists($"{ResourceDirectory}{imageName}")) return null;
            return $"{ResourceDirectory}{imageName}";
        }

        static bool DirectoryContains(string folderToSearch, string folderToFind)
        {
            foreach(var entry in Directory.GetFileSystemEntries(folderToSearch, "*", SearchOption.TopDirectoryOnly))
            {
                if (System.IO.Path.GetFileName(entry) == folderToFind) return true;
            }
            return false;
        }

        public static void SetResourceDirectory()
        {
            // Attempts to locate the resource folder starting from the folder the application runs in propogating upwards.
            ResourceDirectory = AppContext.BaseDirectory;

            while (!DirectoryContains(ResourceDirectory, APPLICATION_RESOURCES))
            {
                ResourceDirectory = Directory.GetParent(ResourceDirectory).FullName;
                max_attempts--;
                if (max_attempts == 0)
                {
                    throw new Exception($"Unable to find folder containing '{APPLICATION_RESOURCES}'.n" +
                        $"Stopped at '{ResourceDirectory}'");
                }
            }

            ResourceDirectory += $"\\{APPLICATION_RESOURCES}\\";

            Debug.WriteLine($"Found resources: \n{ResourceDirectory}");
        }
    }
}
