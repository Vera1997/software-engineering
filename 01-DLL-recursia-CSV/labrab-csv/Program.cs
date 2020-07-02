using System;
using CsvHelper;
using System.IO;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using ConvertDll;

namespace labrabcsv
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            string cur_dir = AppDomain.CurrentDomain.BaseDirectory;
            GetFiles.GetAllFiles(cur_dir, ".csv");
            foreach (var nameFile in GetFiles.lstAllFiles)
                Worker.ChangeOneFileCsv(nameFile, "bin");
            Console.WriteLine("The end...");
            Console.ReadLine();
        }
    }
}
