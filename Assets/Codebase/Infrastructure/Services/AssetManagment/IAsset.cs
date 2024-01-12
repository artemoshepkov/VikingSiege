using UnityEngine;

namespace Codebase.Infrastructure.Services.AssetManagment
{
    public interface IAsset : IService
    {
        GameObject Instantiate(string path);
        GameObject Instantiate(string path, Vector3 at);
    }
}