using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BlazorBattles.Shared
{
    public class UserLogin
    {
        [Required(ErrorMessage = "Please enter an Email Address.")]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
