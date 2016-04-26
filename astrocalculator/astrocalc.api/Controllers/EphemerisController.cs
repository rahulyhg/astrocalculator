using astrocalc.app.httpmodels;
using astrocalc.app.repos;
using astrocalc.app.storemodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using astrocalc.app.services.usnautical;

namespace astrocalc.api.Controllers {
    [RoutePrefix("ephemeris")]
    [EnableCors(origins:"*", headers:"*", methods:"*")]
    public class EphemerisController : AnyController
    {
        [HttpGet]
        [Route("range/months")]
        public async Task<IHttpActionResult> RangeOfMonths() {
            IMonth qi = _repo.QueryInterface<IMonth>();
            return Ok<List<Month>>((await qi.Index()).ToList<Month>());
        }
        [HttpGet]
        [Route("range/years")]
        public async Task<IHttpActionResult> RangeOfYears() {
            var years = new List<int>() {};
            for (int i = 1999; i < 2045; i++) {
                years.Add(i);
            }
            return Ok<List<int>>(years);
        }
        [HttpGet]
        [Route("range/zeniths")]
        public async Task<IHttpActionResult> RangeOfZeniths() {
            return Ok<List<Zenith>>((await (_repo.QueryInterface<IZenith>()).Index()).ToList<Zenith>());
        }
        [HttpGet]
        [Route("locations/cities/{id}")]
        public async Task<IHttpActionResult> Cities(string id) {
            ICity qi = _repo.QueryInterface<ICity>();
            return Ok<City>(await qi.OfId(id));
        }
        [HttpGet]
        [Route("locations/likely/{phrase}")]
        public async Task<IHttpActionResult> LocationsLike(string phrase) {
            ICity qi = _repo.QueryInterface<ICity>();
            return Ok<List<City>>((await qi.Likely(phrase)).ToList<City>());
        }
        [HttpGet]
        [Route("solar/{lat}/{lng}/{zen}/{yr:int}/{mn:int}")]
        public async Task<IHttpActionResult> SolarEphemeris(double lat, double lng, double zen, int yr, int mn) {
            if (yr > 0 && mn <= 12 && mn > 0) {
                DateTime startdt = new DateTime(yr, mn, 1);
                DateTime enddt = new DateTime(yr, mn, DateTime.DaysInMonth(yr, mn));
                //it is between these dates that the solar times would have to be calualted 
                DateTime dt = startdt;
                List<SolarTime> solartimes = new List<SolarTime>();
                for (int i = startdt.Day; i <= enddt.Day; i++) {
                    solartimes.Add(new SolarTime() {
                        date = dt,
                        julian = dt.JulianDay(),
                        sunrise = dt.LocalSunrise(lng, lat, zen),
                        declination = dt.SolarDeclination(lng)
                    });
                    dt =dt.AddDays(1);
                }
                return Ok<List<SolarTime>>(solartimes);
            }
            else {
                throw new ArgumentException(String.Format("The requested ephemeris is not in the correct time format"));
            }
        }
    }
}
