using System;
using UnityEngine;

namespace Codebase.Data
{
    [Serializable]
    public class LootData
    {
        [SerializeField] private int _balance = 50;

        public int Balance
        {
            get => _balance;
            private set
            {
                _balance = value;
                Collected?.Invoke();
            }
        }

        public event Action Collected;

        public void Collect(int value)
        {
            if (value < 0)
                throw new ArgumentException("Collected value must be > than 0");

            Balance += value;
        }

        public void Spend(int value)
        {           
            if (value < 0)
                throw new ArgumentException("Spent value must be > than 0");

            Balance -= value;
        }
    }
}