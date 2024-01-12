using TMPro;
using UnityEngine;

namespace Codebase.UI
{
    public abstract class UICounter : MonoBehaviour
    {
        [SerializeField] protected TextMeshProUGUI _lootText;

        private void Start()
        {
            OnStart();
        }

        protected virtual void OnStart()
        {
            UpdateLootText();
        }

        protected abstract void UpdateLootText();
    }
}