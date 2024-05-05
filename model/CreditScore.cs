public class CreditScore
{
  public static IEnumerable<string> Calcular(int age, string gender, int drivingExperience, string education, string income, int vehicleYear, string vehicleType, string annualMileage, List<Data> dataset)
  {
    string ageRange = Pessoa.GetIdade(age);
    string drivingExperienceRange = Pessoa.GetExperiencia(drivingExperience);
    string vehicleYearRange = Veiculo.GetAnoVeiculo(vehicleYear);


    var filteredData = dataset.Where(x => x.GENDER == gender && x.DRIVING_EXPERIENCE == drivingExperienceRange && x.EDUCATION == education && x.INCOME == income && x.VEHICLE_TYPE == vehicleType && x.VEHICLE_YEAR == vehicleYearRange && x.ANNUAL_MILEAGE == annualMileage && x.AGE == ageRange);

    return filteredData.Select(x => x.CREDIT_SCORE);
  }
}