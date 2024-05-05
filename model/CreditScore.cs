public class CreditScore
{
  public static IEnumerable<string> Calcular(int age, int drivingExperience, int vehicleYear, string vehicleType, string gender, string education, string income, string annualMileage, IDataLoad _dataLoad)
  {
    string ageRange = Pessoa.GetIdade(age);
    string drivingExperienceRange = Pessoa.GetExperiencia(drivingExperience);
    string vehicleYearRange = Veiculo.GetAnoVeiculo(vehicleYear);

    List<Data> data = _dataLoad.Search();
    var filteredData = data.Where(x => x.GENDER == gender && x.DRIVING_EXPERIENCE == drivingExperienceRange && x.EDUCATION == education && x.INCOME == income && x.VEHICLE_TYPE == vehicleType && x.VEHICLE_YEAR == vehicleYearRange && x.ANNUAL_MILEAGE == annualMileage && x.AGE == ageRange);

    return filteredData.Select(x => x.CREDIT_SCORE);
  }
}