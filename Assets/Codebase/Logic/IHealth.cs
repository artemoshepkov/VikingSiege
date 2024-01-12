using System;
using Codebase.Logic.Damage;

namespace Codebase.Logic
{
    public interface IHealth : IDamageable
    {
        float CurrentHp { get; set; }
        float MaxHp { get; set; }
        event Action HealthChanged;
    }
}