using Codebase.Data;

namespace Codebase.UI
{
    public class KilledEnemiesCounter : UICounter
    {
        private EnemiesData _enemiesData;
    
        public void Construct(EnemiesData enemiesData)
        {
            _enemiesData = enemiesData;
            _enemiesData.Changed += UpdateLootText;
        }
    
        private void OnDestroy() => _enemiesData.Changed -= UpdateLootText;
        protected override void UpdateLootText() => _lootText.text = _enemiesData.AmountKilled.ToString();
    }
}