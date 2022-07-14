namespace Monads.Core.Results;

public static class ResultExtensions
{
	/// <summary>
	/// Converts the <paramref name="source"/> into a new form.
	/// </summary>
	/// <typeparam name="T1">The original error type.</typeparam>
	/// <typeparam name="T2">The new error type.</typeparam>
	/// <param name="source">The result to be transformed.</param>
	/// <param name="selector">Mapping function to create the new error value.</param>
	/// <returns></returns>
	public static IResult<T2> Select<T1, T2>(
		this IResult<T1> source, Func<T1, T2> selector)
	{
		return source.IsSuccess
			? new Result<T2>()
			: new Result<T2>(selector(source.Error));
	}

	/// <summary>
	/// Unwraps a nested <see cref="IResult{TLeft}"/>.
	/// </summary>
	/// <typeparam name="T">The error type.</typeparam>
	/// <returns></returns>
	public static IResult<T> SelectMany<T>(
		this IResult<IResult<T>> source)
	{
		return source.Error;
	}

	/// <summary>
	/// Unwraps a nested <see cref="IResult{TLeft}"/>.
	/// </summary>
	/// <typeparam name="T">The original error type.</typeparam>
	/// <typeparam name="TResult">The new error type.</typeparam>
	/// <param name="source">The result to be unwrapped.</param>
	/// <param name="selector">Mapping function to create the new error value.</param>
	/// <returns></returns>
	public static IResult<TResult> SelectMany<T, TResult>(
		this IResult<T> source,
		Func<T, IResult<TResult>> selector)
	{
		return source.Select(selector).SelectMany();
	}

	/// <summary>
	/// Unwraps a nested <see cref="IResult{TLeft}"/>.
	/// </summary>
	/// <typeparam name="T">The error type.</typeparam>
	/// <typeparam name="U">The intermediate error type.</typeparam>
	/// <typeparam name="TResult">The ending error type.</typeparam>
	/// <param name="source">The result to be unwrapped.</param>
	/// <param name="keySelector">
	/// Mapping function to create the intermediate error value.
	/// </param>
	/// <param name="resultSelector">
	/// Mapping function to create final error value from the intermediate error value.
	/// </param>
	/// <returns></returns>
	public static IResult<TResult> SelectMany<T, U, TResult>(
		this IResult<T> source,
		Func<T, IResult<U>> keySelector,
		Func<T, U, TResult> resultSelector)
	{
		return source.SelectMany(x => keySelector(x).Select(y => resultSelector(x, y)));
	}
}
