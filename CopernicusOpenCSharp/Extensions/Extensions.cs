using CopernicusOpenCSharp.Models;
using Newtonsoft.Json;
using System;
using System.IO;

namespace CopernicusOpenCSharp.Extensions
{
    public static class Extensions
    {
        public static bool SalvarDado(this string csv,string path, FileMode fileMode = FileMode.Create)
        {
            try
            {
                FileStream fs   = new FileStream(path, fileMode);
                StreamWriter sw = new StreamWriter(fs);

                sw.Write(csv);
                sw.Flush();
                sw.Close();
                fs.Close();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }            
        }

        public static Query ExtractJson(this string json) => JsonConvert.DeserializeObject<Query>(json);

        public static QueryId ExtractJsonId(this string json) => JsonConvert.DeserializeObject<QueryId>(json);
    }
}
