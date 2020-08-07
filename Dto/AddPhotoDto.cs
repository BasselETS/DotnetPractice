using Microsoft.AspNetCore.Http;

namespace WebApp_Core.Dto
{
    public class AddPhotoDto
    {
        public int ID { get; set; } 
        public IFormFile FormFile { get; set; }
    }
}