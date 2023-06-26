using static System.Console;

namespace PracticeWithIComparable;

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

public class Figure : IComparable<Figure>
{
    private int length;
    private int square;
    public Figure() { }
    public Figure(int length)
    {
        Length = length;
        square = length * length;
    }

    public int Square
    {
        get => square;
    }

    public int Length
    {
        get => length;
        set
        {
            length = value;
            square = length * length;
        }
    }

    public int CompareTo(Figure? other)
    {
        if (other is null)
        {
            throw new ArgumentNullException("Other in not Figure object");
        }
        else
            return this.Square.CompareTo(other.Square);
    }
}