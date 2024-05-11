using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    public GameObject player;
    public Vector3 basevector3;
    private void LateUpdate()
    {
        // ���� targetObject ����������
        if (player != null)
        {
            // ��������� ������, ������������ �� �������� ������� � targetObject
            Vector3 direction = player.transform.position - transform.position;

            // ��������� ���� �������� ������ ������ �� ����
            float yAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float xAngle = -Mathf.Atan2(direction.y, Mathf.Sqrt(direction.x * direction.x + direction.z * direction.z)) * Mathf.Rad2Deg;

            // ��������� ������� � �������� �������
            transform.rotation = Quaternion.Euler(0, yAngle + basevector3.y, 0f);
        }
    }
}
