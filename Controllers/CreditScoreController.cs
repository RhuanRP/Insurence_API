using Microsoft.AspNetCore.Mvc;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Linq;

[ApiController]
[Route("[controller]/[action]")]
public class CreditScoreController : ControllerBase
{
  private readonly IDataLoad _dataLoad;
  private readonly ILogger<CreditScoreController> _logger;

  public CreditScoreController(ILogger<CreditScoreController> logger, IDataLoad dataLoad)
  {
    _logger = logger;
    _dataLoad = dataLoad;
  }

  [HttpGet("/credit_score")]
  public IActionResult CalculateCreditScore(
    [FromForm] int age,
    [FromForm] string gender,
    [FromForm] int drivingExperience,
    [FromForm] string education,
    [FromForm] string income,
    [FromForm] int vehicleYear,
    [FromForm] string vehicleType,
    [FromForm] string annualMileage)
  {

    var creditScores = CreditScore.Calcular(age, drivingExperience, vehicleYear, vehicleType, gender, education, income, annualMileage, _dataLoad);
    return Ok(creditScores);
  }
}
