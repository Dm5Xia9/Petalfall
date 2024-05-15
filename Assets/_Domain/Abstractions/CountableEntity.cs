public abstract class CountableEntity<TEntity, TMono> : Entity<TEntity, TMono>, ICountableEntity
    where TEntity : CountableEntity<TEntity, TMono>
    where TMono : CountableMonoBehaviour<TEntity, TMono>
{
    protected CountableEntity(TMono gameObject) : base(gameObject)
    { }

    public override bool CanCleaned => Count <= 0;

    public virtual int Count
    {
        get { return Unity.Count; }
        set { Unity.Count = value; }
    }

    public virtual int Capacity
    {
        get { return Unity.Capacity; }
        set { Unity.Capacity = value; }
    }

    public override bool CanUse(IEntity target)
    {
        return base.CanUse(target) || Count > 0;
    }

}
