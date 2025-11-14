using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using AGS_services.Repositories;

public class AwsS3Service : IFileStorageService
{
    private readonly IAmazonS3 _s3Client;
    private readonly string _bucketName;
    private readonly string _region;

    public AwsS3Service(IConfiguration configuration)
    {
        var awsConfig = configuration.GetSection("AWS");
        _bucketName = awsConfig["BucketName"];
        _region = awsConfig["Region"];
        var accessKey = awsConfig["AccessKeyID"];
        var secretKey = awsConfig["SecretAccessKey"];

        _s3Client = new AmazonS3Client(accessKey, secretKey, Amazon.RegionEndpoint.GetBySystemName(_region));
    }

    public async Task<string> UploadFileAsync(IFormFile file)
    {
        var uniqueFileName = $"{Guid.NewGuid()}-{file.FileName}";

        var request = new PutObjectRequest
        {
            BucketName = _bucketName,
            Key = uniqueFileName,
            InputStream = file.OpenReadStream(),
            ContentType = file.ContentType
        };

        await _s3Client.PutObjectAsync(request);
        return uniqueFileName;
    }
    public string GetFileUrl(string key)
    {
        var presignedUrlRequest = new GetPreSignedUrlRequest
        {
            BucketName = _bucketName,
            Key = key,
            Expires = DateTime.UtcNow.AddHours(1)
        };

        return _s3Client.GetPreSignedURL(presignedUrlRequest);
    }
}