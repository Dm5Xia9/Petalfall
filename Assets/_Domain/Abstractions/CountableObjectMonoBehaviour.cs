using UnityEngine;

public abstract class CountableObjectMonoBehaviour<TEntity, TMono> : CountableMonoBehaviour<TEntity, TMono>, ITimetableObject
    where TEntity : CountableEntity<TEntity, TMono>
    where TMono : CountableMonoBehaviour<TEntity, TMono>
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