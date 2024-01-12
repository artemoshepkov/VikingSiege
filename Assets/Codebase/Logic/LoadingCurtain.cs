using System.Collections;
using UnityEngine;

namespace Codebase.Logic
{
    public class LoadingCurtain : MonoBehaviour
    {
        private WaitForSeconds _waitForSeconds;

        [SerializeField] private float _secondForLoading;
        
        public CanvasGroup Curtain;

        private void Awake()
        {
            _waitForSeconds = new WaitForSeconds(_secondForLoading);
            DontDestroyOnLoad(this);
        }

        private void OnValidate()
        {
            if (_secondForLoading < 0)
                _secondForLoading = 0;
        }

        public void Show()
        {
            gameObject.SetActive(true);
            Curtain.alpha = 1;
        }

        public void Hide() => StartCoroutine(DoFaidIn());

        private IEnumerator DoFaidIn()
        {
            while (Curtain.alpha > 0)
            {
                Curtain.alpha -= 0.03f;
                yield return _waitForSeconds;
            }
            
            gameObject.SetActive(false);
        }
    }
}