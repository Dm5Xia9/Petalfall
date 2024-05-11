using System.Collections;

using StarterAssets;

using UnityEngine;

public class ReturnTeleport : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private GameObject _tpPoint;

    [SerializeField] private float FadeDuration;

    [SerializeField] private ThirdPersonController _person;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "PlayerArmature")
            StartCoroutine(TeleportCoroutine());
    }

    private IEnumerator TeleportCoroutine()
    {
        float elapsedTime = 0f;
        while (elapsedTime < FadeDuration)
        {
            _canvasGroup.alpha = Mathf.Lerp(0f, 1f, elapsedTime / FadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        _canvasGroup.alpha = 1f;
        _person.Teleport(_tpPoint.transform.position);
        DayAndNightControl.Instance.SecondsInAFullDay = 1440;
        DateTimeUI.Instance.isDay = null;

        yield return null;

        elapsedTime = 0f;
        while (elapsedTime < FadeDuration)
        {
            _canvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsedTime / FadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        _canvasGroup.alpha = 0f;
    }
}
