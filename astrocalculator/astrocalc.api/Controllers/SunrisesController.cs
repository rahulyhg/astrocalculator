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

namespace astrocalc.api.Controllers {
    [Route("sunrises")]
    public class SunrisesController : Controller {
        protected Repo _repo = new Repo();
        [Route("")]
        [HttpGet]
        public ObjectResult Index() {
            var result = new SolarConfig() {
                Month = DateTime.Today.Month,
                Year = DateTime.Today.Year,
                CityChoices = _repo
                .QueryInterface<ICity>().Index().ToList<City>()
            };
            return new ObjectResult(result);
        }
        [HttpPost]
        [Route("")]
        public ObjectResult SunriseForMonth([FromBody]SolarConfig config) {
            DateTime dt = new DateTime(config.Year, config.Month, 1, 6, 17, 0);
            SolarDetail sd = new SolarDetail() {
                Declination = 9.5,
                JulianDay = 117,
                Sunrise = dt,
                Sunset = dt.AddHours(12),
            };
            List<SolarDetail> details = new List<SolarDetail>() { sd};
            for (int i = 2; i <DateTime.DaysInMonth(dt.Year, dt.Month) ; i++) {
                details.Add(new SolarDetail() {
                    Declination = sd.Declination,
                    JulianDay = sd.JulianDay+1,
                    Sunrise = sd.Sunrise.AddDays(1),
                    Sunset = sd.Sunrise.AddDays(1).AddHours(12)
                });
            }
            return new ObjectResult(details);
        }
    }
}
