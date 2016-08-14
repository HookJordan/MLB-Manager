using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment9.Controllers
{
    public class TeamController : Controller
    {
        private Manager man = new Manager(); 
        // GET: Team
        public ActionResult Index()
        {
            return View(man.TeamsGetAll());
        }

        // GET: Team/Details/5
        public ActionResult Details(int id)
        {
            return View(man.TeamGetById(id));
        }

        [Authorize(Roles = "Manager")]
        public ActionResult Create()
        {
            return View(new TeamAddForm());
        }

        [Authorize(Roles = "Manager")]
        [HttpPost]
        public ActionResult Create(TeamAdd team)
        {
            if (!ModelState.IsValid)
                return View(team);
            else
            {
                var added = man.TeamAdd(team); //add the team..

                if (added == null)
                    return View(team);
                else
                    return RedirectToAction("Details", "Team", new { id = added.Id }); 
            }
        }

        public ActionResult TeamSearch()
        {
            return View(new TeamSearchForm()); 
        }

        [Route("Team/Teams/{searchText}")]
        public ActionResult Teams(string searchText ="")
        {
            var t = man.TeamGetByName(searchText);

            if (t == null)
                return PartialView("_TeamList", new List<TeamBase>());
            else
                return PartialView("_TeamList", t); 
        }

        [Authorize(Roles = "Coach")]
        [Route("Team/{id}/CreatePlayer")]
        public ActionResult CreatePlayer(int id)
        {
            var team = man.TeamGetById(id);

            if (team == null)
                return HttpNotFound();
            else
            {
                var form = new PlayerAddForm();
                form.TeamId = team.Id;
                form.TeamName = team.Name;

                return View(form); 
            }
        }

        [Authorize(Roles = "Coach")]
        [HttpPost]
        [Route("Team/{id}/CreatePlayer")]
        public ActionResult CreatePlayer(PlayerAdd player)
        {
            if(!ModelState.IsValid)
            {
                return View(player); 
            }
            else
            {
                var added = man.PlayerAdd(player);

                if (added == null)
                    return View(added);
                else
                {
                    return RedirectToAction("Details", "Player", new { id = added.Id });
                }
            }
        }

        [Authorize(Roles = "Manager")]
        [Route("Team/{id}/CreateCoach")]
        public ActionResult CreateCoach(int id)
        {
            var team = man.TeamGetById(id);

            if (team == null)
                return HttpNotFound();
            else
            {
                var form = new CoachAddForm();
                form.TeamName = team.Name;
                form.TeamId = team.Id;

                return View(form);
            }
        }

        [Authorize(Roles = "Manager")]
        [HttpPost]
        [Route("Team/{id}/CreateCoach")]
        public ActionResult CreateCoach(CoachAdd coach)
        {
            if (!ModelState.IsValid)
            {
                return View(coach);
            }
            else
            {
                var added = man.CoachAdd(coach);

                if (added == null)
                    return View(added);
                else
                {
                    return RedirectToAction("Details", "Coach", new { id = added.Id });
                }
            }
        }



    }
}
