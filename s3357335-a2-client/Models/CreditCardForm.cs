using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using s3357335_a2_client.Utilities;

namespace s3357335_a2_client.Models
{
    public class CreditCardForm
    {
        [Display(Name = "Credit Card Type")]
        [Required]
        public CardType CreditCardType { get; set; }

        [Display(Name = "Credit Card Number")]
        [Required]
        [CreditCard]
        public string CreditCardNumber { get; set; }

        [Display(Name = "Email")]
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}