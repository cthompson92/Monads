using Monads.Core.Either.Left;
using Monads.Core.Either.Right;

namespace Monads.Core.Either;

public static class EitherExtensions
{
	/// <summary>
	/// Converts the <paramref name="source"/> into a new form, handling both the
	/// left and right cases.
	/// </summary>
	/// <typeparam name="TLeft">The original error type.</typeparam>
	/// <typeparam name="TNewLeft">The new error type.</typeparam>
	/// <typeparam name="TRight">The original value type.</typeparam>
	/// <typeparam name="TNewRight">The new value type.</typeparam>
	/// <param name="source">The either to be transformed.</param>
	/// <param name="leftSelector">
	/// Mapping function to determine the new error state, if the <paramref name="source"/> is left.
	/// </param>
	/// <param name="rightSelector">
	/// Mapping function to determine the new value state, if the <paramref name="source"/> is right.
	/// </param>
	/// <returns></returns>
	/// <exception cref="InvalidOperationException"></exception>
	public static IEither<TNewLeft, TNewRight> SelectBoth<TLeft, TNewLeft, TRight, TNewRight>(
		this IEither<TLeft, TRight> source,
		Func<TLeft, TNewLeft> leftSelector,
		Func<TRight, TNewRight> rightSelector)
	{
		return source switch
		{
			R<TLeft, TRight> success
				=> new R<TNewLeft, TNewRight>(rightSelector(success.Value)),

			L<TLeft, TRight> failure
				=> new L<TNewLeft, TNewRight>(leftSelector(failure.Error)),

			_ => throw new InvalidOperationException("Result type was not understood."),
		};
	}

	/// <summary>
	/// Converts the <paramref name="source"/> into a new form if it is in the right state.
	/// </summary>
	/// <typeparam name="TLeft">The error type.</typeparam>
	/// <typeparam name="TRight">The original value type.</typeparam>
	/// <typeparam name="TNewRight">The new value type.</typeparam>
	/// <param name="source">The either to be transformed.</param>
	/// <param name="selector">
	/// Mapping function to determine the new value state, if the <paramref name="source"/> is right.
	/// </param>
	/// <returns></returns>
	/// <exception cref="InvalidOperationException"></exception>
	public static IEither<TLeft, TNewRight> Select<TLeft, TRight, TNewRight>(
		this IEither<TLeft, TRight> source,
		Func<TRight, TNewRight> selector)
	{
		return source.SelectRight(selector);
	}

	/// <summary>
	/// Converts the <paramref name="source"/> into a new form if it is in the right state.
	/// </summary>
	/// <typeparam name="TLeft">The error type.</typeparam>
	/// <typeparam name="TRight">The original value type.</typeparam>
	/// <typeparam name="TNewRight">The new value type.</typeparam>
	/// <param name="source">The either to be transformed.</param>
	/// <param name="selector">
	/// Mapping function to determine the new value state, if the <paramref name="source"/> is right.
	/// </param>
	/// <returns></returns>
	/// <exception cref="InvalidOperationException"></exception>
	public static IEither<TLeft, TNewRight> SelectRight<TLeft, TRight, TNewRight>(
		this IEither<TLeft, TRight> source,
		Func<TRight, TNewRight> selector)
	{
		return source.SelectBoth(x => x, selector);
	}

	/// <summary>
	/// Converts the <paramref name="source"/> into a new form if it is in the left state.
	/// </summary>
	/// <typeparam name="TLeft">The original error type.</typeparam>
	/// <typeparam name="TNewLeft">The new error type.</typeparam>
	/// <typeparam name="TRight">The value type.</typeparam>
	/// <param name="source">The either to be transformed.</param>
	/// <param name="selector">
	/// Mapping function to determine the new error state, if the <paramref name="source"/> is left.
	/// </param>
	/// <returns></returns>
	/// <exception cref="InvalidOperationException"></exception>
	public static IEither<TNewLeft, TRight> SelectLeft<TLeft, TRight, TNewLeft>(
		this IEither<TLeft, TRight> source,
		Func<TLeft, TNewLeft> selector)
	{
		return source.SelectBoth(selector, x => x);
	}

