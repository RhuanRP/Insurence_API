using System.Text.Json.Serialization;

public class Pessoa
{
  public Pessoa(int age, string gender, int drivingExperience, string education, string income, int vehicleYear, string vehicleType, string annualMileage)
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
  public int Age { get; set; }
  public string Gender { get; set; }
  public int DrivingExperience { get; set; }
  public string Education { get; set; }
  public string Income { get; set; }
  public int VehicleYear { get; set; }
  public string VehicleType { get; set; }
  public string AnnualMileage { get; set; }

  public static string GetAgeRange(int age)
  {
    if (age < 16)
      return "";
    else if (age < 26)
      return "16-25";
    else if (age < 40)
      return "26-39";
    else if (age < 65)
      return "40-64";
    else
      return "65+";
  }

  public static string GetDrivingExperienceRange(int drivingExperience)
  {
    if (drivingExperience < 10)
      return "0-9y";
    else if (drivingExperience < 20)
      return "10-19y";
    else if (drivingExperience < 30)
      return "20-29y";
    else
      return "30y+";
  }
}