using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspNetAppForTestingRazor.Pages
{
    public class ContactFormModel : PageModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [BindProperty]
        public string Email { get; set; }

        [Required]
        [BindProperty]
        public string Name { get; set; }

        [Required]
        [BindProperty]
        public string Title { get; set; }

        [Required]
        [BindProperty]
        public string Content { get; set; }

        public string InfoMessage { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                this.InfoMessage = "Thank you for submitting contact form!";
                // TODO: Send mail or save data
                return Page();
            }
            return this.Redirect("/");
            
        }
    }
}
