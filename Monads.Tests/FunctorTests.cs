using FluentAssertions;
using Monads.Core.Maybe;
using Monads.Core.Results;

namespace Monads.Tests;

internal static class FunctorTests
{
	private static readonly Random Random = new Random();

	internal static IEnumerable<int> GetIntValues()
	{
		for (var i = 0; i < 50; i++)
		{
			yield return Random.Next();
		}
	}

	internal static IResult<string> FunctorAllowsLinqSyntax<T>(
		IResult<T> result)
	{
		return from x in result
				select x.ToString();
	}

	internal static IMaybe<string> FunctorAllowsLinqSyntax<T>(
		IMaybe<T> result)
		where T: notnull
	{
		return from x in result
				select x.ToString();
	}

	internal static IResult<string> NestedFunctorAllowsLinqSyntax<T>(
		IResult<IResult<T>> result)
	{
		return from x in result
				from y in x
				select y.ToString();
	}

	internal static IMaybe<string> NestedFunctorAllowsLinqSyntax<T>(
		IMaybe<IMaybe<T>> result)
		where T : notnull
	{
		return from x in result
				from y in x
				select y.ToString();
	}

	internal static void ResultShouldSelectCorrectValue<T>(
		IResult<T> result, string expected)
	{
		//act
		var str = result.Select(x => x!.ToString());

		//assert
		str.Error.Should().Be(expected);
	}

	internal static void MaybeShouldSelectCorrectValue<T>(
		IMaybe<T> result, string nothing, string expected)
		where T : notnull
	{
		//act
		var str = result.Select(x => x.ToString()!);

		//assert
		str.Match(nothing, x => x).Should().Be(result.IsJust() ? expected : nothing);
	}

	/// <summary>
	/// Tests the provided <paramref name="result"/> value for
	/// adherence to the first functor law.
	/// </summary>
	internal static void IdentityLaw<T>(
		IResult<T> result)
	{
		Func<T, T> id = x => x;

		result.Should().Be(result.Select(id));
	}

	/// <summary>
	/// Tests the provided <paramref name="m"/> value for
	/// adherence to the first functor law.
	/// </summary>
	internal static void IdentityLaw<T>(
		IMaybe<T> m)
		where T : notnull
	{
		Func<T, T> id = x => x;

		m.Should().Be(m.Select(id));
	}

	/// <summary>
	/// Tests the provided <paramref name="result"/> value for
	/// adherence to the second functor law.
	/// </summary>
	internal static void TransitiveLaw<T1, T2, T3>(
		IResult<T1> result,
		Func<T1, T2> f,
		Func<T2, T3> g)
	{
		var result1 = result.Select(f).Select(g);
		var result2 = result.Select(x => g(f(x)));

		result1.Should().Be(result2);
	}

	/// <summary>
	/// Tests the provided <paramref name="m"/> value for
	/// adherence to the second functor law.
	/// </summary>
	internal static void TransitiveLaw<T1, T2, T3>(
		IMaybe<T1> m,
		Func<T1, T2> f,
		Func<T2, T3> g)
		where T1 : notnull
		where T2 : notnull
		where T3 : notnull
	{
		var result1 = m.Select(f).Select(g);
		var result2 = m.Select(x => g(f(x)));

		result1.Should().Be(result2);
	}
}
