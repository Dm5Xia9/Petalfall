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

    public virtual bool CanUse(IEntity target)
    {
        if (IsItem == false)
        {
            return target is Pickaxe pickaxe && pickaxe.CanUse(this);
        }
        return false;
    }
    public abstract bool IsItem { get; }

    public abstract bool CanCleaned { get; }

    IEntityMonoBehaviour IEntity.Unity => Unity;

    public bool IsDeleted { get; set; }

    public virtual void Use(IEntity target)
    {
        if (IsItem == false && target is Pickaxe pickaxe)
        {
            pickaxe.Use(this);
            IsDeleted = true;
            throw new Exception("destroy");
        }
    }
}
