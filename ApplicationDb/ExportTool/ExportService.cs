using CsvHelper;
using EntityModels;
using System.Globalization;
using Newtonsoft.Json;

namespace ExportTool;

public class ExportService<T> where T : IPerson
{
	public string PathToFile { get; }
	public string CsvFileName { get; }

	public ExportService(string PathToFile, string CsvFileName)
	{
		this.PathToFile = PathToFile;
		this.CsvFileName = CsvFileName;
	}

	/// <summary>
	/// Метод экспортирования сущностей в CSV файл
	/// </summary>
	/// <param name="clients">Коллекция объектов, реализующих IPerson</param>
	public void ExportPersons(IEnumerable<T> clients)
	{
		// Создаём каталог для файла
		DirectoryInfo directory = new(PathToFile);
		if (!directory.Exists)
		{
			directory.Create();
		}
		using (FileStream fileStream = new(Path.Combine(PathToFile, CsvFileName), FileMode.Create))
		{
			using (StreamWriter streamWriter = new(fileStream))
			{
				using (CsvWriter writer = new(streamWriter, CultureInfo.InvariantCulture))
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

	/// <summary>
	/// Метод импортирования сущностей из CSV файла
	/// </summary>
	/// <returns>Коллекция объектов из CSV файла</returns>
	public IEnumerable<T>? ImportPersons()
	{
		using (FileStream fileStream = new(Path.Combine(PathToFile, CsvFileName), FileMode.OpenOrCreate))
		{
			using (StreamReader streamReader = new(fileStream))
			{
				using (CsvReader reader = new(streamReader, CultureInfo.InvariantCulture))
				{
					// Считываем из файла данные объекта Person
					return reader.GetRecords<T>().ToList();
				}
			}
		}
	}

	/// <summary>
	/// Метод сериализации объекта в JSON формат
	/// </summary>
	/// <param name="person">Сущность, реализующая IPerson</param>
	/// <param name="path">Путь к файлу</param>
	public static void SerializePerson(T person, string path)
	{
		FileInfo pathInfo = new(path);
		if (!pathInfo.Exists)
		{
			pathInfo.Create();
		}
		using (FileStream fileStream = new(path, FileMode.Create))
		{
			using (StreamWriter streamWriter = new(fileStream))
			{
				JsonSerializer jsonSerializer = new();
			    jsonSerializer.Serialize(streamWriter, person);
			}
		}
	}

	/// <summary>
	/// Метод десериализации объекта из JSON файла
	/// </summary>
	/// <param name="path">Путь к файлу</param>
	/// <returns>Объект, если файл существует</returns>
	public static T? DeSerializePerson(string path)
	{
		if (!(new FileInfo(path)).Exists)
			return default(T);
		return JsonConvert.DeserializeObject<T?>(File.ReadAllText(path));
	}
}