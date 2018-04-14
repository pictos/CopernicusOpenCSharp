using CopernicusOpenCSharp.Models;
using System.Threading.Tasks;

namespace CopernicusOpenCSharp.Interfaces
{
    public interface IAccess
    {
        /// <summary>
        /// Method to get metadata of all resource or specific resource
        /// </summary>
        /// <param name="options">Enumerable of possible resources</param>
        /// <param name="format">Enumerable of possible formats to get data</param>
        /// <param name="id">id for get a specific product</param>
        /// <returns></returns>
        Task<string> GetDataAsync(Entites options = Entites.Products, Format format = Format.json, string id = null);
        

        /// <summary>
        /// Method to download and save the metadata into a file.
        /// </summary>
        /// <param name="path">Destination of downloaded file</param>
        /// <param name="options">Resource to download</param>
        /// <param name="id">Id of resource</param>
        /// <returns></returns>
        Task<bool>   DownloadAllData(string path, Entites options = Entites.Products, string id = null);
    }
}
