using Microsoft.AspNetCore.Mvc;
using my_books.Data.models;
using my_books.Data.ViewModels;

namespace my_books.ActionResult
{
    public class CustomActionResult : IActionResult
    {

        private readonly CustomActionResultVM customActionResultVM;

        public CustomActionResult(CustomActionResultVM customActionResultVM)
        {
            this.customActionResultVM = customActionResultVM;
        }
           
        public async Task ExecuteResultAsync(ActionContext context)
        {
            //build the response
            var objectResult = new ObjectResult(customActionResultVM.Exception ?? customActionResultVM.Publisher as object)
            {
                StatusCode = (customActionResultVM.Exception != null) ? StatusCodes.Status500InternalServerError : StatusCodes.Status200OK
            };
            await objectResult.ExecuteResultAsync(context);
        }
    }
}
