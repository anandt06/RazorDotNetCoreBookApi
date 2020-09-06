using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListRazorCoreApp.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookListRazorCoreApp.Pages.BookList
{
    public class EditModel : PageModel
    {

        private readonly ApplicationDbContext _applicationDbContext;

        [BindProperty]
        public Book Book { get; set; }
        public EditModel(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public async Task OnGet(int id)
        {
            Book = await _applicationDbContext.Books.FindAsync(id);
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var BookFrmDb = await _applicationDbContext.Books.FindAsync(Book.Id);
                BookFrmDb.Author =Book.Author;
                BookFrmDb.Name = Book.Name;
                await _applicationDbContext.SaveChangesAsync();
                RedirectToPage("Index");
            }
            return RedirectToPage();
        }
    }
}