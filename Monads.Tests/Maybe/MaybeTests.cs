using FluentAssertions;
using Monads.Core.Maybe;

namespace Monads.Tests.Maybe;

public abstract class MaybeTests
{
	protected abstract IMaybe<T> Create<T>(T value)
		where T: notnull;

	protected async Task ShouldAllowAsync(int value, string nothing, string expected)
	{
		//arrange
		var m = Create(value);

		Task<string> FooAsync(int x)
		{
			return Task.FromResult(x.ToString());
		}

		//act
		var result = await m.MatchAsync(nothing, async x => await FooAsync(x));
			
		//assert
		result.Should().Be(expected);
	}
	
	protected void MaybeShouldAllowLinqSyntax(int value, string nothing, string expected)
	{
		//arrange
		var m = Create(value);

		//act
		var result = FunctorTests.FunctorAllowsLinqSyntax(m);

		//assert
		result.Match(nothing, x => x).Should().Be(expected);
	}
	
	protected void NestedMaybeShouldAllowLinqSyntax(int value, string nothing, string expected)
	{
		//arrange
		var nested = Create(Create(value));

		//act
		var result = FunctorTests.NestedFunctorAllowsLinqSyntax(nested);

		//assert
		result.Match(nothing, x => x).Should().Be(expected);
	}

	public static IEnumerable<object[]> GetIntToStringTestCases()
	{
		return FunctorTests.GetIntValues().Select(x => new object[] { x, x.ToString() });
	}

	protected void MaybeShouldSelectCorrectValue(int value, string nothing, string expected)
	{
		//arrange
		var result = Create(value);

		//act
		FunctorTests.MaybeShouldSelectCorrectValue(result, nothing, expected);
	}

	[Theory]
	[InlineData("")]
	[InlineData("foo")]
	[InlineData("bar")]
	[InlineData("corge")]
	[InlineData("antidisestablishmentarianism")]
	public void MaybeShouldObeyFunctorIdentityLawByValue(string value)
	{
		//arrange
		var m = Create(value);

		//act
		FunctorTests.IdentityLaw(m);
	}

	[Fact]
	public void MaybeShouldObeyFunctorIdentityLawByReference()
	{
		//arrange
		var m = Create(new object());

		//act
		FunctorTests.IdentityLaw(m);
	}

	[Fact]
	public void MaybeShouldObeyFunctorTransitiveLaw()
	{
		//arrange
		var result = Create(new object());

		//act
		FunctorTests.TransitiveLaw(
			result,
			x => x.ToString()!,
			x => x.Length + 1);
	}
}
