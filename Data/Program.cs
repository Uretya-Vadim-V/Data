using System;
using System.IO;

namespace Data
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Tree<int, int, string> tree = new(0);
                using (StreamReader file = new(@"..\..\..\..\\data.txt"))
                {
                    string line;
                    string[] stringData = new string[3];
                    while ((line = file.ReadLine()) != null)
                    {
                        stringData = line.Split(new char[] { ';' });
                        tree.Add(Convert.ToInt32(stringData[0]), Convert.ToInt32(stringData[1]), stringData[2]);
                    }
                }
                tree.BuildTrees();
                foreach (var item in tree)
                {
                    Console.WriteLine(item);
                }
            }
            catch (FileNotFoundException filEx)
            {
                Console.WriteLine($"Файл не найден: {filEx.Message}");
            }
            catch (DirectoryNotFoundException dirEx)
            {
                Console.WriteLine($"Каталог не найден: {dirEx.Message}");
            }
        }
    }
}
