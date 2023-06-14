using CsvHelper;
using EntityModels;
using System.Globalization;

namespace ExportTool
{
	public class ExportService
	{
		private string _pathToDirecory { get; set; }
		private string _csvFileName { get; set; }
		public ExportService(string pathToDirectory, string csvFileName)
		{
			_pathToDirecory = pathToDirectory;
			_csvFileName = csvFileName;
		}

		public void ExportClients(IEnumerable<Client> clients)
		{
			// Создаём каталог для файла
			DirectoryInfo dirInfo = new DirectoryInfo(_pathToDirecory);
			if (!dirInfo.Exists)
			{
				dirInfo.Create();
			}
			using (FileStream fileStream = new FileStream(Path.Combine(_pathToDirecory, _csvFileName),
				FileMode.OpenOrCreate))
			{
				using (StreamWriter streamWriter = new StreamWriter(fileStream))
				{
					using (var writer = new CsvWriter(streamWriter, CultureInfo.InvariantCulture))
					{
						// Формируем заголовки будущей таблицы
						writer.WriteHeader<Client>();
						writer.NextRecord();
						writer.WriteRecords(clients);
						writer.NextRecord();
						// Очищает буфер, который был задействован CsvWriter
						writer.Flush();
					}
				}
			}
		}

		public IEnumerable<Client>? ImportClients()
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
						return reader.GetRecords<Client>().ToList();
					}
				}
			}
		}
	}
}