using FluentAssertions;
using Monads.Core.Results;

namespace Monads.Tests.Results;

[Trait("Category", "Unit")]
public class ResultTests
{
	[Fact]
	public void ResultShouldAllowLinqSyntax()
	{
		//arrange
		var result = new Result<int>(17);

		//act
		var selected = FunctorTests.FunctorAllowsLinqSyntax(result);

		//assert
		selected.Error.Should().Be("17");
	}

	[Fact]
	public void NestedResultShouldAllowLinqSyntax()
	{
		//arrange
		var result = Result.Return(new Result<int>(17));

		//act
		var selected = FunctorTests.NestedFunctorAllowsLinqSyntax(result);

		//assert
		selected.Error.Should().Be("17");
	}

	public static IEnumerable<object[]> GetIntToStringTestCases()
	{
		return FunctorTests.GetIntValues().Select(x => new object[] { x, x.ToString() });
	}

	[Theory]
	[MemberData(nameof(GetIntToStringTestCases))]
	public void ResultShouldSelectCorrectValue(int value, string expected)
	{
		//arrange
		var result = new Result<int>(value);

		//act
		FunctorTests.ResultShouldSelectCorrectValue(result, expected);
	}

	[Theory]
	[InlineData("")]
	[InlineData("foo")]
	[InlineData("bar")]
	[InlineData("corge")]
	[InlineData("antidisestablishmentarianism")]
	public void ResultShouldObeyFunctorIdentityLawByValue(string value)
	{
		//arrange
		var result = new Result<string>(value);

		//act
		FunctorTests.IdentityLaw(result);
	}

	[Fact]
	public void ResultShouldObeyFunctorIdentityLawByReference()
	{
		//arrange
		var result = new Result<object>(new object());

		//act
		FunctorTests.IdentityLaw(result);
	}

	[Fact]
	public void ResultShouldObeyFunctorTransitiveLaw()
	{
		//arrange
		var result = new Result<object>(new object());

		//act
		FunctorTests.TransitiveLaw(
			result,
			x => x.ToString()!,
			x => x.Length + 1);
	}
}
