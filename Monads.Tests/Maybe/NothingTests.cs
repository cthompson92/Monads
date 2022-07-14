using Monads.Core.Maybe;

namespace Monads.Tests.Maybe;

[Trait("Category", "Unit")]
public class NothingTests : MaybeTests
{
	protected override IMaybe<T> Create <T>(T value) => new Nothing<T>();

	public static IEnumerable<object[]> GetIntTestCases() =>
		FunctorTests.GetIntValues().Select(x => new object[] { x });

	[Theory]
	[MemberData(nameof(GetIntTestCases))]
	public async Task MatchShouldAllowAsync(int value)
	{
		await ShouldAllowAsync(value, string.Empty, string.Empty);
	}

	[Theory]
	[MemberData(nameof(GetIntTestCases))]
	public void NothingShouldAllowLinqSyntax(int value)
	{
		MaybeShouldAllowLinqSyntax(value, string.Empty, string.Empty);
	}

	[Theory]
	[MemberData(nameof(GetIntTestCases))]
	public void NestedNothingShouldAllowLinqSyntax(int value)
	{
		NestedMaybeShouldAllowLinqSyntax(value, string.Empty, string.Empty);
	}

	[Theory]
	[MemberData(nameof(GetIntTestCases))]
	public void NothingShouldSelectCorrectValue(int value)
	{
		MaybeShouldSelectCorrectValue(value, string.Empty, string.Empty);
	}
}
