using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using my_books.ActionResult;
using my_books.Data.services;
using my_books.Data.ViewModels;
using Newtonsoft.Json;

namespace my_books.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        private PublishersService _publishersService;

        public PublishersController(PublishersService publishersService)
        {
            _publishersService = publishersService;
        }

        [HttpPost]
        public IActionResult AddPublisher([FromBody] PublisherVM publisher)
        {
            _publishersService.AddPublisher(publisher);
            return Ok();
        }

        [HttpGet("get-publisher-by-id/{id}")]

        public CustomActionResult GetPublisherById(int id)
        {
            var publisher = _publishersService.GetPublisherById(id);
            if (publisher != null)
            {
               return new CustomActionResult(new CustomActionResultVM()
                {
                    Publisher = publisher
                });
            }
            else
            {
                return new CustomActionResult(new CustomActionResultVM()
                {
                    Exception = new Exception("Publisher not found response using CustomActionResult")
                });
            }
        }

        [HttpGet]
        public IActionResult GetAllPublisher(string? sortBy, string? searchName,int? pageIndex,int? pageSize)
        {
            var publishers = _publishersService.GetAllPublisher(sortBy,searchName,pageIndex,pageSize);
            var metadata = new
            {
                publishers.TotalPages,
                publishers.PageIndex,
                publishers.HasNextPage,
                publishers.HasPreviousPage
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(publishers); 
        }
    }
}
