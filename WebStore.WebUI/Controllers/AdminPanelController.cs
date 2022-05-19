using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebStore.Domain.Entities;
using WebStore.WebUI.Domain;
using WebStore.WebUI.Domain.Entities;
using WebStore.WebUI.Models;

namespace WebStore.WebUI.Controllers
{
    public class AdminPanelController : Controller
    {
        public GameRepository gameRepository;
        public AdminPanelController()
        {
            gameRepository = new GameRepository();
            gameRepository.RepositoryInit();
        }

        public bool AdminCheck()
        {
            User user = (User)Session["User"];
            if (user == null)
                return false;
            if(user.Admin == 0)
                return false;
            return true;
        }

        public ActionResult Index()
        {
            if (!AdminCheck())
                return View("Index");
            return View();
        }

        public ActionResult EditPage()
        {
            if (!AdminCheck())
                return View("Index");
            GamesListViewModel model = new GamesListViewModel
            {
                Games = DatabaseRelation.getGames()
        .OrderBy(game => game.GameID)
            };
            return View(model);
        }

        public ActionResult Edit(int gameId)
        {
            if (!AdminCheck())
                return View("Index");
            Game game = new Game();
            foreach(Game g in DatabaseRelation.getGames())
            {
                if (g.GameID == gameId)
                    game = g;
            }
            return View(game);
        }

        public ActionResult AddPage()
        {
            if (!AdminCheck())
                return View("Index");
            return View();
        }

        [HttpPost]
        public ViewResult EditGame(Game game)
        {
            GamesListViewModel model = new GamesListViewModel
            {
                Games = DatabaseRelation.getGames()
                .OrderBy(gm => gm.GameID)
            };
            if (ModelState.IsValid)
            {
                if (game != null)
                {
                    DatabaseRelation.EditGame(game);
                    model = new GamesListViewModel
                    {
                        Games = DatabaseRelation.getGames()
                        .OrderBy(gm => gm.GameID)
                    };
                    return View("EditPage", model);
                }
                else return View("Index");
            }
            else return View("Index");
        }

        [HttpPost]
        public ViewResult AddGame(Game game)
        {
            if (ModelState.IsValid)
            {
                if (game != null)
                {
                    DatabaseRelation.AddGame(game);
                    GamesListViewModel model = new GamesListViewModel
                    {
                        Games = DatabaseRelation.getGames()
                        .OrderBy(gm => gm.GameID)
                    };
                    return View("EditPage", model);
                }
                else return View("Index");
            }
            else return View("Index");
        }

        public RedirectToRouteResult DeleteGame(int gameId)
        {
            DatabaseRelation.DeleteGame(gameId);
            GamesListViewModel model = new GamesListViewModel
            {
                Games = DatabaseRelation.getGames()
                .OrderBy(gm => gm.GameID)
            };
            return RedirectToAction("EditPage", model);
        }

        [HttpGet]
        public ActionResult OrdersPage()
        {
            return View(DatabaseRelation.getOrders());
        }
    }
}