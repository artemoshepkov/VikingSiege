using Codebase.Data;
using Codebase.Logic.Stats;
using Codebase.Logic.Stats.StatsShop;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Codebase.UI
{
    public class UpgradeStatButton : MonoBehaviour
    {
        private IStat _stat;
        private IStatsShop _shop;
        private LootData _lootData;
        
        private Color _upgradableButtonColor = Color.cyan;
        private Color _unUpgradeableButtonColor = Color.gray;

        [SerializeField] private Button _button;
        [SerializeField] private StatsId _statId;
        [SerializeField] private TextMeshProUGUI _price;
        [SerializeField] private TextMeshProUGUI _value;
        [SerializeField] private Image _imageButton;

        public void Construct(IStatsShop shop, LootData lootData)
        {
            _lootData = lootData;
            _shop = shop;
            _stat = _shop.GetStatInfo(_statId);
            
            _lootData.Collected += UpdateButtonView;
            UpdateButtonView();
        }

        private void Start()
        {
            _button.onClick.AddListener(() => TryUpgrade());
            UpdateButtonInfo();
        }

        private void TryUpgrade()
        {
            if (_shop.TryUpgrade(_statId))
            {
                UpdateButtonInfo();
            }
        }

        private void UpdateButtonView()
        {
            if (_shop.CanUpgrade(_stat))
            {
                _imageButton.color = _upgradableButtonColor;
                return;
            }

            _imageButton.color = _unUpgradeableButtonColor;
        }

        private void UpdateButtonInfo()
        {
            _price.text = _stat.Price.ToString();
            _value.text = _stat.Value.ToString();
        }
    }
}