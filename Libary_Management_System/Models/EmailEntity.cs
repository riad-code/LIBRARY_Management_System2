using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Libary_Management_System.Models
{
    public class EmailEntity
    {
        [Required, EmailAddress]
        public string FromEmail { get; set; }

        [Required, EmailAddress]
        public string ToEmailAddress { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string EmailBody { get; set; }

        [ValidateNever]
        public string AttatchmentURL { get; set; }
    }

}

