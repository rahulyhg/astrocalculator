using astrocalc.api.models;
using astrocalc.api.Models;
using astrocalc.api.Repos;
using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Globalization;
using astrocalc.api.services.usnautical;
using Microsoft.AspNet.Cors.Core;

namespace astrocalc.api.Controllers {
    [Route("sunrises")]
    [EnableCors("opentopublic")]
    public class SunrisesController : Controller {
        protected Repo _repo = new Repo();
        [Route("")]
        [HttpGet]
        public ObjectResult Index() {
            var result = new SolarConfig() {
                month = DateTime.Today.Month,
                year = DateTime.Today.Year,
                citychoices = _repo
                .QueryInterface<ICity>().Index().ToList<City>()
            };
            return new ObjectResult(result);
        }
        [HttpPost]
        [Route("")]
        public ObjectResult SunriseForMonth([FromBody]SolarConfig config) {
            List<SolarDetail> details = new List<SolarDetail>();
            for (int i = 1; i < DateTime.DaysInMonth(config.year, config.month); i++) {
                DateTime dt = new DateTime(config.year, config.month, i, 0, 0, 0); // this is the date to start with
                DateTime dtSunrise= dt.LocalSunrise(config.city.longitude, config.city.latitude, config.zenith);
                details.Add(new SolarDetail() {
                    sunrise = dtSunrise,
                    declination = ServiceExtensions.SolarDeclination(dt.TrueSolarLongitude(config.city.longitude)),
                    julianday = dt.JulianDay(),
                    gregorianday = dt
                }); 
            }
            return new ObjectResult(details);
        }
    }
}
