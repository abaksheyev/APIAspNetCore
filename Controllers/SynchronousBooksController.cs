using System;
using System.Threading.Tasks;
using APIAspNetCore.Services;
using Microsoft.AspNetCore.Mvc;

namespace APIAspNetCore.Controllers
{

    [Route("api/synchronousbooks")]
    [ApiController]
    public class SynchronousBooksController: ControllerBase
    {
        private IBooksRepository _booksrepository;
        public SynchronousBooksController(IBooksRepository booksRepository)
        {
            _booksrepository = booksRepository ?? throw new ArgumentNullException(nameof(booksRepository));
        }

        [HttpGet]
        public IActionResult GetBooks()
        {
            var bookEntities = _booksrepository.GetBooks();
            return Ok(bookEntities);
        }
   

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetBook(Guid id)
        {
            var bookEntity = _booksrepository.GetBook(id);
            if(bookEntity == null)
            {
                return NotFound();
            }

            return Ok(bookEntity);
        }
    } 
}