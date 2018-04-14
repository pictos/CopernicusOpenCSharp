using CopernicusOpenCSharp.Models;
using Newtonsoft.Json;
using System;
using System.IO;

namespace CopernicusOpenCSharp.Extensions
{
    public static class Extensions
    {
        /// <summary>
        /// Method to save MetaData in external file.
        /// </summary>
        /// <param name="data">MetaData</param>
        /// <param name="path">Destination where you save the file</param>
        /// <param name="fileMode">Specifies how  the operating system </param>
        /// <returns>True if success, False if is not</returns>
        public static bool SaveData(this string data,string path, FileMode fileMode = FileMode.Create)
        {
            try
            {
                FileStream fs   = new FileStream(path, fileMode);
                StreamWriter sw = new StreamWriter(fs);

                sw.Write(data);
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

        /// <summary>
        /// Method to return a .Net object from json string. AllData.
        /// </summary>
        /// <param name="json">MetaData returned from GetDataAsync.</param>
        /// <returns>return the .net object for all data.</returns>
        public static Query ExtractJson(this string json) => JsonConvert.DeserializeObject<Query>(json);

        /// <summary>
        /// Method to return a .Net object from json string. Specific data.
        /// </summary>
        /// <param name="json">MetaData returned from GetDataAsync</param>
        /// <returns>return the .net object for all data.</returns>
        public static QueryId ExtractJsonId(this string json) => JsonConvert.DeserializeObject<QueryId>(json);
    }
}
