using BackEnd.Database.Tables;
using System.Text.RegularExpressions;

namespace BackEnd.utils
{
    public static class ImageProcessing
    {
        public async static Task<string> StoreImageWithBase64Format(string base64String, string filepath)
        {
            try
            {
                //check path is exist
                var path = StaticPathCollection.GetUploadProjectPath();
                var folderPath = $"{path}/{filepath.Substring(0, filepath.LastIndexOf('/'))}";
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                // Convert base64 string to byte[]
                byte[] imageBytes = Convert.FromBase64String(base64String);
                // Get store path in root project
                var storePath = StaticPathCollection.GetUploadProjectPath(filepath);
                // Write byte[] to file
                using(MemoryStream ms = new MemoryStream(imageBytes))
                {
                    using (FileStream fs = new FileStream(storePath, FileMode.Create))
                    {
                        ms.WriteTo(fs);
                    }
                }
                return StaticPathCollection.GetStaticPath(filepath);
            }
            catch (Exception ex)
            {
                throw new Exception("Error storing image", ex);
            }
        }
        public async static Task<string> StoreImage(IFormFile file, string filepath)
        {
            var path = StaticPathCollection.GetUploadProjectPath();
            var folderPath = $"{path}/{filepath.Substring(0, filepath.LastIndexOf('/'))}";

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            var storePath = StaticPathCollection.GetUploadProjectPath(filepath);
            using (var stream = new FileStream(storePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return StaticPathCollection.GetStaticPath(filepath);
        }
        public async static Task<(string UpdatedContent, List<string> ImageUrls, List<string> VideoUrls)> ProcessImagesAndVideos(string content, User user)
        {
            var imageUrls = new List<string>();
            var videoUrls = new List<string>();
            // Pattern to match base64 images
            string pattern = @"data:image\/([a-zA-Z]*);base64,([^\""]*)";

            //Find all matches in content
            MatchCollection matches = Regex.Matches(content, pattern);
            //with any match -> restore this picture and store it to uploads folder
            foreach (Match match in matches)
            {
                var imageType = match.Groups[1].Value;
                var base64Data = match.Groups[2].Value;
                // Store image with base64 format
                var filepath = $"post/{user.UID}_{Guid.NewGuid().ToString()}.{imageType}";
                var imagePath = await ImageProcessing.StoreImageWithBase64Format(base64Data, filepath);
                //replace base64 image with image static path
                content.Replace(match.Value, imagePath);
                imageUrls.Add(imagePath);
            }
            return (content, imageUrls, videoUrls);
        }
    }
}
