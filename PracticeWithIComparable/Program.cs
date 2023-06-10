using static System.Console;

namespace PracticeWithIComparable
{
	internal class Program
	{
		static IEnumerable<Figure> SomeSquares => Enumerable.Range(1, 10).Select(i => new Figure(Random.Shared.Next(5, 50)));
		static void Main(string[] args)
		{
			List<Figure> list = SomeSquares.ToList();
			WriteLine("Список фигур до применения сортировки");
			foreach (Figure f in list)
			{
				WriteLine($"Квадрат с ребром: {f.Length} и площадью: {f.Square}");
			}

			list.Sort();
			list.Reverse();

			WriteLine("Список фигур после применения сортировки");
			foreach (Figure f in list)
			{
				WriteLine($"Квадрат с ребром: {f.Length} и площадью: {f.Square}");
			}
			ReadKey();
		}
	}
}