using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace InsuranceProject.Services
{
    public class CloudinaryService:ICloudinaryService
    {
        private readonly Cloudinary _cloudinary;

        public CloudinaryService(IConfiguration config)
        {
            var account = new Account(
                config["CloudinarySettings:CloudName"],
                config["CloudinarySettings:ApiKey"],
                config["CloudinarySettings:ApiSecret"]
            );

            _cloudinary = new Cloudinary(account);
        }

        public string UploadFile(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    throw new ArgumentException("No file uploaded");
                }

                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(file.FileName, file.OpenReadStream())
                };

                var uploadResult = _cloudinary.Upload(uploadParams);

                if (uploadResult.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    throw new Exception("Error uploading file to Cloudinary");
                }

                return uploadResult.SecureUrl.ToString(); // Return URL of the uploaded file
            }
            catch (Exception ex)
            {
                throw new Exception($"Error uploading file: {ex.Message}");
            }
        }

        public string GetFileUrl(Guid publicId)
        {
            string publicIdString = publicId.ToString();
            try
            {
                var url = _cloudinary.Api.UrlImgUp.BuildUrl(publicIdString);
                return url;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching file URL: {ex.Message}");
            }
        }

        public bool DeleteFile(Guid publicId)
        {
            string publicIdString = publicId.ToString();
            try
            {
                var deletionParams = new DeletionParams(publicIdString);
                var deletionResult = _cloudinary.Destroy(deletionParams);
                return deletionResult.StatusCode == System.Net.HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting file: {ex.Message}");
            }
        }
    }
}
