namespace SBRSystem_Data.DTO;

public class BucketConfig
{
    public string S3Bucket { get; set; }
    public string VultrUrl { get; set; }
    public string AWSAccessKey { get; set; }
    public string AWSSecretKey { get; set; }


    public static string SolicitudesFolder { get; } = "folders";
}