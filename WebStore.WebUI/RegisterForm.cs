using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebStore.WebUI
{
    public class RegisterForm
    {
        public int UserId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string Mail { get; set; }
        public string Phone { get; set; }
    }
}