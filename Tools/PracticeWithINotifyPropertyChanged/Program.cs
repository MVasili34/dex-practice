using System.ComponentModel;
using static System.Console;

namespace PracticeWithINotifyPropertyChanged;

internal class Program
{
	static void Main(string[] args)
	{
		//реализовать интерфейс INotifyPropertyChanged на
		//произвольном классе, продемонстрировать его работу
		PrpertChangeTest();

		//реализовать очередь, которая генерирует событие, когда кол - во
		//объектов в ней превышает n и событие, когда становится пустой
		QueueTesting();

		//реализовать класс анализирующий поток чисел, и если число
		//отличается более чем x -процентов выбрасывает событие
		IntStreamAnalyze(0.8);

		ReadKey();
	}

	static void PrpertChangeTest()
	{
		PropChangeTest test = new();
		test.PropertyChanged += PrintOut!;
		void PrintOut(object sender, PropertyChangedEventArgs e) => WriteLine("Свойтсво изменено: " + e.PropertyName);
		test.DogName = "NewDogName";
	}
	public static void PrintText(object sender, string Mes) => WriteLine($"Выброшено событие: {Mes}");
	static void QueueTesting()
	{
		Queue<int> queue = new();
		queue.Enqueue(1);
		EventQueue<int> test = new(queue);
		test.OnQueueExcessMessage += PrintText;
		test.AddInqueue(2);
		test.AddInqueue(3);

		WriteLine(test.GetOutQueue());
		WriteLine(test.GetOutQueue());
		WriteLine(test.GetOutQueue());
	}

	static void IntStreamAnalyze(double percent)
	{
		NumbersStreamAnalyze test = new(percent);
		test.OnNumbersExcess += PrintText!;
		test.AddElement(1);
		test.AddElement(10);
	}
}