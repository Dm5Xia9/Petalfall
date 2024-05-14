using UnityEngine;

public static class EntityMonoBehaviourExtensions
{
    public static Transform GetParent(this IEntityMonoBehaviour unity)
    {
        return unity.GameObject.transform.parent;
    }
}
