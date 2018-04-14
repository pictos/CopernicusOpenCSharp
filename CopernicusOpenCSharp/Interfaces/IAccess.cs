using CopernicusOpenCSharp.Models;
using System.Threading.Tasks;

namespace CopernicusOpenCSharp.Interfaces
{
    public interface IAccess
    {
        Task<string> GetDataAsync(Entites opcoes = Entites.Products, Format formato = Format.json, string id = null);
        Task<bool>   DownloadData(string id, string path, Entites opcoes = Entites.Products);
        Task<bool>   DownloadAllData(string path, Entites opcoes = Entites.Products, string id = null);
    }
}
