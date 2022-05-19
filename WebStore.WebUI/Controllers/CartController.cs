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
    public class CartController : Controller
    {
        private GameRepository gameRepository;
        public CartController()
        {
            gameRepository = new GameRepository();
            gameRepository.RepositoryInit();
        }

        public ViewResult Index(Cart cart, string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                User = (User)Session["User"],
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }

        public RedirectToRouteResult AddToCart(Cart cart, int gameId, string returnUrl)
        {
            Game game = gameRepository.games
                .FirstOrDefault(g => g.GameID == gameId);

            if (game != null)
            {
                cart.AddItem(game, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart, int gameId, string returnUrl)
        {
            Game game = gameRepository.games
                .FirstOrDefault(g => g.GameID == gameId);

            if (game != null)
            {
                cart.RemoveLine(game);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }

        public ViewResult Checkout(Cart cart)
        {
            if (Session["User"] as User == null)
                return null;
            DatabaseRelation.MakeOrder(cart, (User)Session["User"]);
            cart.Clear();
            return View();
        }

        [HttpGet]
        public ActionResult LoginForm()
        {
            return View("~/Views/Game/LoginForm.cshtml");
        }

        [HttpGet]
        public ActionResult RegisterForm()
        {
            return View("~/Views/Game/RegisterForm.cshtml");
        }
    }
}