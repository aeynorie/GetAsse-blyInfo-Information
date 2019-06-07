using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GetAssemblyVersion
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] arg = Environment.GetCommandLineArgs();
            string targetPath = GetVertsionFilesPath(arg[1]);
            FileRead(SearchTargetFiles(targetPath));
            //var assemblyName = Assembly.GetExecutingAssembly().GetName();
            //var name = assemblyName.Name;    // "アセンブリ名"
            //var version = assemblyName.Version; // "1.0.0.0"
            //Console.WriteLine(name);
            //Console.WriteLine($"{name},{version}です");
        }
        static string GetVertsionFilesPath(string path)
        {
            return path;
        }

        static List<FileInfo> SearchTargetFiles(string path)
        {
            var info = new DirectoryInfo(path);
            var fileList = new List<FileInfo>();

            foreach(var file in info.EnumerateFiles("AssemblyInfo.cs", SearchOption.AllDirectories))
            {
                fileList.Add(file);
            }
            return fileList;
            //var targetInfo = info.EnumerateFiles("*.dll").Concat(info.EnumerateFiles("*.exe")).ToList();
        }
         
        static void FileRead(List<FileInfo> files)
        {
            for(int i=0; i<files.Count; i++)
            {
                using (StreamReader sr = new StreamReader(files[i].FullName))
                {
                    string content = sr.ReadToEnd();
                    Console.WriteLine(content);
                }
            }
        }
    }
}
