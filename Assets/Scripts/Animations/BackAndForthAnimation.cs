using System.Collections;

using UnityEngine;

public class BackAndForthAnimation : MonoBehaviour
{
    [SerializeField] private float _scaleSpeed = 1f;
    [SerializeField] private float _minScale = 1f;
    [SerializeField] private float _maxScale = 1.2f;

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
            while (transform.localScale.x < _maxScale)
            {
                transform.localScale += Vector3.one * (_scaleSpeed * Time.deltaTime);
                yield return null;
            }

            while (transform.localScale.x > _minScale)
            {
                transform.localScale -= Vector3.one * (_scaleSpeed * Time.deltaTime);
                yield return null;
            }
        }
    }
}
