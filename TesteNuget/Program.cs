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

using System;
using System.Diagnostics;
using System.Threading.Tasks;
using CopernicusOpenCSharp;
using CopernicusOpenCSharp.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TesteNuget
{
    class Program
    {
        static Progress<double> MyProgress = new Progress<double>();
        static double old = 0;
        static async Task Main(string[] args)
        {
            Console.WriteLine("Liguei");
            await Teste();
            Console.WriteLine("Fim");
            Console.ReadKey();
        }

        public static async Task Teste()
        {
            
            MyProgress.ProgressChanged += (sender, value) =>
            {
                value = Math.Round(value, 2);

                if (value != old)
                {
                    Console.WriteLine($"{value} %");
                }

                old = value;
            };

            CopernicusService service = new CopernicusService("userName", "password");
            string id  = "'fea3cd38-918d-4974-8586-2578cbb07844'";
            var teste  = await service.GetDataAsync(id: id);
            var test2  = teste.ExtractJsonId();
            string url = $"https://scihub.copernicus.eu/dhus/odata/v1/Products({id})/$value";
            Console.WriteLine($"\n\n\n {url}");            
            var seila  = await service.DownloadMetaDataAsync(@"C:\Users\pedro\Desktop\Teste", MyProgress, id: id);
        }
    }
}