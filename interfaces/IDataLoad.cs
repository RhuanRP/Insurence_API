public interface IDataLoad
{
  List<Data> Search();
  List<T> Load<T>(string local);
}