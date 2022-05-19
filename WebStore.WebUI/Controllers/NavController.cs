using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebStore.WebUI.Domain;

namespace WebStore.WebUI.Controllers
{
    public class NavController : Controller
    {
        private GameRepository gameRepository;
        public NavController()
        {
            gameRepository = new GameRepository();
            gameRepository.RepositoryInit();
        }

        public PartialViewResult Menu(string category = null)
        {
            ViewBag.SelectedCategory = category;

            IEnumerable<string> categories = gameRepository.games
                .Select(game => game.Category)
                .Distinct()
                .OrderBy(x => x);
            return PartialView(categories);
        }
    }
}