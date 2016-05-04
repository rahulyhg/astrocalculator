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
using astrocalc.app.services.solar;
using astrocalc.app.services;

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
        [Route("solar/vedic/{lat}/{lng}/{gmtoffset}/{yr:int}/{mn:int}")]
        public async Task<IHttpActionResult> VedicSolarEphemeris(double lat, double lng, double gmtoffset, int yr, int mn) {
            if (yr > 0 && mn <= 12 && mn > 0) {
                List<SolarClock> clocks = new List<SolarClock>(); //this is the output result 
                DateTime startdt = new DateTime(yr, mn, 1, 0, 0, 0); //we start from the first day  in the month requested
                for (int i = 0; i < DateTime.DaysInMonth(yr, mn); i++) {
                    DateTime currDate = startdt.AddDays(i);
                    clocks.Add(Solar.VedicShuddhi(currDate.SolarClock(lat, lng, gmtoffset, false)));
                }
                return Ok<List<SolarClock>>(clocks);
            }
            else {
                throw new ArgumentException(String.Format("The requested ephemeris is not in the correct time format"));
            }
        }
        [HttpGet]
        [Route("solar/{lat}/{lng}/{gmtoffset}/{yr:int}/{mn:int}")]
        public async Task<IHttpActionResult> SolarEphemeris(double lat, double lng, double gmtoffset, int yr, int mn) {
            if (yr > 0 && mn <= 12 && mn > 0) {
                List<SolarClock> clocks = new List<SolarClock>(); //this is the output result 
                DateTime startdt = new DateTime(yr, mn, 1, 0, 0, 0); //we start from the first day  in the month requested
                for (int i = 0; i < DateTime.DaysInMonth(yr, mn); i++) {
                    DateTime currDate = startdt.AddDays(i);
                    clocks.Add(currDate.SolarClock(lat, lng,gmtoffset, true));
                }
                return Ok<List<SolarClock>>(clocks);
            }
            else {
                throw new ArgumentException(String.Format("The requested ephemeris is not in the correct time format"));
            }
        }
    }
}
