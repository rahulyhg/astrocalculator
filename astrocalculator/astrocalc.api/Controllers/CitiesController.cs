using astrocalc.app.models;
using astrocalc.app.repos;
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
    [EnableCors(origins:"*", headers:"*", methods:"*")]
    public class CitiesController : ApiController
    {
        protected Repo repo = new Repo();
        [Route("cities/{skip:int}/{top:int}")]
        [HttpGet]
        public async Task<IHttpActionResult> Index(int skip, int top) {
            return Ok<List<City>>((await (repo.QueryInterface<ICity>()).Index(skip, top)).ToList<City>());
        }
        [Route("cities/{id}")]
        [HttpGet]
        public async Task<IHttpActionResult> Index(string id) {
            return Ok<City>(await(repo.QueryInterface<ICity>()).OfId(id));
        }
        [Route("states/{state}/cities")]
        [HttpGet]
        public async Task<IHttpActionResult> CitiesOfState(string state) {
            return Ok<List<City>>(await (repo.QueryInterface<ICity>()).OfState(state));
        }
        [Route("cities/likely/{phrase}")]
        [HttpGet]
        public async Task<IHttpActionResult> CitiesLikely(string phrase) {
            return Ok<List<City>>((await (repo.QueryInterface<ICity>()).Likely(phrase)).ToList<City>());
        }
    }
}
