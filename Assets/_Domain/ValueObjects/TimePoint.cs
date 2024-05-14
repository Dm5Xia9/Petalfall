using System;

[Serializable]
public class TimePoint
{
    public int Hour;
    public int Minutes;

    public TimePoint()
    { }

    public TimePoint(int hour, int minutes)
    {
        Hour = hour;
        Minutes = minutes;
    }

    public static bool InTimeline(TimePoint startTimePoint, TimePoint endTimePoint)
    {
        DateTime now = DayAndNightControl.Now;
        bool result = now.Hour >= startTimePoint.Hour &&
                      now.Hour < endTimePoint.Hour;

        //TODO обработать временную точку с минутами
        return result;
    }
}