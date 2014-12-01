using InverGrove.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace InverGrove.Web.Controllers.api
{
    public class ProfileController : ApiController
    {
        private readonly IProfileService profileService;

        public ProfileController(IProfileService profileService)
        {
            this.profileService = profileService;
        }

        public IProfile Get(int userId)
        {
            return this.profileService.GetProfile(userId);
        } 
    }
}
