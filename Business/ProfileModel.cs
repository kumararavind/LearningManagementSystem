using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Business
{
    public class ProfileModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Email { get; set; }

        [Required]
        public Int64 Contact { get; set; }
        [DisplayName("Birth date")]
        [Required]
        [DataType(DataType.Date)]
        public string DateOfBirth { get; set; }

        [DisplayName("Gender")]
        [Required]
        public string Gender { get; set; }

        [DisplayName("Address")]
        [Required]
        public string Address { get; set; }

        [DisplayName("Wallet Amount")]
        public Int64 Wallet { get; set; }


        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("Confirm Password")]
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        public int AccountID { get; set; }
        public string AccountType { get; set; }
        public List<ProfileModel> AccountList { get; set; }
    }
}
