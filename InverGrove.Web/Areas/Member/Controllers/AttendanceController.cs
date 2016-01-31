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
        public ActionResult Manage()
        {
            var membersForAttendance = this.attendanceService.GetMembersForAttendance();

            return View("_Manage", (object)JsonConvert.SerializeObject(membersForAttendance, 
                new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }));
        }

        [HttpGet]
        public ActionResult Details(DateTime? attendanceDate)
        {
            if(attendanceDate == null)
            {
                this.RedirectToAction("Manage");
            }

            var attendanceDetails = this.attendanceService.GetAttendanceByDate(attendanceDate.Value);

            return View("_Details", (object)JsonConvert.SerializeObject(attendanceDetails, 
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