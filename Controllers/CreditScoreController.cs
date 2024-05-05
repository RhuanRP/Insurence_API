using Microsoft.AspNetCore.Mvc;
using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using Microsoft.Extensions.Caching.Memory;

[ApiController]
[Route("[controller]/[action]")]
public class CreditScoreController : ControllerBase
{
  private readonly ILogger<CreditScoreController> _logger;
  private readonly IDataLoad _dataLoad;
  private readonly IMemoryCache _cache;
  private const string CsvCacheKey = "CsvData";
  public CreditScoreController(ILogger<CreditScoreController> logger, IDataLoad dataLoad, IMemoryCache cache)
  {
    _logger = logger;
    _dataLoad = dataLoad;
    _cache = cache;
  }

  [HttpGet(Name = "GetCsvData")]
  public async Task<IEnumerable<string>> GetCsvData()
  {
    string csvContent;
    if (!_cache.TryGetValue(CsvCacheKey, out csvContent))
    {
      using (var httpClient = new HttpClient())
      {
        try
        {
          var response = await httpClient.GetAsync("https://insuranceapistorage.blob.core.windows.net/csv/Car_Insurance_Claim.csv?sv=2022-11-02&ss=b&srt=sco&sp=rlitf&se=2024-06-06T04:48:06Z&st=2024-05-05T20:48:06Z&spr=https&sig=W1%2FAoUXGf6XuVAp3J2q7gRNix0hGDEo0y1%2F%2FHdZLV%2BE%3D");
          if (response.IsSuccessStatusCode)
          {
            csvContent = await response.Content.ReadAsStringAsync();
            _cache.Set(CsvCacheKey, csvContent, new MemoryCacheEntryOptions
            {
              AbsoluteExpiration = DateTimeOffset.UtcNow.AddMinutes(25)
            });
          }
          else
          {
            throw new Exception("Falha ao obter o conteúdo do CSV.");
          }
        }
        catch (Exception ex)
        {
          _logger.LogError(ex, "Erro ao acessar a URL do CSV.");
          throw;
        }
      }
    }
    var csvLines = csvContent.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
    return csvLines;
  }

  [HttpGet("/credit_score")]
  public async Task<IActionResult> CalculateCreditScoreAsync(
    [FromQuery] int age,
    [FromQuery] string gender,
    [FromQuery] int drivingExperience,
    [FromQuery] string education,
    [FromQuery] string income,
    [FromQuery] int vehicleYear,
    [FromQuery] string vehicleType,
    [FromQuery] string annualMileage)
  {

    IEnumerable<string> csvData = await GetCsvData();

    List<Data> dataset = new List<Data>();
    using (var reader = new StringReader(string.Join(Environment.NewLine, csvData)))
    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
    {
      dataset = csv.GetRecords<Data>().ToList();
    }

    var creditScores = CreditScore.Calcular(age, gender, drivingExperience, education, income, vehicleYear, vehicleType, annualMileage, dataset);
    return Ok(creditScores);
  }
}