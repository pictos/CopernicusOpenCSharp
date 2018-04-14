using CopernicusOpenCSharp.Extensions;
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
        private readonly string user = string.Empty;
        private readonly string password = string.Empty;
        private HttpClient Client { get; set; }

        public CopernicusService(string login, string pass)
        {
            user = login;
            password = pass;
            var credentials = new NetworkCredential(user, password);
            var handler = new HttpClientHandler { Credentials = credentials };
            Client = new HttpClient(handler);
        }

        #region Interface

        public Task<bool> DownloadAllData(string path, Entites opcoes = Entites.Products, string id = null)
        {
            throw new NotImplementedException("This feature is not implemented");
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