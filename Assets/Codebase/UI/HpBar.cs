using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Codebase.UI
{
    public class HpBar : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private TextMeshProUGUI _text;

        public void SetValue(float current, float max)
        {
            _image.fillAmount = current / max;
            _text.text = (int) current + " / " + (int) max;
        }
    }
}