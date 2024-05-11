using UnityEngine;

public class Tree : MonoBehaviour
{
    [SerializeField] private float _minScale = 0.8f;
    [SerializeField] private float _maxScale = 2f;

    private void Start()
    {
        transform.Rotate(0f, Random.Range(-180f, 180f), 0f);
        transform.localScale = transform.localScale * Random.Range(_minScale, _maxScale);
    }
}
