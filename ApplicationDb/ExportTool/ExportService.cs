using CsvHelper;
using EntityModels;
using System.Globalization;
using Newtonsoft.Json;

namespace ExportTool
{
	public class ExportService<T> where T : IPerson
	{
		private string _pathToDirecory { get; set; }
		private string _csvFileName { get; set; }
		public ExportService(string pathToDirectory, string csvFileName)
		{
			_pathToDirecory = pathToDirectory;
			_csvFileName = csvFileName;
		}

		public void ExportPersons(IEnumerable<T> clients)
		{
			// Создаём каталог для файла
			DirectoryInfo dirInfo = new DirectoryInfo(_pathToDirecory);
			if (!dirInfo.Exists)
			{
				dirInfo.Create();
			}
			using (FileStream fileStream = new FileStream(Path.Combine(_pathToDirecory, _csvFileName),
				FileMode.Create))
			{
				using (StreamWriter streamWriter = new StreamWriter(fileStream))
				{
					using (var writer = new CsvWriter(streamWriter, CultureInfo.InvariantCulture))
					{
						// Формируем заголовки будущей таблицы
						writer.WriteHeader<T>();
						writer.NextRecord();
						writer.WriteRecords(clients);
						writer.NextRecord();
						// Очищает буфер, который был задействован CsvWriter
						writer.Flush();
					}
				}
			}
		}

		public IEnumerable<T>? ImportPersons()
		{
			using (FileStream fileStream = new FileStream(Path.Combine(_pathToDirecory, _csvFileName),
			FileMode.OpenOrCreate))
			{
				using (StreamReader streamReader = new StreamReader(fileStream))
				{
					using (var reader = new CsvReader(streamReader,
					CultureInfo.InvariantCulture))
					{
						// Считываем из файла данные объекта Person
						return reader.GetRecords<T>().ToList();
					}
				}
			}
		}

		public static void SerializePerson(T person, string path)
		{
			FileInfo patInfo = new(path);
			if (!patInfo.Exists)
			{
				patInfo.Create();
			}
			using (FileStream fileStream = new FileStream(path, FileMode.Create))
			{
				using (StreamWriter streamWriter = new StreamWriter(fileStream))
				{
					JsonSerializer jsonSerializer = new();
				    jsonSerializer.Serialize(streamWriter, person);
				}
			}
		}

		public static Task<T?>? DeSerializePerson(string path) => !(new FileInfo(path)).Exists ? null:
			Task.FromResult(JsonConvert.DeserializeObject<T?>(File.ReadAllText(path)));
	}
}