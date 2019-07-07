using System;
using System.Threading.Tasks;
using APIAspNetCore.Filters;
using APIAspNetCore.Models;
using APIAspNetCore.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace APIAspNetCore.Controllers
{

    [Route("api/books")]
    [ApiController]
    public class BooksController: ControllerBase
    {
        private IBooksRepository _booksrepository;
        private readonly IMapper _mapper;
        public BooksController(IBooksRepository booksRepository, IMapper mapper)
        {
            _booksrepository = booksRepository ?? throw new ArgumentNullException(nameof(booksRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        [BooksResultFilter]
        public async Task<IActionResult> GetBooks()
        {
            var bookEntities = await _booksrepository.GetBooksAsync();
            return Ok(bookEntities);
        }
   

        [HttpGet]
        [BookResultFilter]
        [Route("{id}", Name= "GetBook")]
        public async Task<IActionResult> GetBook(Guid id)
        {
            var bookEntity = await _booksrepository.GetBooksAsync(id);
            if(bookEntity == null)
            {
                return NotFound();
            }

            return Ok(bookEntity);
        }

        [HttpPost]
        [BookResultFilter]
        public async Task<IActionResult> CreateBook([FromBody] BookForCreation book)
        {
            var bookEntity = _mapper.Map<Entities.Book>(book);
            _booksrepository.AddBook(bookEntity);

            await _booksrepository.SaveChangesAsync();

            await _booksrepository.GetBooksAsync(bookEntity.Id);

            return CreatedAtRoute("GetBook", new Book { Id = bookEntity.Id}, bookEntity);
        }
    } 
}