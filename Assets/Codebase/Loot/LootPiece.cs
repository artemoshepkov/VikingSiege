using System.Collections;
using Codebase.Data;
using UnityEngine;

namespace Codebase.Loot
{
    public class LootPiece : MonoBehaviour
    {
        private bool _collected;
        private int _value;
        private LootData _lootData;
        private WaitForSeconds _waitTimeToPickUp;

        [SerializeField] private float _timeToPickUp;
        
        public void Construct(LootData lootData)
        {
            _lootData = lootData;
        }
        
        public void Initialize(int value)
        {
            _value = value;
        }

        private void Start()
        {
            _waitTimeToPickUp = new WaitForSeconds(_timeToPickUp);
            StartCoroutine(BeginToPickUping());
        }

        private IEnumerator BeginToPickUping()
        {
            yield return _waitTimeToPickUp;
            
            PickUp();
        }

        private void PickUp()
        {
            if (_collected)
                return;

            _collected = true;
            
            _lootData.Collect(_value);
            Destroy(gameObject);
        }
    }
}