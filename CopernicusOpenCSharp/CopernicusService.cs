﻿using CopernicusOpenCSharp.Extensions;
using CopernicusOpenCSharp.Interfaces;
using CopernicusOpenCSharp.Models;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
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
                string json     = await GetDataAsync(id: id).ConfigureAwait(false);
                var result      = json.ExtractJsonId();
                string url      = result.D.Metadata.MediaSrc;
                string fileName = $"{result.D.Name}.zip";

                using (var response         = await Client.GetAsync(url,HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false))
                using (var streamToReadFrom = await response.Content.ReadAsStreamAsync())
                {
                    string fileToWriteTo = Path.Combine(path, fileName);
                    using (var streamToWriteTo = File.Open(fileToWriteTo,FileMode.Create))
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
                Console.WriteLine(teste);
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