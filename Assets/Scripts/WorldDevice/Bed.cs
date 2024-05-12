using System;
using System.Collections;

using StarterAssets;

using UnityEngine;

namespace Assets.Scripts.WorldDevice
{
    public class Bed : BaseWorldDevice
    {
        [SerializeField] private WorldGenerator _worldGenerator;

        [SerializeField] private CanvasGroup _blackCanvas;
        [SerializeField] private float _fadeDuration;

        [SerializeField] private int _wakeUpTime;
        [SerializeField] private float _speed;

        public override string Message => "Лечь спать";

        protected override void OnActive()
        {
            StartCoroutine(Timer());
            _worldGenerator.Generate();
        }

        private IEnumerator Timer()
        {
            StartCoroutine(EyeClosing());
            DateTime time = DayAndNightControl.Now;
            DateTime awakeTime = new(time.Year, time.Month, time.Day + (time.Hour >= 6 ? 1 : 0), _wakeUpTime, 0, 0);
            ThirdPersonController.Instance.playerInput.enabled = false;
            while (Check(awakeTime))
            {
                DayAndNightControl.Instance.SkipTicks(Time.deltaTime * _speed);
                yield return null;
            }

            StartCoroutine(EyeOpening());
            ThirdPersonController.Instance.playerInput.enabled = true;
            yield return null;
        }

        private IEnumerator EyeClosing()
        {
            float startAlpha = 0.0f, endAlpha = 1.0f;
            float elapsedTime = 0f;
            while (elapsedTime < _fadeDuration)
            {
                _blackCanvas.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / _fadeDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            _blackCanvas.alpha = endAlpha;
        }

        private IEnumerator EyeOpening()
        {
            float startAlpha = 1.0f, endAlpha = 0.0f;
            float elapsedTime = 0f;
            while (elapsedTime < _fadeDuration)
            {
                _blackCanvas.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / _fadeDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            _blackCanvas.alpha = endAlpha;
        }

        private bool Check(DateTime maxDt)
        {
            try
            {
                return DayAndNightControl.Now <= maxDt;
            }
            catch
            {
                return true;
            }
        }
    }
}