namespace JEPCO.Application.Interfaces.FileExport;

public interface IFileExport
{
    Task<string> GenerateFileAsync<T>(IEnumerable<T> records, List<string>? customHeaders = null);
}
