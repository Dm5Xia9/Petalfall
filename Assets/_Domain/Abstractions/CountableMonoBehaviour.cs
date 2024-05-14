using UnityEngine;

public abstract class CountableMonoBehaviour<TEntity, TMono> : EntityMonoBehaviour<TEntity, TMono>
    where TEntity : CountableEntity<TEntity, TMono>
    where TMono : CountableMonoBehaviour<TEntity, TMono>
{
    [SerializeField] private int _count;
    [SerializeField] private int _capacity = int.MaxValue;

    public virtual int Count
    {
        get { return _count; }
        set { _count = value; }
    }

    public virtual int Capacity
    {
        get { return _capacity; }
        set { _capacity = value; }
    }
}