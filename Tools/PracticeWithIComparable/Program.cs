﻿using static System.Console;

namespace PracticeWithIComparable;

internal class Program
{
	static void Main(string[] args)
	{
        List<Figure> listOfSquares = GenerateFigures().ToList();
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
    private static IEnumerable<Figure> GenerateFigures() => Enumerable.Range(1, 10)
        .Select(i => new Figure(Random.Shared.Next(5, 50)));
}

public class Figure : IComparable<Figure>
{
    private int _length;
    private int _square;
    public Figure() { }
    public Figure(int length)
    {
        Length = length;
    }

    public int Square => _square;

    public int Length
    {
        get => _length;
        set
        {
            _length = value;
            _square = _length * _length;
        }
    }

    public int CompareTo(Figure other)
    {
        if (other is null)
            throw new ArgumentNullException("Other is not Figure object");
        return this.Square.CompareTo(other.Square);
    }
}