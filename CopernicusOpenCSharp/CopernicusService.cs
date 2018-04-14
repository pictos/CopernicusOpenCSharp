using CopernicusOpenCSharp.Extensions;
using CopernicusOpenCSharp.Interfaces;
using CopernicusOpenCSharp.Models;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace CopernicusOpenCSharp
{
    public class CopernicusService : IAccess
    {
        private readonly string user     = string.Empty;
        private readonly string password = string.Empty;


        public CopernicusService(string login, string pass)
        {
            user     = login;
            password = pass;
        }
       //teste
        #region Interface

        public async Task<bool> DownloadAllData(string path, Entites opcoes = Entites.Products,string id= null)
        {
            try
            {
                var json = await GetDataAsync();
                var result = json.ExtractJson();

                foreach (var item in result.D.Results)
                {
                    string fileName = item.Name + ".zip";
                    path = Path.Combine(path, fileName);
                    var url = item.Metadata.Media_src;
                }
                return true;

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<bool> DownloadData(string id, string path,Entites opcoes = Entites.Products)
        {
            return await DownloadAllData(path, opcoes, id);
        }

        public async Task<string> GetDataAsync(Entites opcoes = Entites.Products, Format formato = Format.json, string id = null)
        {
            try
            {
                HttpWebRequest Client = Login(opcoes, id,formato);

                var resposta = await Client.GetResponseAsync();
                string json;

                using (var sr = new StreamReader(resposta.GetResponseStream()))
                {
                    json = sr.ReadToEnd();
                }

                Console.WriteLine(json);

                return json;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return ex.Message;
            }
        }

        #endregion

        private HttpWebRequest Login(Entites opcoes, string id, Format formato = Format.json)
        {
            string url = string.Empty;

            if (formato == Format.csv)
                url = $"https://scihub.copernicus.eu/dhus/odata/v1/{opcoes}({id})?$format=text/csv";
            else
                url = $"https://scihub.copernicus.eu/dhus/odata/v1/{opcoes}({id})?$format={formato}";


            Console.WriteLine($"url: {url}\n");

            HttpWebRequest Client = (HttpWebRequest)WebRequest.Create(url);
            Client.Method         = "GET";
            Client.Credentials    = new NetworkCredential(user, password);
            Client.UserAgent      = "curl/7.37.0";
            Client.ContentType    = "application/json";
            
            return Client;
        }
    }
}