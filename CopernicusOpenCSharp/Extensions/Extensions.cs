//MIT License

//Copyright(c) 2018 Pedro Henrique de Souza Jesus

//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:

//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.

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
