using System.ComponentModel;
using static System.Console;

namespace PracticeWithINotifyPropertyChanged;

internal class Program
{
	static void Main(string[] args)
	{
		PropertyChangeTest();

		QueueTesting();

		IntStreamAnalyze(0.8);

		ReadKey();
	}

	static void PropertyChangeTest()
	{
		PropChangeTest changeTest = new();
		changeTest.PropertyChanged += PrintOut!;
		void PrintOut(object sender, PropertyChangedEventArgs e) => WriteLine("Свойтсво изменено: " + e.PropertyName);
		changeTest.DogName = "NewDogName";
	}
	public static void PrintText(object sender, string Mes) => WriteLine($"Выброшено событие: {Mes}");
	static void QueueTesting()
	{
		Queue<int> queue = new();
		queue.Enqueue(1);
		EventQueue<int> excessTest = new(queue);
		excessTest.OnQueueExcessMessage += PrintText;
		excessTest.AddInqueue(2);
		excessTest.AddInqueue(3);

		WriteLine(excessTest.GetOutQueue());
		WriteLine(excessTest.GetOutQueue());
		WriteLine(excessTest.GetOutQueue());
	}

	static void IntStreamAnalyze(double percent)
	{
		NumbersStreamAnalyze analyzeTest = new(percent);
		analyzeTest.OnNumbersExcess += PrintText!;
		analyzeTest.AddElement(1);
		analyzeTest.AddElement(10);
	}
}