namespace Monads.Core.Maybe;

public static class MaybeExtensions
{
	public static Task<TResult> MatchAsync <T, TResult>(
		this IMaybe<T> m,
		TResult nothing,
		Func<T, Task<TResult>> just)
		where T: notnull
		where TResult : notnull
	{
		return m.Match(Task.FromResult(nothing), just);
	}

	public static bool IsNothing <T>(this IMaybe<T> m)
		where T : notnull
	{
		return m.Match(true, _ => false);
	}

	public static bool IsJust <T>(this IMaybe<T> m)
		where T : notnull
	{
		return m.Match(false, _ => true);
	}

	public static bool HasValue <T>(this IMaybe<T> m)
		where T : notnull
		=> m.IsJust();

	public static IMaybe<TResult> Select <T, TResult>(
		this IMaybe<T> source,
		Func<T, TResult> selector)
		where T : notnull
		where TResult : notnull
	{
		return source.Match<IMaybe<TResult>>(
			new Nothing<TResult>(),
			x => new Just<TResult>(selector(x)));
	}

	/// <summary>
	/// Unwraps a nested <see cref="IMaybe{TLeft}"/>.
	/// </summary>
	/// <typeparam name="T">The value type.</typeparam>
	/// <returns></returns>
	public static IMaybe<T> SelectMany <T>(
		this IMaybe<IMaybe<T>> source)
		where T : notnull
	{
		return source.Match(new Nothing<T>(), x => x);
	}

	/// <summary>
	/// Unwraps a nested <see cref="IMaybe{TLeft}"/>.
	/// </summary>
	/// <typeparam name="T">The original type.</typeparam>
	/// <typeparam name="TResult">The new type.</typeparam>
	/// <param name="source">The maybe to be unwrapped.</param>
	/// <param name="selector">Mapping function to create the new value.</param>
	/// <returns></returns>
	public static IMaybe<TResult> SelectMany<T, TResult>(
		this IMaybe<T> source,
		Func<T, IMaybe<TResult>> selector)
		where T : notnull
		where TResult : notnull
	{
		return source.Select(selector).SelectMany();
	}

	/// <summary>
	/// Unwraps a nested <see cref="IMaybe{TLeft}"/>.
	/// </summary>
	/// <typeparam name="T">The original type.</typeparam>
	/// <typeparam name="U">The intermediate type.</typeparam>
	/// <typeparam name="TResult">The ending type.</typeparam>
	/// <param name="source">The maybe to be unwrapped.</param>
	/// <param name="keySelector">
	/// Mapping function to create the intermediate value.
	/// </param>
	/// <param name="resultSelector">
	/// Mapping function to create final value from the intermediate value.
	/// </param>
	/// <returns></returns>
	public static IMaybe<TResult> SelectMany<T, U, TResult>(
		this IMaybe<T> source,
		Func<T, IMaybe<U>> keySelector,
		Func<T, U, TResult> resultSelector)
		where T : notnull
		where U : notnull
		where TResult : notnull
	{
		return source.SelectMany(x => keySelector(x).Select(y => resultSelector(x, y)));
	}
}
