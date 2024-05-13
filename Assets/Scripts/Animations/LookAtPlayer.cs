using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    public GameObject player;
    public Vector3 basevector3;
    private void LateUpdate()
    {
        // Если targetObject существует
        if (player != null)
        {
            // Вычисляем вектор, направленный от текущего объекта к targetObject
            Vector3 direction = player.transform.position - transform.position;

            // Вычисляем угол поворота вокруг каждой из осей
            float yAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float xAngle = -Mathf.Atan2(direction.y, Mathf.Sqrt(direction.x * direction.x + direction.z * direction.z)) * Mathf.Rad2Deg;

            // Применяем поворот к текущему объекту
            transform.rotation = Quaternion.Euler(0, yAngle + basevector3.y, 0f);
        }
    }
}