	/// <summary>
	/// Attempts to access the value of the <paramref name="source"/>. This assumes that the
	/// <paramref name="source"/> is in the right state, otherwise an exception is thrown. 
	/// </summary>
	/// <typeparam name="TLeft">The error type.</typeparam>
	/// <typeparam name="TRight">The value type.</typeparam>
	/// <param name="source">The either to access the value of.</param>
	/// <returns></returns>
	/// <exception cref="InvalidOperationException"></exception>
	/// <remarks>
	/// This method is not safe to access without proper guard checks to verify that the
	/// <paramref name="source"/> is in the right state first.
	/// </remarks>
	public static TRight GetValueOrFail<TLeft, TRight>(
		this IEither<TLeft, TRight> source)
	{
		if (source is R<TLeft, TRight> r)
		{
			return r.Value;
		}

		throw new InvalidOperationException("Either is left.");
	}

	/// <summary>
	/// Attempts to access the error of the <paramref name="source"/>. This assumes that the
	/// <paramref name="source"/> is in the left state, otherwise an exception is thrown. 
	/// </summary>
	/// <typeparam name="TLeft">The error type.</typeparam>
	/// <typeparam name="TRight">The value type.</typeparam>
	/// <param name="source">The either to access the error of.</param>
	/// <returns></returns>
	/// <exception cref="InvalidOperationException"></exception>
	/// <remarks>
	/// This method is not safe to access without proper guard checks to verify that the
	/// <paramref name="source"/> is in the left state first.
	/// </remarks>
	public static TLeft GetErrorOrFail<TLeft, TRight>(
		this IEither<TLeft, TRight> source)
	{
		if (source is L<TLeft, TRight> l)
		{
			return l.Error;
		}

		throw new InvalidOperationException("Either is right.");
	}

	/// <summary>
	/// Attempts to access the value of the <paramref name="source"/>. If the 
	/// <paramref name="source"/> is in the left state, then the
	/// <paramref name="fallback"/> value is returned.
	/// </summary>
	/// <typeparam name="TLeft">The error type.</typeparam>
	/// <typeparam name="TRight">The value type.</typeparam>
	/// <param name="source">The either to access the value of.</param>
	/// <param name="fallback">The value to return if the <paramref name="source"/> is in the left state.</param>
	/// <returns></returns>
	public static TRight GetValueOrDefault<TLeft, TRight>(
		this IEither<TLeft, TRight> source, TRight fallback)
	{
		if (source is R<TLeft, TRight> r)
		{
			return r.Value;
		}

		return fallback;
	}

	/// <summary>
	/// Attempts to access the error of the <paramref name="source"/>. If the 
	/// <paramref name="source"/> is in the right state, then the
	/// <paramref name="fallback"/> value is returned.
	/// </summary>
	/// <typeparam name="TLeft">The error type.</typeparam>
	/// <typeparam name="TRight">The value type.</typeparam>
	/// <param name="source">The either to access the error of.</param>
	/// <param name="fallback">The value to return if the <paramref name="source"/> is in the right state.</param>
	/// <returns></returns>
	public static TLeft GetErrorOrDefault<TLeft, TRight>(
		this IEither<TLeft, TRight> source, TLeft fallback)
	{
		if (source is L<TLeft, TRight> l)
		{
			return l.Error;
		}

		return fallback;
	}

	/// <summary>
	/// Unwraps a nested <see cref="IEither{TLeft,TRight}"/>.
	/// </summary>
	/// <typeparam name="L1">The error type.</typeparam>
	/// <typeparam name="R1">The value type.</typeparam>
	/// <param name="source">The either to be unwrapped.</param>
	/// <returns></returns>
	public static IEither<L1, R1> SelectMany<L1, R1>(
		this IEither<L1, IEither<L1, R1>> source)
	{
		return source switch
		{
			L<L1, IEither<L1, R1>> l => new L<L1, R1>(l.Error),
			R<L1, IEither<L1, R1>> r => r.Value,

			_ => throw new InvalidOperationException("Either type was not understood."),
		};
	}

	/// <summary>
	/// Unwraps a nested <see cref="IEither{TLeft,TRight}"/>.
	/// </summary>
	/// <typeparam name="L1">The error type.</typeparam>
	/// <typeparam name="R1">The original value type.</typeparam>
	/// <typeparam name="R2">The new value type.</typeparam>
	/// <param name="source">The either to be unwrapped.</param>
	/// <param name="selector">Mapping function to create the new error value.</param>
	/// <returns></returns>
	public static IEither<L1, R2> SelectMany<L1, R1, R2>(
		this IEither<L1, IEither<L1, R1>> source,
		Func<R1, IEither<L1, R2>> selector)
	{
		return source switch
		{
			L<L1, IEither<L1, R1>> l => new L<L1, R2>(l.Error),
			R<L1, IEither<L1, R1>> r => r.SelectMany().Select(selector).SelectMany(),

			_ => throw new InvalidOperationException("Either type was not understood."),
		};
	}

