using System;
using UnityEngine;

namespace Codebase.Logic.Waves
{
    [Serializable]
    public class Wave
    {
        [SerializeField] private int _amountEnemies;
        public int AmountEnemies => _amountEnemies;
    }
}