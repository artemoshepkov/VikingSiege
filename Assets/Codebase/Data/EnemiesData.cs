using System;
using UnityEngine;

namespace Codebase.Data
{
    [Serializable]
    public class EnemiesData
    {
        [SerializeField] private int _amountKilled = 0;

        public int AmountKilled
        {
            get => _amountKilled;
            set
            {
                _amountKilled = value;
                Changed?.Invoke();
            }
        }

        public event Action Changed;
    }
}