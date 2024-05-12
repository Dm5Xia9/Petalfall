using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public abstract class Entity<TEntity, TMono> : IEntity
    where TMono : IEntityMonoBehaviour
    where TEntity : IEntity
{
    protected Entity(TMono unity) : this(Guid.NewGuid(), unity) { }
    protected Entity(Guid id, TMono unity)
    {
        Id = id;
        Unity = unity;
    }

    public Guid Id { get; private set; }
    public TMono Unity { get; private set; }
    public abstract bool CanCleaned { get; }
    public abstract string Title { get; }

    public abstract bool CanUse(IEntity target);
    public abstract bool IsItem { get; }

    public virtual string ActionMessage => null;

    IEntityMonoBehaviour IEntity.Unity => Unity;

    public virtual void Use(IEntity target)
    {

    }

}
