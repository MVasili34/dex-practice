using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ServicesDbTests.Tests;

public class ReflectionTests
{
	/// <summary>
	/// Метод создания экземпляра класса по текстову имени
	/// </summary>
	/// <param name="target">Строка с названием класса</param>
	/// <returns>Объект класса object, иначе null</returns>
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

	[Fact]
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
	[Fact]
	public void CallingOverloadedMethodTest()
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

	[Fact]
	public void GetPrivatePropertiesTest()
	{
		string className = "ServicesDbTests.Tests.Triangle";
		Type type = Type.GetType(className)!;

		if (type is not null)
		{
			object? instance = Activator.CreateInstance(type);
			if (instance is not null)
			{
				var propertyInfo = type.GetProperties(BindingFlags.NonPublic | BindingFlags.Instance);
				Assert.Equal(0.0, propertyInfo.First().GetValue(instance)!);
			}
		}
	}
}

public class Triangle
{
    private double square;
    private int sideA;
    private int sideB;
    private int sideC;
    public Triangle() { }
    public Triangle(int sideA, int sideB, int sideC)
    {
        this.sideA = sideA;
        this.sideB = sideB;
        this.sideC = sideC;
        this.square = CalculateSquare();
    }

    private double Square
    {
        get => square;
    }
    private double CalculateSquare()
    {
        return CalculateSquare(sideA, sideB, sideC);
    }

    public double CalculateSquare(int a2, int b2, int c2)
    {
        double p = (a2 + b2 + c2) / 2.0;
        return Math.Sqrt(p * (p - a2) * (p - b2) * (p - c2));
    }

    public int SideA
    {
        get => sideA;
        set
        {
            sideA = value;
            square = CalculateSquare();
        }
    }
    public int SideB
    {
        get => sideB;
        set
        {
            sideB = value;
            square = CalculateSquare();
        }
    }
    public int SideC
    {
        get => sideC;
        set
        {
            sideC = value;
            square = CalculateSquare();
        }
    }
}
