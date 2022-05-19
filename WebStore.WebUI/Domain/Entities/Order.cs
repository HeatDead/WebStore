using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebStore.WebUI.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int GameId { get; set; }
    }
}