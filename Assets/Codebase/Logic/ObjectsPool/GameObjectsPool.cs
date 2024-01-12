using System;
using UnityEngine;

namespace Codebase.Logic.ObjectsPool
{
    public class GameObjectsPool : PoolBase<GameObject>
    {
        public GameObjectsPool(Func<GameObject> preloadFunc, int preloadCount) : base(preloadFunc, GetAction,
            ReturnAction, preloadCount)
        {
        }

        public static void GetAction(GameObject @object) => @object.SetActive(true);

        public static void ReturnAction(GameObject @object) => @object.SetActive(false);
    }
}