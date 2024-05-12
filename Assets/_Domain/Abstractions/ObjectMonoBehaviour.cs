using UnityEngine;

public abstract class ObjectMonoBehaviour<TEntity, TMono> : EntityMonoBehaviour<TEntity, TMono>, ITimetableObject
    where TEntity : IEntity
    where TMono : IEntityMonoBehaviour
{

    [SerializeField] private TimePoint _startTimePoint;
    [SerializeField] private TimePoint _endTimePoint;

    public TimePoint StartTimePoint => _startTimePoint;
    public TimePoint EndTimePoint => _endTimePoint;

    public bool InTimeline()
    {
        return TimePoint.InTimeline(_startTimePoint, _endTimePoint);
    }

}
