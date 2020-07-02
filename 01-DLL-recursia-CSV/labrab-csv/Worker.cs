using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CsvHelper;

namespace labrabcsv
{
    public class Language
    {
        public string Name { get; set; }
        public string Mask { get; set; }
    }
    public static class GetFiles
    {
        public static List<string> lstAllFiles = new List<string>();

        public static void GetAllFiles(string dir, string ext)
        {
            foreach (var nameFile in Directory.GetFiles(dir))
            {
                if (Path.GetExtension(nameFile) == ext)
                    lstAllFiles.Add(nameFile);
            }
            foreach (var item in Directory.GetDirectories(dir))
            {
                GetAllFiles(item, ext);
            }
        }
    }
    public class Worker
    {
        public static void ChangeOneFileCsv(string nameFile, string par = "dec")
        {
            List<Language> lst = new List<Language>();
            #region CsvHelper
            using (StreamReader sr = new StreamReader(nameFile))
            {
                using (CsvReader cr = new CsvReader(sr, CultureInfo.CurrentCulture))
                {
                    IEnumerable getEnum = cr.GetRecords<Language>();
                    IEnumerator getIn = getEnum.GetEnumerator();
                    while (getIn.MoveNext())
                    {
                        Language item = (Language)getIn.Current;
                        if (par == "dec")
                            item.Mask = new ConvertDll.Convert(item.Mask).Num_dec.ToString();
                        else
                            item.Mask = new ConvertDll.Convert(Convert.ToInt32(item.Mask)).Num_bin;
                        lst.Add(item);
                    }
                }
            }
            #endregion

            #region MyRegion
            using (StreamWriter sw = new StreamWriter(nameFile))
            {
                using (CsvWriter cw = new CsvWriter(sw, CultureInfo.CurrentCulture))
                {
                    cw.Configuration.NewLine = CsvHelper.Configuration.NewLine.LF;
                    cw.WriteHeader<Language>();
                    cw.NextRecord();
                    foreach (var item in lst)
                    {
                        cw.WriteRecord(item);
                        cw.NextRecord();
                    }
                }
            }
            #endregion
        }
    }
}
