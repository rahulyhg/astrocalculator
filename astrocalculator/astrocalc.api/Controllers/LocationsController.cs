using astrocalc.app.repos;
using astrocalc.app.storemodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace astrocalc.api.Controllers
{
    [RoutePrefix("locations")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class LocationsController : AnyController
    {
        [HttpGet]
        [Route("locations/cities/{id}")]
        public async Task<IHttpActionResult> Cities(string id) {
            ICity qi = _repo.QueryInterface<ICity>();
            return Ok<City>(await qi.OfId(id));
        }
        [HttpGet]
        [Route("locations/states")]
        public async Task<IHttpActionResult> States() {
            ICity qi = _repo.QueryInterface<ICity>();
            return Ok<List<string>>(await qi.States());
        }
        [HttpPost]
        [Route("locations/cities")]
        public async Task<IHttpActionResult> Cities(City newLocation) {
            ICity qi = _repo.QueryInterface<ICity>();
            return Ok<City>(await qi.Create(newLocation));
        }
        [HttpGet]
        [Route("locations/likely/{phrase}")]
        public async Task<IHttpActionResult> LocationsLike(string phrase) {
            ICity qi = _repo.QueryInterface<ICity>();
            return Ok<List<City>>((await qi.Likely(phrase)).ToList<City>());
        }
    }
}
