using System;
using System.Collections.Generic;
using System.Web.Mvc;
using InverGrove.Domain.Interfaces;
using InverGrove.Domain.ViewModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

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
        [HttpGet]
        public ActionResult ManageAttendance()
        {
            var membersForAttendance = this.attendanceService.GetMembersForAttendance();

            return View("_ManageAttendance", (object)JsonConvert.SerializeObject(membersForAttendance, 
                new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }));
        }

        [HttpPost]
        public JsonResult Add(List<AttendancePerson> attendancePersons)
        {
            if (attendancePersons == null)
            {
                return Json(new { success = false, errorMessage = "No people were selected" });
            }
            
            try
            {
                var success = this.attendanceService.AddAttendance(attendancePersons);

                return success ? Json(new { success = true }) : Json(new { success = false, errorMessage = "Error occurred when attempting to update attendance records." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = ex.Message });
            } 
        }
    }
}