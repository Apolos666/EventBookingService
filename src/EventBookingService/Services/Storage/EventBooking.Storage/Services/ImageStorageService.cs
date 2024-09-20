namespace EventBooking.Storage.Services;

[Authorize(AuthenticationSchemes = "storage_service")]
public class ImageStorageService
    (IHostEnvironment environment)
    : ImageStorage.ImageStorageBase
{
    private const string StaticFilesPath = "StaticFiles";
    private const string BaseUrl = "https://localhost:6067";
    
    public override async Task<UploadImageResponse> UploadImage(UploadImageRequest request, ServerCallContext context)
    {
        var category = string.IsNullOrEmpty(request.Category) ? "misc" : request.Category.ToLowerInvariant();
        var uploadsFolderPath = Path.Combine(environment.ContentRootPath, StaticFilesPath, category);

        if (!Directory.Exists(uploadsFolderPath))
        {
            Directory.CreateDirectory(uploadsFolderPath);
        }

        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(request.FileName)}";
        var filePath = Path.Combine(uploadsFolderPath, fileName);

        await File.WriteAllBytesAsync(filePath, request.ImageData.ToByteArray());
        
        var imageUrl = $"{BaseUrl}/{StaticFilesPath}/{category}/{fileName}";

        return new UploadImageResponse
        {
            ImageUrl = imageUrl
        };
    }
}