using UnityEngine;

public interface IEntityMonoBehaviour
{
    public IEntity Entity { get; }
    public GameObject GameObject { get; }
    public bool CanTargetEvent();
    public bool IsDestroyed();
    public void HiddenPlaceholder();
    public void VisiblePlaceholder();

    public void ToHandPosition();
}