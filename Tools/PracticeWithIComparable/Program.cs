using static System.Console;

namespace PracticeWithIComparable;

internal class Program
{
	static IEnumerable<Figure> squares => Enumerable.Range(1, 10)
        .Select(i => new Figure(Random.Shared.Next(5, 50)));
	static void Main(string[] args)
	{
        List<Figure> listOfSquares = squares.ToList();
        WriteLine("Список фигур до применения сортировки");
        foreach (Figure figure in listOfSquares)
        {
            WriteLine($"Квадрат с ребром: {figure.Length} и площадью: {figure.Square}");
        }
        listOfSquares.Sort();
        listOfSquares.Reverse();

        WriteLine("Список фигур после применения сортировки");
        foreach (Figure figure in listOfSquares)
        {
            WriteLine($"Квадрат с ребром: {figure.Length} и площадью: {figure.Square}");
        }
        ReadKey();
    }
}

public class Figure : IComparable<Figure>
{
    private int length;
    private int square;
    public Figure() { }
    public Figure(int Length)
    {
        this.Length = Length;
    }

    public int Square => square;

    public int Length
    {
        get => length;
        set
        {
            length = value;
            square = length * length;
        }
    }

    public int CompareTo(Figure other)
    {
        if (other is null)
            throw new ArgumentNullException("Other in not Figure object");
        return this.Square.CompareTo(other.Square);
    }
}