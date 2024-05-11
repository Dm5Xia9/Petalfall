using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackAndForthAnimation : MonoBehaviour
{
    public float scaleSpeed = 1f; // �������� ��������
    public float minScale = 1f; // ����������� �������
    public float maxScale = 1.2f; // ������������ �������

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
            // ����������
            while (transform.localScale.x < maxScale)
            {
                transform.localScale += Vector3.one * (scaleSpeed * Time.deltaTime);
                yield return null;
            }

            // ����������
            while (transform.localScale.x > minScale)
            {
                transform.localScale -= Vector3.one * (scaleSpeed * Time.deltaTime);
                yield return null;
            }
        }
    }
}
