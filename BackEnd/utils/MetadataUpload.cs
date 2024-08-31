using Microsoft.AspNetCore.Mvc;

namespace BackEnd.utils
{
    public class MetadataUpload
    {
        public static async Task<IActionResult> UploadImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return new BadRequestObjectResult("File is empty");
            }

            ////defined file_path to save metadata
            //// path = ???

            //using (var stream = new FileStream(path, FileMode.Create))
            //{
            //    await file.CopyToAsync(stream);
            //}

            ////return metadata path
            //return new OkObjectResult(path);
            return new OkObjectResult("path");
        }
    }
}
