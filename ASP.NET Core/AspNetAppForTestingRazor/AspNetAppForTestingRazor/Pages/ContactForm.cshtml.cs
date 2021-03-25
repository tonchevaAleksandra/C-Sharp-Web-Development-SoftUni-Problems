using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
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

        public override void OnPageHandlerSelected (PageHandlerSelectedContext context)
        {
            base.OnPageHandlerSelected(context);
        } // After a handler (OnGet, OnPost) has been selected,before model binding

        public override void OnPageHandlerExecuting(PageHandlerExecutingContext context)
        {
            base.OnPageHandlerExecuting(context);
        } // Before the handler executes, after model binding

        public override void OnPageHandlerExecuted(PageHandlerExecutedContext context)
        {
            base.OnPageHandlerExecuted(context);
        } // After the handler executes, before the action result

        public override Task OnPageHandlerExecutionAsync(PageHandlerExecutingContext context, PageHandlerExecutionDelegate next)
        {
            //if (....)
            //{
            //var result = await next();
            //}

            return base.OnPageHandlerExecutionAsync(context, next);
        } // Before the handler is invoked, after model binding
    }
}
