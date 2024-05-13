using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[System.Serializable]
public class TimePoint
{
    public TimePoint()
    {

    }
    public TimePoint(int hour, int minutes)
    {
        Hour = hour;
        Minutes = minutes;
    }

    public int Hour;
    public int Minutes;

    public static bool InTimeline(TimePoint startTimePoint, TimePoint endTimePoint)
    {
        var now = DayAndNightControl.Now;
        bool result = now.Hour >= startTimePoint.Hour &&
                        now.Hour < endTimePoint.Hour;

        //TODO обработать временную точку с минутами
        return result;
    }
}