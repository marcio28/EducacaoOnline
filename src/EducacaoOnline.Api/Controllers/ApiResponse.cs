namespace EducacaoOnline.Api.Controllers
{
    public class ApiResponse<T>
    {
        public bool success { get; set; }
        public T? data { get; set; }
    }
}