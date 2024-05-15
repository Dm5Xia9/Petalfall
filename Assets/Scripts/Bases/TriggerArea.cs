using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TriggerArea : MonoBehaviour
{
    public List<IEntityMonoBehaviour> events = new List<IEntityMonoBehaviour>();
    public IEntityMonoBehaviour CurrentActive;

    public void Toggle()
    {
        IEntityMonoBehaviour near = FindNearestGameObject();
        if (CurrentActive == near)
        {
            return;
        }

        if (CurrentActive != null)
        {
            CurrentActive.HiddenPlaceholder();
        }


        CurrentActive = near;
        if (near != null)
        {
            CurrentActive.VisiblePlaceholder();
        }

    }


    public IEntityMonoBehaviour FindNearestGameObject()
    {
        IEntityMonoBehaviour nearestObject = null;
        float nearestDistance = float.MaxValue;

        events.RemoveAll((x) => x == null || x.IsDestroyed() || x.Entity.IsDeleted);

        // Перебираем все объекты в списке
        foreach (IEntityMonoBehaviour obj in events.Where(p => p.CanTargetEvent()))
        {
            if (Player.Instance.HandEntity != null && Player.Instance.HandEntity == obj.Entity)
            {
                continue;
            }
            // Вычисляем расстояние до каждого объекта
            float distance = Vector3.Distance(transform.position, obj.GameObject.transform.position);

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
