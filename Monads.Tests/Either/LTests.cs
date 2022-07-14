using Monads.Core.Either.Left;

namespace Monads.Tests.Either;

[Trait("Category", "Unit")]
public class LTests
{
	[Fact]
	public void LeftEitherShouldAllowLinqSyntax()
	{
		//arrange
		var l = new L<string, int>("error");

		//act
		EitherTests.EitherShouldAllowLinqSyntax(l);
	}

	public static IEnumerable<object[]> GetIntToStringTestCases()
	{
		return FunctorTests.GetIntValues().Select(x => new object[] { x, x.ToString() });
	}

	[Theory]
	[MemberData(nameof(GetIntToStringTestCases))]
	public void LeftEitherShouldSelectCorrectError(int value, string expected)
	{
		//arrange
		var l = new L<int, string>(value);

		//act
		EitherTests.EitherShouldSelectCorrectError(l, expected);
	}

	[Theory]
	[InlineData("")]
	[InlineData("foo")]
	[InlineData("bar")]
	[InlineData("corge")]
	[InlineData("antidisestablishmentarianism")]
	public void LeftEitherObeysFunctorIdentityLawByValue(string value)
	{
		//arrange
		var l = new L<object, string>(value);

		//act
		EitherTests.FunctorLaws.IdentityLaw(l);
	}

	[Fact]
	public void LeftEitherObeysFunctorIdentityLawByReference()
	{
		//arrange
		var l = new L<object, object>(new object());

		//act
		EitherTests.FunctorLaws.IdentityLaw(l);
	}

	[Fact]
	public void LeftEitherObeysFunctorTransitiveLaw()
	{
		//arrange
		var l = new L<object, object>(new object());

		//act
		EitherTests.FunctorLaws.TransitiveLaw(
			l,
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
	public void LeftEitherObeysBifunctorIdentityLawsByValue(string value)
	{
		//arrange
		var l = new L<object, string>(value);

		//act
		EitherTests.BifunctorLaws.IdentityLaws(l);
	}

	[Fact]
	public void LeftEitherObeysBifunctorIdentityLawsByReference()
	{
		//arrange
		var l = new L<object, object>(new object());

		//act
		EitherTests.BifunctorLaws.IdentityLaws(l);
	}

	[Fact]
	public void LeftEitherObeysBifunctorConsistencyLaw()
	{
		//arrange
		var l = new L<object, object>(new object());

		//act
		EitherTests.BifunctorLaws.ConsistencyLaws(
			l,
			x => x.ToString(),
			x => x.ToString());
	}

	[Fact]
	public void LeftEitherObeysBifunctorCompositionLaws()
	{
		//arrange
		var l = new L<object, object>(new object());

		//act
		EitherTests.BifunctorLaws.CompositionLaws(
			l,
			x => x.ToString()!,
			x => x.Length,
			x => x.ToString()!,
			x => x.Length + 1);
	}
}
