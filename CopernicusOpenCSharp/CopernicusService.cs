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

using CopernicusOpenCSharp.Extensions;
using CopernicusOpenCSharp.Interfaces;
using CopernicusOpenCSharp.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace CopernicusOpenCSharp
{
    public class CopernicusService : IAccess
    {
        private readonly string user     = string.Empty;
        private readonly string password = string.Empty;
        private HttpClient Client { get; set; }

        public CopernicusService(string login, string pass)
        {
            user            = login;
            password        = pass;
            var credentials = new NetworkCredential(user, password);
            var handler     = new HttpClientHandler { Credentials = credentials };
            Client          = new HttpClient(handler);
        }

        #region Interface

        [Obsolete("This method is deprecated, use the DownloadDataAsync")]
        /// <summary>
        /// Method to download and save the metadata into a file.
        /// </summary>
        /// <param name="path">Destination of downloaded file</param>
        /// <param name="options">Resource to download</param>
        /// <param name="id">Id of resource</param>
        /// <returns></returns>
        public async Task<bool> DownloadData(string path, string id, Entites opcoes = Entites.Products)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new Exception("The Id can't be null!");
            }
            try
            {
                string json = await GetDataAsync(id: id).ConfigureAwait(false);
                var result = json.ExtractJsonId();
                string url = result.D.Metadata.MediaSrc;
                string fileName = $"{result.D.Name}.zip";

                using (var response = await Client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false))
                using (var streamToReadFrom = await response.Content.ReadAsStreamAsync())
                {
                    string fileToWriteTo = Path.Combine(path, fileName);
                    using (var streamToWriteTo = File.Open(fileToWriteTo, FileMode.Create))
                    {
                        await streamToReadFrom.CopyToAsync(streamToWriteTo);
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }            
        }


        /// <summary>
        /// Method to download and save the metadata into a file. With this method you can show the download progress.
        /// </summary>
        /// <param name="path">Destination of downloaded file</param>
        /// <param name="progress">IProgress to hear the download progress</param>
        /// <param name="id">Id of resource</param>
        /// <param name="opcoes">Resource to download</param>
        /// <param name="token">Token Source to cancellation the Task</param>
        /// <returns></returns>
        public async Task<bool> DownloadDataAsync(string path, IProgress<double> progress, string id, Entites opcoes = Entites.Products, CancellationToken token = default(CancellationToken))
        {
            if (string.IsNullOrEmpty(id))
                throw new Exception("The Id can't be null!");
            
            try
            {
                var json     = await GetDataAsync(id: id).ConfigureAwait(false);
                var result      = json.ExtractJsonId();
                var url      = result.D.Metadata.MediaSrc;
                var fileName = $"{result.D.Name}.zip";
                var tamanho = int.Parse(result.D.ContentLength);

                using (var response = await Client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false))
                {
                    if (!response.IsSuccessStatusCode)
                        throw new Exception($"Error in download: {response.StatusCode}");

                    var total = response.Content.Headers.ContentLength ?? -1L;

                    var canReportProgress = total != -1 && progress != null;

                    using (var streamToReadFrom = await response.Content.ReadAsStreamAsync())
                    {

                        var totalRead = 0L;
                        var buffer = new byte[1024];
                        var isMoreToRead = true;
                        var fileToWriteTo = Path.Combine(path, fileName);
                        var output = new FileStream(fileToWriteTo, FileMode.Create);
                        do
                        {
                            token.ThrowIfCancellationRequested();

                            var read = await streamToReadFrom.ReadAsync(buffer, 0, buffer.Length, token);

                            if (read == 0)
                                isMoreToRead = false;
                            else
                            {
                                var data = new byte[read];
                                buffer.ToList().CopyTo(0, data, 0, read);

                                output.Write(buffer, 0, read);

                                totalRead += read;
                                if (canReportProgress)
                                    progress.Report((totalRead * 1d) / (total * 1d) * 100);
                            }

                        } while (isMoreToRead);

                        output.Close();


                        return true;


                        //using (var streamToWriteTo = File.Open(fileToWriteTo, FileMode.Create))
                        //{
                        //    await streamToReadFrom.CopyToAsync(streamToWriteTo);
                        //    streamToReadFrom.CopyTo(streamToWriteTo);
                        //    return true;
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            #region ToDo
            //try
            //{
            //    string json = "";
            //    if (id != null)
            //        json = await GetDataAsync(id: id);
            //    else
            //        json = await GetDataAsync();

            //    var result = json.ExtractJson();

            //    foreach (var item in result.D.Results)
            //    {
            //        string fileName = item.Name + ".zip";
            //        path = Path.Combine(path, fileName);
            //        var url = item.Metadata.Media_src;
            //    }
            //    return true;

            //}
            //catch (Exception ex)
            //{

            //    Console.WriteLine(ex.Message);
            //    return false;
            //}
            #endregion
        }        

        /// <summary>
        /// Method to get metadata of all resource or specific resource
        /// </summary>
        /// <param name="options">Enumerable of possible resources</param>
        /// <param name="format">Enumerable of possible formats to get data</param>
        /// <param name="id">id for get a specific product</param>
        /// <returns></returns>
        public async Task<string> GetDataAsync(Entites opcoes = Entites.Products, Format formato = Format.json, string id = null)
        {
            try
            {
                string url = string.Empty;

                if (formato == Format.csv)
                    url = $"https://scihub.copernicus.eu/dhus/odata/v1/{opcoes}({id})?$format=text/csv";
                else
                    url = $"https://scihub.copernicus.eu/dhus/odata/v1/{opcoes}({id})?$format={formato}";


                Console.WriteLine($"url: {url}\n");
                string teste = await Client.GetStringAsync(url);
                var show = JValue.Parse(teste).ToString(Formatting.Indented);
                Console.WriteLine(show);
                return teste;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return ex.Message;
            }
        }      
        #endregion
    }
}