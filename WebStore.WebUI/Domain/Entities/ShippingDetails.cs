using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebStore.WebUI.Domain.Entities
{
    public class ShippingDetails
    {
        [Required(ErrorMessage = "Please enter your name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Insert first shipping address")]
        [Display(Name = "First address")]
        public string Line1 { get; set; }
        [Display(Name = "Second address")]
        public string Line2 { get; set; }
        [Display(Name = "Third address")]
        public string Line3 { get; set; }

        [Required(ErrorMessage = "Specify the city")]
        [Display(Name = "City")]
        public string City { get; set; }
        [Display(Name = "Country")]

        [Required(ErrorMessage = "Specify country")]
        public string Country { get; set; }
    }
}