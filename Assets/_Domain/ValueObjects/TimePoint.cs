using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public class TimePoint
{
    public TimePoint(int hour, int? minutes = null)
    {
        Hour = hour;
        Minutes = minutes;
    }

    public int Hour { get; private set; }
    public int? Minutes { get; private set; }

    public static bool InTimeline(TimePoint startTimePoint, TimePoint endTimePoint)
    {
        var now = DayAndNightControl.Now;
        bool result = now.Hour >= startTimePoint.Hour &&
                        now.Hour < endTimePoint.Hour;

        //TODO обработать временную точку с минутами
        return result;
    }
}