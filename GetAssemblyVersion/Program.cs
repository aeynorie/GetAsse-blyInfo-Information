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
            string format;
            using (var sw = new StreamWriter(@"C:\Data\test1.csv", false, System.Text.Encoding.Default))
            {
                for (int i = 0; i < files.Count; i++)
                {
                    //一行ずつ読みとった情報を格納
                    string content = "";
                    //読み込むファイルの絶対パス
                    string filePath = files[i].FullName;

                    using (StreamReader sr = new StreamReader(files[i].FullName))
                    {
                        sw.WriteLine(filePath);
                        while ((content = sr.ReadLine()) != null)
                        {
                            //string pattern = ("[assembly:"+ * + "]");
                            if (content.Contains("[assembly: Assembly"))
                            {
                                format = content.Trim().Replace("[", "").Replace("]", "").Replace(": ", "\t");
                                //特定のコメントを削除。未機能。
                                if (content == "// [assembly: AssemblyVersion(\"" + @"1.0.*" + "\"" + ")]")
                                {
                                    format = content.Replace(Environment.NewLine, "");
                                }
                                //指定csvに出力
                                sw.WriteLine(format);
                            }
                        }
                        //1ファイルの読み込みが終わると改行
                        sw.WriteLine("");
                    }
                }
            }
        }

    }
}
