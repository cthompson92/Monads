using FluentAssertions;
using Monads.Core.Either;

namespace Monads.Tests.Either;

internal static class EitherTests
{
	internal static IEither<TLeft, string> EitherShouldAllowLinqSyntax<TLeft, TRight>(
			IEither<TLeft, TRight> either)
	{
		return from x in either
			   select x.ToString();
	}

	internal static IEither<L1, string> NestedEitherShouldAllowLinqSyntax<L1, R1>(
			IEither<L1, IEither<L1, R1>> left, IEither<L1, IEither<L1, R1>> right)
	{
		return from x in left
			   from y in right
			   select x.ToString() + y.ToString();
	}

	internal static void EitherShouldSelectCorrectValue<TLeft, TRight>(
		IEither<TLeft, TRight> either, string expected)
	{
		Func<TRight, string> selector = x => x?.ToString() ?? string.Empty;

		//act
		var result = either.Select(selector);

		//assert
		result.GetValueOrFail().Should().Be(expected);
	}

	internal static void EitherShouldSelectRightCorrectValue<TLeft, TRight>(
		IEither<TLeft, TRight> either, string expected)
	{
		Func<TRight, string> selector = x => x?.ToString() ?? string.Empty;

		//act
		var result = either.SelectRight(selector);

		//assert
		result.GetValueOrFail().Should().Be(expected);
	}

	internal static void EitherShouldSelectCorrectError<TLeft, TRight>(
		IEither<TLeft, TRight> either, string expected)
	{
		Func<TLeft, string> selector = x => x?.ToString() ?? string.Empty;

		//act
		var result = either.SelectLeft(selector);

		//assert
		result.GetErrorOrFail().Should().Be(expected);
	}

	internal static class FunctorLaws
	{
		/// <summary>
		/// Tests the provided <paramref name="either"/> value for
		/// adherence to the first functor law.
		/// </summary>
		internal static void IdentityLaw<TLeft, TRight>(
			IEither<TLeft, TRight> either)
		{
			Func<TRight, TRight> id = x => x;

			either.Should().Be(either.Select(id));
		}

		/// <summary>
		/// Tests the provided <paramref name="either"/> value for
		/// adherence to the second functor law.
		/// </summary>
		internal static void TransitiveLaw<L1, L2, L3, R1, R2, R3>(
			IEither<L1, R1> either,
			Func<L1, L2> f,
			Func<L2, L3> g,
			Func<R1, R2> h,
			Func<R2, R3> i)
		{
			either.SelectLeft(f).SelectLeft(g)
				.Should().Be(either.SelectLeft(x => g(f(x))));

			either.SelectRight(h).SelectRight(i)
				.Should().Be(either.SelectRight(y => i(h(y))));
		}
	}

	internal static class BifunctorLaws
	{
		internal static void IdentityLaws<TLeft, TRight>(
			IEither<TLeft, TRight> either)
		{
			either.Should().Be(either.SelectBoth(x => x, x => x));
		}

		internal static void ConsistencyLaws<L1, L2, R1, R2>(
			IEither<L1, R1> either,
			Func<L1, L2> f,
			Func<R1, R2> g)
		{
			//Func<L1, string> f = x => x.ToString();
			//Func<R1, string> g = x => x.ToString();

			var combinedResult = either.SelectBoth(f, g);

			either.SelectLeft(f).SelectRight(g).Should().Be(combinedResult);
			either.SelectRight(g).SelectLeft(f).Should().Be(combinedResult);
		}

		internal static void CompositionLaws<L1, L2, L3, R1, R2, R3>(
			IEither<L1, R1> either,
			Func<L1, L2> g,
			Func<L2, L3> f,
			Func<R1, R2> i,
			Func<R2, R3> h)
		{
			//Func<L1, string> g = x => x.ToString();
			//Func<R1, string> i = x => x.ToString();

			//Func<string, int> f = x => x.Length;
			//Func<string, int> h = x => x.Length + 1;

			either.SelectBoth(x => f(g(x)), y => h(i(y)))
				.Should().Be(either.SelectBoth(g, i).SelectBoth(f, h));
		}
	}
}
