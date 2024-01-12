using Codebase.Data;

namespace Codebase.UI
{
    public class LootCounter : UICounter
    {
        private LootData _lootData;
        
        public void Construct(LootData lootData)
        {
            _lootData = lootData;
            _lootData.Collected += UpdateLootText;
        }

        private void OnDestroy() => _lootData.Collected -= UpdateLootText;

        protected override void UpdateLootText() => _lootText.text = _lootData.Balance.ToString();
    }
}