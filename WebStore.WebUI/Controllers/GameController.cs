using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebStore.Domain.Entities;
using WebStore.WebUI.Domain;
using WebStore.WebUI.Domain.Entities;
using WebStore.WebUI.Models;

namespace WebStore.WebUI.Controllers
{
    public class GameController : Controller
    {
        public GameRepository gameRepository;
        public int pageSize = 8;

        public GameController()
        {
            gameRepository = new GameRepository();
            gameRepository.RepositoryInit();
        }

        public ViewResult List(string category, int page = 1)
        {
            GamesListViewModel model = new GamesListViewModel
            {
                Games = DatabaseRelation.getGames()
                    .Where(p => category == null || p.Category == category)
                    .OrderBy(game => game.GameID)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = category == null ?
                gameRepository.games.Count() :
                gameRepository.games.Where(game => game.Category == category).Count()
                },
                CurrentCategory = category
            };
            return View(model);
        }

        [HttpGet]
        public ActionResult LoginForm()
        {
            return View();
        }

        [HttpPost]
        public ViewResult LoginForm(LoginForm client)
        {
            if (ModelState.IsValid)
            {
                if (client != null)
                {
                    User u = DatabaseRelation.logIn(client);
                    if (u != null)
                    {
                        Session["User"] = u;
                        return View("Welcome", u);
                    }
                    else return View();
                }
                else return View("Index");
            }
            else return View();
        }

        [HttpGet]
        public ActionResult RegisterForm()
        {
            return View();
        }

        [HttpPost]
        public ViewResult RegisterForm(RegisterForm client)
        {
            if (ModelState.IsValid)
            {
                if (client != null)
                {
                    DatabaseRelation.RegisterUser(client);
                    return View();
                }
                else return View("Index");
            }
            else return View();
        }

        public PartialViewResult LoginRegisterForm()
        {
            User user = (User)Session["User"];
            if(user == null)
                return PartialView();
            else
                if (user.Admin == 1)
                return PartialView("AdminController", user);
            else
                return PartialView("UserController", user);
        }

        [HttpGet]
        public RedirectToRouteResult LogOut()
        {
            Session["User"] = null;
            return RedirectToAction("LoginForm");
        }
    }
}