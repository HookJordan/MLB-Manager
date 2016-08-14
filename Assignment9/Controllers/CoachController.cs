using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment9.Controllers
{
    public class CoachController : Controller
    {
        private Manager man = new Manager();
        // GET: Coach
        public ActionResult Index()
        {
            return View(man.CoachesGetAll());
        }

        // GET: Coach/Details/5
        public ActionResult Details(int id)
        {
            return View(man.CoachesGetById(id));
        }

        public ActionResult SearchCoach()
        {
            return View(new CoachSearchForm());
        }

        [Route("Coach/Coaches/{searchText}")]
        public ActionResult Coaches(string searchText = "")
        {
            // Fetch matching tracks
            var c = man.CoachGetByName(searchText);

            if (c == null)
            {
                // Empty list
                return PartialView("_CoachList", new List<CoachBase>());
            }
            else
            {
                return PartialView("_CoachList", c);
            }
        }
    }
}
