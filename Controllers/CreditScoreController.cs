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

    Pessoa pessoa = new Pessoa(age, gender, drivingExperience, education, income, vehicleYear, vehicleType, annualMileage);
    string ageRange = Pessoa.GetAgeRange(age);
    string drivingExperienceRange = Pessoa.GetDrivingExperienceRange(drivingExperience);

    string vehicleYearRange = vehicleYear < 2015 ? "before 2015" : "after 2015";

    List<Data> data = _dataLoad.Search();
    var filteredData = data.Where(x => x.GENDER == gender && x.DRIVING_EXPERIENCE == drivingExperienceRange && x.EDUCATION == education && x.INCOME == income && x.VEHICLE_TYPE == vehicleType && x.VEHICLE_YEAR == vehicleYearRange && x.ANNUAL_MILEAGE == annualMileage && x.AGE == ageRange);

    var creditScores = filteredData.Select(x => x.CREDIT_SCORE);

    return Ok(creditScores);
  }


}