	/// <summary>
	/// Unwraps a nested <see cref="IEither{TLeft,TRight}"/>.
	/// </summary>
	/// <typeparam name="L1">The error type.</typeparam>
	/// <typeparam name="R1">The value type.</typeparam>
	/// <typeparam name="U">The intermediate value type</typeparam>
	/// <typeparam name="TResult">The ending value type.</typeparam>
	/// <param name="source">The either to be unwrapped.</param>
	/// <param name="keySelector">
	/// Mapping function to create the intermediate error value.
	/// </param>
	/// <param name="resultSelector">
	/// Mapping function to create final error value from the intermediate error value.
	/// </param>
	/// <returns></returns>
	public static IEither<L1, TResult> SelectMany<L1, R1, U, TResult>(
		this IEither<L1, IEither<L1, R1>> source,
		Func<R1, IEither<L1, U>> keySelector,
		Func<R1, U, TResult> resultSelector)
	{
		return source.SelectMany(x => keySelector(x).Select(y => resultSelector(x, y)));
	}

	/// <summary>
	/// Unwraps a nested <see cref="IEither{TLeft,TRight}"/>.
	/// </summary>
	/// <typeparam name="L1">The error type.</typeparam>
	/// <typeparam name="R1">The value type.</typeparam>
	/// <param name="source">The either to be unwrapped.</param>
	/// <returns></returns>
	public static IEither<L1, R1> SelectMany<L1, R1>(
		this IEither<IEither<L1, R1>, R1> source)
	{
		return source switch
		{
			R<IEither<L1, R1>, R1> r => new R<L1, R1>(r.Value),
			L<IEither<L1, R1>, R1> l2 => l2.Error,

			_ => throw new InvalidOperationException("Either type was not understood."),
		};
	}

	/// <summary>
	/// Unwraps a nested <see cref="IEither{TLeft,TRight}"/>.
	/// </summary>
	/// <typeparam name="L1">The original error type.</typeparam>
	/// <typeparam name="R1">The value type.</typeparam>
	/// <typeparam name="L2">The new error value type.</typeparam>
	/// <param name="source">The either to be unwrapped.</param>
	/// <param name="selector">Mapping function to create the new error value.</param>
	/// <returns></returns>
	public static IEither<L2, R1> SelectMany<L1, R1, L2>(
		this IEither<IEither<L1, R1>, R1> source,
		Func<L1, IEither<L2, R1>> selector)
	{
		return source switch
		{
			R<IEither<L1, R1>, R1> r => new R<L2, R1>(r.Value),
			L<IEither<L1, R1>, R1> l => l.SelectMany().SelectLeft(selector).SelectMany(),

			_ => throw new InvalidOperationException("Either type was not understood."),
		};
	}

	/// <summary>
	/// Unwraps a nested <see cref="IEither{TLeft,TRight}"/>.
	/// </summary>
	/// <typeparam name="L1">The error type.</typeparam>
	/// <typeparam name="R1">The value type.</typeparam>
	/// <typeparam name="U">The intermediate value type</typeparam>
	/// <typeparam name="TResult">The ending value type.</typeparam>
	/// <param name="source">The either to be unwrapped.</param>
	/// <param name="keySelector">
	/// Mapping function to create the intermediate error value.
	/// </param>
	/// <param name="resultSelector">
	/// Mapping function to create final error value from the intermediate error value.
	/// </param>
	/// <returns></returns>
	public static IEither<TResult, R1> SelectMany<L1, R1, U, TResult>(
		this IEither<IEither<L1, R1>, R1> source,
		Func<L1, IEither<U, R1>> keySelector,
		Func<L1, U, TResult> resultSelector)
	{
		return source.SelectMany(x => keySelector(x).SelectLeft(y => resultSelector(x, y)));
	}

	/// <summary>
	/// Generates a single error string from a collection of errors
	/// in the <paramref name="either"/>.
	/// </summary>
	/// <typeparam name="L1"></typeparam>
	/// <typeparam name="R1"></typeparam>
	/// <param name="either">The left <see cref="IEither{TLeft,TRight}"/> to retrieve the errors from.</param>
	/// <param name="map">Function to apply to each error item.</param>
	/// <param name="separator">An optional separator to use between each joined string.</param>
	/// <returns></returns>
	public static string JoinErrors<L1, R1>(
		this L<IEnumerable<L1>, R1> either,
		Func<L1, string> map,
		string separator = "")
	{
		return string.Join(separator, either.Error.Select(map));
	}
}
