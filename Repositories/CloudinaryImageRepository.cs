using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace Bloggie.Repositories;

public class CloudinaryImageRepository : IImageRepository
{
    private readonly IConfiguration _configuration;
    public Account _account;

    public CloudinaryImageRepository(IConfiguration configuration)
    {
        _configuration = configuration;
        _account = new Account(_configuration.GetSection("Cloudinary")["Name"],
            _configuration.GetSection("Cloudinary")["ApiKey"], _configuration.GetSection("Cloudinary")["ApiSecret"]);
    }
    public async Task<string> UploadImageAsync(IFormFile file)
    {
        var client = new Cloudinary(_account);
        var uploadParams = new ImageUploadParams()
        {
            File = new FileDescription(file.Name, file.OpenReadStream()),
            DisplayName = file.Name
        };
        var uploadResult = await client.UploadAsync(uploadParams);
        if (uploadResult != null)
        {
            return uploadResult.SecureUrl.ToString();
        }
        return string.Empty;
    }
}