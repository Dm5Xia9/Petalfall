using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackAndForthAnimation : MonoBehaviour
{
    public float scaleSpeed = 1f; // Скорость анимации
    public float minScale = 1f; // Минимальный масштаб
    public float maxScale = 1.2f; // Максимальный масштаб

    private void Start()
    {
        Restart();
    }

    public void Restart()
    {
        StartCoroutine(ScaleAnimation());

    }
    private IEnumerator ScaleAnimation()
    {
        while (true)
        {
            // Увеличение
            while (transform.localScale.x < maxScale)
            {
                transform.localScale += Vector3.one * (scaleSpeed * Time.deltaTime);
                yield return null;
            }

            // Уменьшение
            while (transform.localScale.x > minScale)
            {
                transform.localScale -= Vector3.one * (scaleSpeed * Time.deltaTime);
                yield return null;
            }
        }
    }
}
