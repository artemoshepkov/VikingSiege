using System;
using UnityEngine;

namespace Codebase.Logic
{
    public class DestroyableEntity : MonoBehaviour
    {
        private Action _onDestroy;

        public void Construct(Action onDestroy) => _onDestroy = onDestroy;

        public void Destroy() => _onDestroy.Invoke();
    }
}