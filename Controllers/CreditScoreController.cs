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
    [FromHeader] int age,
    [FromHeader] string gender,
    [FromHeader] int drivingExperience,
    [FromHeader] string education,
    [FromHeader] string income,
    [FromHeader] int vehicleYear,
    [FromHeader] string vehicleType,
    [FromHeader] string annualMileage)
  {

    var creditScores = CreditScore.Calcular(age, gender, drivingExperience, education, income, vehicleYear, vehicleType, annualMileage, _dataLoad);
    return Ok(creditScores);
  }
}