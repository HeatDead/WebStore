using System.Collections.Generic;
using System.Web.Mvc;
using WebStore.WebUI.Domain;
using WebStore.WebUI.Domain.Entities;

namespace WebStore.WebUI.Infrastructure.Binders
{
    public class LoginModelBinder : IModelBinder
    {
        private const string sessionKey = "User";

        public object BindModel(ControllerContext controllerContext,
            ModelBindingContext bindingContext)
        {
            User user = null;
            if (controllerContext.HttpContext.Session != null)
            {
                user = (User)controllerContext.HttpContext.Session[sessionKey];
            }

            if (user == null)
            {
                user = null;
                if (controllerContext.HttpContext.Session != null)
                {
                    controllerContext.HttpContext.Session[sessionKey] = user;
                }
            }

            return user;
        }
    }
}