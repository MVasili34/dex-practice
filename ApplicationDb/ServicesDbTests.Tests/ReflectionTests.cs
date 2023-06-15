using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ServicesDbTests.Tests;

public class Triangle
{
	private double square;
	private int a;
	private int b;
	private int c;
	public Triangle() { }
	public Triangle(int a, int b, int c)
	{
		square = CalculateSquare();
	}

	private double Square
	{
		get => square;
	}
	private double CalculateSquare()
	{
		double p = (a + b + c) / 2.0;
		return Math.Sqrt(p * (p - a) * (p - b) * (p - c));
	}

	public double CalculateSquare(int a2, int b2, int c2)
	{
		double p = (a2 + b2 + c2) / 2.0;
		return Math.Sqrt(p * (p - a2) * (p - b2) * (p - c2));
	}

	public int A
	{
		get => a;
		set
		{
			a = value;
			square = CalculateSquare();
		}
	}
	public int B
	{
		get => b;
		set
		{
			b = value;
			square = CalculateSquare();
		}
	}
	public int C
	{
		get => c;
		set
		{
			c = value;
			square = CalculateSquare();
		}
	}
}
public class ReflectionTests
{

	private object? GetInstanceObject(string? target)
	{
		if (target is not null)
		{
			Type? type = Type.GetType(target);
			if (type is not null)
			{
				return Activator.CreateInstance(type);
			}
		}
		return null;
	}

	[Fact] //тест инициализации объекта
	public void InstantiationByNameTest()
	{
		object? instance = GetInstanceObject("ServicesDbTests.Tests.Triangle");
		if (instance is not null)
		{
			Assert.Equal(typeof(Triangle), instance.GetType());
		}
		else
		{
			Assert.Fail("Экземпляр класса null!)");
		}
	}
	[Fact] //тест вызова перегруженного метода объекта
	public void CallingMethodByNameTest()
	{
		object? instance = GetInstanceObject("ServicesDbTests.Tests.Triangle");
		if (instance is not null)
		{
			MethodInfo methodInfo = instance.GetType().GetMethod("CalculateSquare",
					BindingFlags.Public | BindingFlags.Instance, new Type[] {
						typeof(int), typeof(int), typeof(int) })!;
			Assert.Equal(6.0, methodInfo.Invoke(instance, new object[] { 3, 4, 5 }));
		}
		else
		{
			Assert.Fail("Экземпляр класса null!)");
		}
	}

	[Fact] //тест обращения к закрытому свойству класса
	public void GetPrivatePropertiesTest()
	{
		string className = "ServicesDbTests.Tests.Triangle";
		Type type = Type.GetType(className)!;

		if (type is not null)
		{
			object instance = Activator.CreateInstance(type)!;
			if (instance is not null)
			{
				var propertyInfo = type.GetProperties(BindingFlags.NonPublic | BindingFlags.Instance);
				Assert.Equal(0.0, propertyInfo.First().GetValue(instance)!);
			}
		}
	}

}
