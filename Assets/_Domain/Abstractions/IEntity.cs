using System;

public interface IEntity
{
    public Guid Id { get; }
    public string Title { get; }
    public bool IsDeleted { get; set; }
    public string ActionMessage { get; }
    public bool IsItem { get; }
    public bool CanCleaned { get; }

    public IEntityMonoBehaviour Unity { get; }

    public bool CanUse(IEntity target);
    public void Use(IEntity target);

}