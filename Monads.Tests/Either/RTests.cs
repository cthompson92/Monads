using Monads.Core.Either.Right;

namespace Monads.Tests.Either;

[Trait("Category", "Unit")]
public class RTests
{
	[Fact]
	public void RightEitherShouldAllowLinqSyntax()
	{
		//arrange
		var r = new R<string, int>(5);

		//act
		EitherTests.EitherShouldAllowLinqSyntax(r);
	}

	public static IEnumerable<object[]> GetIntToStringTestCases()
	{
		return FunctorTests.GetIntValues().Select(x => new object[] { x, x.ToString() });
	}

	[Theory]
	[MemberData(nameof(GetIntToStringTestCases))]
	public void RightEitherShouldSelectCorrectValue(int value, string expected)
	{
		//arrange
		var r = new R<string, int>(value);

		//act
		EitherTests.EitherShouldSelectCorrectValue(r, expected);
	}

	[Theory]
	[MemberData(nameof(GetIntToStringTestCases))]
	public void RightEitherShouldSelectRightCorrectValue(int value, string expected)
	{
		//arrange
		var r = new R<string, int>(value);

		//act
		EitherTests.EitherShouldSelectRightCorrectValue(r, expected);
	}

	[Theory]
	[InlineData("")]
	[InlineData("foo")]
	[InlineData("bar")]
	[InlineData("corge")]
	[InlineData("antidisestablishmentarianism")]
	public void RightEitherObeysFunctorIdentityLawByValue(string value)
	{
		//arrange
		var r = new R<object, string>(value);

		//act
		EitherTests.FunctorLaws.IdentityLaw(r);
	}

	[Fact]
	public void RightEitherObeysFunctorIdentityLawByReference()
	{
		//arrange
		var r = new R<object, object>(new object());

		//act
		EitherTests.FunctorLaws.IdentityLaw(r);
	}

	[Fact]
	public void RightEitherObeysFunctorTransitiveLaw()
	{
		//arrange
		var r = new R<object, object>(new object());

		//act
		EitherTests.FunctorLaws.TransitiveLaw(
			r,
			x => x.ToString()!,
			x => x.Length,
			x => x.ToString()!,
			x => x.Length + 1);
	}

	[Theory]
	[InlineData("")]
	[InlineData("foo")]
	[InlineData("bar")]
	[InlineData("corge")]
	[InlineData("antidisestablishmentarianism")]
	public void RightEitherObeysBifunctorIdentityLawsByValue(string value)
	{
		//arrange
		var r = new R<object, string>(value);

		//act
		EitherTests.BifunctorLaws.IdentityLaws(r);
	}

	[Fact]
	public void RightEitherObeysBifunctorIdentityLawsByReference()
	{
		//arrange
		var r = new R<object, object>(new object());

		//act
		EitherTests.BifunctorLaws.IdentityLaws(r);
	}

	[Fact]
	public void RightEitherObeysBifunctorConsistencyLaw()
	{
		//arrange
		var r = new R<object, object>(new object());

		//act
		EitherTests.BifunctorLaws.ConsistencyLaws(
			r,
			x => x.ToString(),
			x => x.ToString());
	}

	[Fact]
	public void RightEitherObeysBifunctorCompositionLaws()
	{
		//arrange
		var r = new R<object, object>(new object());

		//act
		EitherTests.BifunctorLaws.CompositionLaws(
			r,
			x => x.ToString()!,
			x => x.Length,
			x => x.ToString()!,
			x => x.Length + 1);
	}
}
