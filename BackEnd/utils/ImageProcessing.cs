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
    }
}
