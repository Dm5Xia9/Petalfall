using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface ITimetableObject
{
    TimePoint StartTimePoint { get; }
    TimePoint EndTimePoint { get; }
}
