using CopernicusOpenCSharp.Models;
using System.Threading.Tasks;

namespace CopernicusOpenCSharp.Interfaces
{
    public interface IAccess
    {
       
        Task<string> GetDataAsync(Entites options = Entites.Products, Format format = Format.json, string id = null);
        
        Task<bool>   DownloadAllData(string path, Entites options = Entites.Products, string id = null);
    }
}
