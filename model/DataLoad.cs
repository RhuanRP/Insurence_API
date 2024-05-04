using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

public class DataLoad : IDataLoad
{
  public List<Data> Search()
  {
    return Load<Data>(".\\data\\Car_Insurance_Claim");
  }
  public List<T> Load<T>(string local)
  {
    local = local + ".csv";
    if (!File.Exists(local))
      throw new ArgumentException(local);

    using (var reader = new StreamReader(local))
    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
    {
      return csv.GetRecords<T>().ToList();
    }
  }
}