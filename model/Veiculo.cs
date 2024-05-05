public class Veiculo
{

  public static string GetAnoVeiculo(int vehicleYear)
  {
    if (vehicleYear < 2015)
      return "before 2015";
    else
      return "after 2015";
  }
}