using CopernicusOpenCSharp.Models;
using System.Threading.Tasks;

namespace CopernicusOpenCSharp.Interfaces
{
    public interface IAccess
    {
       
        Task<string> GetDataAsync(Entites options = Entites.Products, Format format = Format.json, string id = null);
        
        Task<bool> DownloadData(string path, string id = null, Entites options = Entites.Products);

    }
}
