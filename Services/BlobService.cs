using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

public class BlobService
{
    private readonly BlobContainerClient _container;

    public BlobService(IConfiguration config)
    {
        string connectionString = Environment.GetEnvironmentVariable("AZURE_STORAGE_CONNECTION_STRING");
        string container = Environment.GetEnvironmentVariable("AZURE_CONTAINER_NAME");

        _container = new BlobContainerClient(connectionString, container);
        _container.CreateIfNotExists();
        _container.SetAccessPolicy(PublicAccessType.Blob);
    }

    public async Task<string> UploadAsync(IFormFile file, string folder)
    {
        string extension = Path.GetExtension(file.FileName);
        string fileName = $"{folder}/{Guid.NewGuid()}{extension}";

        var blob = _container.GetBlobClient(fileName);

        using (var stream = file.OpenReadStream())
        {
            await blob.UploadAsync(stream, overwrite: true);
        }

        return blob.Uri.ToString();
    }

    public async Task DeleteAsync(string url)
    {
        string blobName = url.Split(".net/")[1];
        var blob = _container.GetBlobClient(blobName);
        await blob.DeleteIfExistsAsync();
    }
}
