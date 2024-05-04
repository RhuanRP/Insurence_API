using Microsoft.AspNetCore.Mvc;
using CsvHelper;


[ApiController]
[Route("[controller]/[action]")]
public class CreditScoreController : ControllerBase
{
  private readonly IDataLoad _dataLoad;

  private readonly ILogger<CreditScoreController> _logger;

  public CreditScoreController(ILogger<CreditScoreController> logger, IDataLoad DataLoad)
  {
    _logger = logger;
    _dataLoad = DataLoad;
  }

  [HttpGet("/credit_score")]
  public IActionResult CalculateCreditScore([FromHeader] string age, [FromHeader] string gender, [FromHeader] string drivingExperience, [FromHeader] string education, [FromHeader] string income, [FromHeader] string vehicleYear, [FromHeader] string vehicleType, [FromHeader] string annualMileage)
  {

    List<Data> Data = _dataLoad.Search();
    var Pessoa = Data.Where(x => x.AGE == age && x.GENDER == gender && x.DRIVING_EXPERIENCE == drivingExperience && x.EDUCATION == education && x.INCOME == income && x.VEHICLE_TYPE == vehicleType && x.VEHICLE_YEAR == vehicleYear && x.ANNUAL_MILEAGE == annualMileage);
    var creditScores = Pessoa.Select(x => x.CREDIT_SCORE);

    return Ok(creditScores);
  }
}