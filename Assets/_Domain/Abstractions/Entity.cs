using System;

public abstract class Entity<TEntity, TMono> : IEntity
    where TEntity : IEntity
    where TMono : IEntityMonoBehaviour
{
    protected Entity(TMono unity) : this(Guid.NewGuid(), unity)
    { }

    protected Entity(Guid id, TMono unity)
    {
        Id = id;
        Unity = unity;

        EntityGarbageCollector.RegisterEntity(this);
    }

    public Guid Id { get; private set; }
    public TMono Unity { get; private set; }

    public abstract string Title { get; }
    public virtual string ActionMessage => null;

    public abstract bool CanUse(IEntity target);
    public abstract bool IsItem { get; }

    public abstract bool CanCleaned { get; }

    IEntityMonoBehaviour IEntity.Unity => Unity;

    public bool IsDeleted { get; set; }

    public virtual void Use(IEntity target)
    { }
}
