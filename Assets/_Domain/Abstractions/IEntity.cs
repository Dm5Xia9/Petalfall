using System;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public interface IEntity
{
    public Guid Id { get; }
    public string Title { get; }
    public bool IsDeleted { get; set; }
    public string ActionMessage { get; }
    public bool CanUse(IEntity target);
    public bool IsItem { get; } 
    public bool CanCleaned { get; }
    public void Use(IEntity target);
    public IEntityMonoBehaviour Unity { get; }

}