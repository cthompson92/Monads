using Monads.Core.Either.Left;
using Monads.Core.Either.Right;

namespace Monads.Core.Either;

/// <summary>
/// Factory methods for creating <see cref="IEither{TLeft,TRight}"/> and
/// <see cref="IEither{TRight}"/> instances.
/// </summary>
public static class Either
{
	public static IEither<L1, R1> Return<L1, R1>(L1 error)
	{
		return new L<L1, R1>(error);
	}

	public static IEither<L1, R1> Return<L1, R1>(R1 value)
	{
		return new R<L1, R1>(value);
	}

	/// <summary>
	/// Creates a left <see cref="IEither{TRight}"/> with the provided
	/// <paramref name="errors"/>.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="errors"></param>
	/// <returns></returns>
	public static IEither<T> Return<T>(
		params KeyValuePair<string, string>[] errors)
	{
		return new L<T>(errors);
	}

	/// <summary>
	/// Creates a left <see cref="IEither{TRight}"/> using the provided
	/// <paramref name="errors"/>.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="errors"></param>
	/// <returns></returns>
	/// <remarks>Each error is given an empty string for the key.</remarks>
	public static IEither<T> Return<T>(
		params string[] errors)
		=> Return<T>(
			errors
				.Select(x => new KeyValuePair<string, string>(string.Empty, x))
				.ToArray());

	/// <summary>
	/// Creates a right <see cref="IEither{TRight}"/> using the provided
	/// <paramref name="value"/>
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="value"></param>
	/// <returns></returns>
	public static IEither<T> Return<T>(T value)
	{
		return new R<T>(value);
	}
}
