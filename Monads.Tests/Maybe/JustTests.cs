using Monads.Core.Maybe;

namespace Monads.Tests.Maybe;

[Trait("Category", "Unit")]
public class JustTests : MaybeTests
{
	protected override IMaybe<T> Create <T>(T value) => Just.Return(value);

	[Theory]
	[MemberData(nameof(GetIntToStringTestCases))]
	public async Task MatchShouldAllowAsync(int value, string expected)
	{
		await ShouldAllowAsync(value, string.Empty, expected);
	}

	[Theory]
	[MemberData(nameof(GetIntToStringTestCases))]
	public void JustShouldAllowLinqSyntax(int value, string expected)
	{
		MaybeShouldAllowLinqSyntax(value, string.Empty, expected);
	}

	[Theory]
	[MemberData(nameof(GetIntToStringTestCases))]
	public void NestedJustShouldAllowLinqSyntax(int value, string expected)
	{
		NestedMaybeShouldAllowLinqSyntax(value, string.Empty, expected);
	}

	[Theory]
	[MemberData(nameof(GetIntToStringTestCases))]
	public void JustShouldSelectCorrectValue(int value, string expected)
	{
		MaybeShouldSelectCorrectValue(value, string.Empty, expected);;
	}
}
