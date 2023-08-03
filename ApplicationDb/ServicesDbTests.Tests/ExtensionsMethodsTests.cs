namespace ServicesDbTests.Tests;

public static class IntExtensions
{
	public static TimeSpan Seconds(this int value) => TimeSpan.FromSeconds(value);
	public static TimeSpan Minutes(this int value) => TimeSpan.FromMinutes(value);
	public static TimeSpan Hours(this int value) => TimeSpan.FromHours(value);
	public static TimeSpan Days(this int value) => TimeSpan.FromDays(value);

}
public class ExtensionsMethodsTests
{
	[Fact]
	public void SecondsPositiveTest() => Assert.Equal(new TimeSpan(0, 0, 1), 1.Seconds());

	[Fact]
	public void SecondsNegativeTest() => Assert.Equal(new TimeSpan(0, 0, -5), -5.Seconds());

	[Fact]
	public void MinutesTest() => Assert.Equal(new TimeSpan(0, 3, 0), 3.Minutes());

	[Fact]
	public void HoursTest() => Assert.Equal(new TimeSpan(12, 0, 0), 12.Hours());

	[Fact]
	public void DaysTest() => Assert.Equal(new TimeSpan(12, 0, 0, 0), 12.Days());
}