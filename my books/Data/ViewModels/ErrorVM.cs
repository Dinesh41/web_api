using Newtonsoft.Json;

namespace my_books.Data.ViewModels
{
    public class ErrorVM
    {
        public string Message { get; set; }
        public int Code { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
