using CsvHelper;
using CsvHelper.Configuration;
using JEPCO.Application.Interfaces.FileExport;
using System.Globalization;
using System.Text;

namespace JEPCO.Infrastructure.FileExport.CSV;

public class CSVManager : IFileExport
{
    public CSVManager()
    {

    }


    public async Task<string> GenerateFileAsync<T>(IEnumerable<T> records, List<string>? customHeaders = null)
    {
        var addCustomHeader = false;
        if (customHeaders != null && customHeaders.Any())
        {
            // Header will be added manually from the variable
            addCustomHeader = true;
        }

        IWriterConfiguration csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = !addCustomHeader,
            Delimiter = ",",
            Encoding = Encoding.UTF8,
            InjectionOptions = InjectionOptions.None,
        };

        using (var writer = new StringWriter())
        using (var csv = new CsvWriter(writer, csvConfig))
        {
            if (addCustomHeader)
            {
                csv.WriteField(customHeaders);
                csv.NextRecord();
            }

            await csv.WriteRecordsAsync(records);

            return writer.ToString();
        }
    }
}
