using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Assignment9.Controllers
{
    public class PlayerController : Controller
    {
        private Manager man = new Manager(); 
        // GET: Player
        public ActionResult Index()
        {
            return View(man.PlayersGetAll());
        }

        // GET: Player/Details/5
        public ActionResult Details(int id)
        {
            return View(man.PlayerGetById(id));
        }

        public ActionResult Search()
        {
            return View(new PlayerSearchForm());
        }

        [Route("Player/Players/{searchText}")]
        public ActionResult Players(string searchText = "")
        {
            // Fetch matching tracks
            var c = man.PlayerGetByName(searchText);

            if (c == null)
            {
                // Empty list
                return PartialView("_PlayerList", new List<PlayerBase>());
            }
            else
            {
                return PartialView("_PlayerList", c);
            }
        }
    }
}
