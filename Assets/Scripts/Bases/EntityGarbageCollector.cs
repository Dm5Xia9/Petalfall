using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class EntityGarbageCollector : MonoBehaviour
{
    private List<IEntity> _entities = new List<IEntity>();
    public static EntityGarbageCollector Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        _entities.RemoveAll(p => p == null);

        foreach(var entity in _entities.Where(p => p.CanCleaned).ToList())
        {
            if(Player.Instance.HandEntity == entity)
            {
                Player.Instance.DropHandEntity();
            }
            Destroy(entity.Unity.GameObject);
            entity.IsDeleted = true;
            _entities.Remove(entity);
        }
    }

    public static void RegisterEntity(IEntity entity)
    {
        Instance._entities.Add(entity);
    }

}
