using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InverGrove.Domain.Extensions;
using InverGrove.Domain.Interfaces;

namespace InverGrove.Web.Areas.Member.Controllers
{
    [Authorize(Roles = "Member")]
    public class AttendanceController : Controller
    {
        private readonly IAttendanceService attendanceService;

        public AttendanceController(IAttendanceService attendanceService)
        {
            this.attendanceService = attendanceService;
        }

        // GET: Member/Attendance
        public ActionResult Index()
        {
            var membersForAttendance = this.attendanceService.GetMembersForAttendance();

            return this.Json(membersForAttendance, JsonRequestBehavior.AllowGet).AsCamelCaseResolverResult();
        }
    }
}