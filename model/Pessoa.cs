using System.Text.Json.Serialization;

public class Pessoa
{
  public Pessoa(string age, string gender, string drivingExperience, string education, string income, string vehicleYear, string vehicleType, string annualMileage)
  {
    Age = age;
    Gender = gender;
    DrivingExperience = drivingExperience;
    Education = education;
    Income = income;
    VehicleYear = vehicleYear;
    VehicleType = vehicleType;
    AnnualMileage = annualMileage;
  }
  public string Age { get; set; }
  public string Gender { get; set; }
  public string DrivingExperience { get; set; }
  public string Education { get; set; }
  public string Income { get; set; }
  public string VehicleYear { get; set; }
  public string VehicleType { get; set; }
  public string AnnualMileage { get; set; }

}