using Codebase.Logic;
using UnityEngine;

namespace Codebase.UI
{
    public class ActorUI : MonoBehaviour
    {
        private IHealth _health;

        [SerializeField] private HpBar _hpBar;

        public void Construct(IHealth health)
        {
            _health = health;
            _health.HealthChanged += UpdateHpBar;
        }

        private void Start() => UpdateHpBar();

        private void OnDestroy()
        {
            if (_health != null)
            {
                _health.HealthChanged -= UpdateHpBar;
            }
        }

        private void UpdateHpBar() => _hpBar.SetValue(_health.CurrentHp, _health.MaxHp);
    }
}