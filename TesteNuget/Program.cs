using System;
using System.Threading.Tasks;
using CopernicusOpenCSharp;
using CopernicusOpenCSharp.Extensions;

namespace TesteNuget
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Liguei");
            Task.Run(async () => await Teste()).Wait();
            Console.WriteLine("Fim");
            Console.ReadKey();
        }

        public static async Task Teste()
        {
            CopernicusService service = new CopernicusService("", "");
            string id  = "'fea3cd38-918d-4974-8586-2578cbb07844'";
            var teste  = await service.GetDataAsync(id: id);
            var test2  = teste.ExtractJsonId();
            string url = $"https://scihub.copernicus.eu/dhus/odata/v1/Products({id})/$value";
            Console.WriteLine($"\n\n\n {url}");
            var seila = await service.DownloadData(@"C:\Users\teste\Desktop", id: id);
            Console.WriteLine(teste);
        }
    }
}
