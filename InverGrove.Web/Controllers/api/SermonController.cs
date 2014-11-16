using System.Collections.Generic;
using System.Web.Http;
using InverGrove.Domain.Interfaces;

namespace InverGrove.Web.Controllers.api
{
    public class SermonController : ApiController
    {
        private readonly ISermonService sermonService;

        public SermonController(ISermonService sermonService)
        {
            this.sermonService = sermonService;
        }

        public IEnumerable<ISermon> Get()
        {
            var sermons = this.sermonService.GetSermons();

            return sermons;
        }

        public ISermon Get(int id)
        {
            var sermon = this.sermonService.GetSermon(id);

            return sermon;
        }
    }
}