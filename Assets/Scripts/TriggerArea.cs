using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TriggerArea : MonoBehaviour
{
    public List<ActivationEvent> events = new List<ActivationEvent>();
    public ActivationEvent CurrentActive;

    public void Toggle()
    {
        ActivationEvent near = FindNearestGameObject();
        if (CurrentActive == near)
        {
            return;
        }

        if (CurrentActive != null)
        {
            CurrentActive.NotActive();
        }


        CurrentActive = near;
        if (near != null)
        {
            CurrentActive.Active();
        }

    }


    public ActivationEvent FindNearestGameObject()
    {
        ActivationEvent nearestObject = null;
        float nearestDistance = float.MaxValue;

        events.RemoveAll((x) => x == null);

        // Перебираем все объекты в списке
        foreach (ActivationEvent obj in events.Where(p => p.TriggerEnable))
        {
            // Вычисляем расстояние до каждого объекта
            float distance = Vector3.Distance(transform.position, obj.transform.position);

            // Если расстояние меньше, чем до ближайшего, обновляем ближайший объект
            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestObject = obj;
            }
        }

        return nearestObject;
    }
}
