using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static UnityEditor.Experimental.GraphView.Port;
using static UnityEngine.EventSystems.EventTrigger;

public abstract class CountableEntity<TEntity, TMono> : Entity<TEntity, TMono>, ICountableEntity where TMono : CountableMonoBehaviour<TEntity, TMono>
       where TEntity : CountableEntity<TEntity, TMono>
{
    protected CountableEntity(TMono gameObject) : base(gameObject)
    {

    }

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
        return Count > 0;
    }

}
