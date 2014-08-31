using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InverGrove.Domain.Interfaces
{
    public interface IAttendanceService
    {
        IEnumerable<Domain.ViewModels.Attendance> ShowEveryonesAttendance();
    }
}
