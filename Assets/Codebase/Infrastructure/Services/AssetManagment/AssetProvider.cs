using UnityEngine;

namespace Codebase.Infrastructure.Services.AssetManagment
{
    internal class AssetProvider : IAsset
    {
        public GameObject Instantiate(string path)
        {
            Object prefab = Resources.Load(path);
            return Object.Instantiate(prefab) as GameObject;
        }

        public GameObject Instantiate(string path, Vector3 at)
        {
            Object prefab = Resources.Load(path);
            return Object.Instantiate(prefab, at, Quaternion.identity) as GameObject;
        }
    }
}