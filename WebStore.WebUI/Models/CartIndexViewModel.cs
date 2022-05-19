using WebStore.WebUI.Domain.Entities;

namespace WebStore.WebUI.Models
{
    public class CartIndexViewModel
    {
        public Cart Cart { get; set; }
        public User User { get; set; }
        public string ReturnUrl { get; set; }
    }
}