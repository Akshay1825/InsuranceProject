namespace InsuranceProject.Services
{
    public interface ICloudinaryService
    {
        public string UploadFile(IFormFile file);
        public string GetFileUrl(Guid publicId);
        public bool DeleteFile(Guid publicId);
    }
}
