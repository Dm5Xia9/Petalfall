using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class TriggerArea : MonoBehaviour
{
    [SerializeField] private ActivationEvent _currentActive;

    private readonly List<ActivationEvent> _events = new();

    public void AddActivationEvent(ActivationEvent activationEvent)
    {
        _events.Add(activationEvent);
    }

    public void RemoveActivationEvent(ActivationEvent activationEvent)
    {
        _events.Remove(activationEvent);
    }

    public void Toggle()
    {
        ActivationEvent near = FindNearestGameObject();
        if (_currentActive == near)
            return;

        if (_currentActive != null)
            _currentActive.NotActive();

        _currentActive = near;
        if (near != null)
            _currentActive.Active();
    }


    private ActivationEvent FindNearestGameObject()
    {
        ActivationEvent nearestObject = null;
        float nearestDistance = float.MaxValue;

        _events.RemoveAll((x) => x == null);

        foreach (ActivationEvent obj in _events.Where(p => p.TriggerEnable))
        {
            float distance = Vector3.Distance(transform.position, obj.transform.position);
            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestObject = obj;
            }
        }

        return nearestObject;
    }
}
