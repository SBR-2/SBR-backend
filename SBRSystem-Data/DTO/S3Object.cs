using Microsoft.AspNetCore.Http;

namespace SBRSystem_Data.DTO;

public class S3Object
{
    public string Name { get; }
    public IFile InputStream { get; }
    public string FileWeight { get; }

    private S3Object(string fileName, IFile binary, string weight)
    {
        Name = fileName;
        InputStream = binary;
        FileWeight = weight;
    }

    public static async Task<S3Object> CrearObjeto(string objName, IFile documento)
    {
        var s3obj = new S3Object(objName, documento, GetFileSize(documento));

        return s3obj;
    }

    public static string GetFileSize(IFile archivo)
    {
        long? tamanioBytes = archivo.Length;
        if (tamanioBytes == null)
            return "";

        // Determinar la unidad de medida más adecuada
        string unidadMedida;
        double size;
        if (tamanioBytes >= 1073741824) // GB
        {
            size = (double)tamanioBytes / 1073741824;
            unidadMedida = "GB";
        }
        else if (tamanioBytes >= 1048576) // MB
        {
            size = (double)tamanioBytes / 1048576;
            unidadMedida = "MB";
        }
        else // KB
        {
            size = (double)tamanioBytes / 1024;
            unidadMedida = "KB";
        }

        // Formatear el resultado con dos decimales
        return $"{size:N0} {unidadMedida}";
    }
}