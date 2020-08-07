using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApp_Core.Data;
using WebApp_Core.Helpers;

namespace WebApp_Core.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]/{userId}")]
    public class PhotoController : ControllerBase
    {
        private readonly IOptions<CloudinarySettings> _cloudinarySettings;
        private Cloudinary _cloudinary;
        private readonly IPhotoRepository repo;
        public PhotoController(IPhotoRepository repo, IOptions<CloudinarySettings> _cloudinarySettings)
        {
            this.repo = repo;
            this._cloudinarySettings = _cloudinarySettings;
            Account account = new Account(
            _cloudinarySettings.Value.CloudName,
            _cloudinarySettings.Value.APIKey,
            _cloudinarySettings.Value.APISecret
            );

            _cloudinary = new Cloudinary(account);
        }
        [HttpPost("uploadPhoto")]
        public async Task<IActionResult> UploadPhoto(int userId, IFormFile filePhoto)
        {
            var uploadResult = new ImageUploadResult();

            using (var stream = filePhoto.OpenReadStream())
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(filePhoto.Name, stream),
                    Transformation = new Transformation().Width(500).Height(500)
                };
                uploadResult = _cloudinary.Upload(uploadParams);
            }

            string url = await repo.AddProfilePhoto(userId, uploadResult.Url.ToString());
            
            return Ok(url);
        }
    }
}