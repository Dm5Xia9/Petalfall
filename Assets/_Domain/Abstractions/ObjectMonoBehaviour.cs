using UnityEngine;

public abstract class ObjectMonoBehaviour<TEntity, TMono> : EntityMonoBehaviour<TEntity, TMono>, ITimetableObject
    where TEntity : IEntity
    where TMono : IEntityMonoBehaviour
{

    [SerializeField] private TimePoint _startTimePoint;
    [SerializeField] private TimePoint _endTimePoint;
    [SerializeField] private bool _ignoreTimeline;
    public TimePoint StartTimePoint => _startTimePoint;
    public TimePoint EndTimePoint => _endTimePoint;

    public bool InTimeline()
    {
        if (_ignoreTimeline)
        {
            return true;
        }
        return TimePoint.InTimeline(_startTimePoint, _endTimePoint);
    }

}
