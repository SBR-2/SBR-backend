using SBRSystem_Data.DTO;

namespace SBRSystem_Entities.Contracts;

public interface IBucketRepository
{
    Task<S3ResponseDto> UploadFileAsync(S3Object s3Obj);
    Task<S3ResponseDto> DeleteFileAsync(S3Object s3Obj);
}