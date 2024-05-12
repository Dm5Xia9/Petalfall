using UnityEngine;

public interface IEntityMonoBehaviour
{
    public IEntity Entity { get;  }
    public GameObject GameObject { get; }
    public bool CanTargetEvent();
    public void HiddenPlaceholder();
    public void VisablePlaceholder();

    public void ToHandPosition();
}