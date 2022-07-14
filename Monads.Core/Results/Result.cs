namespace Monads.Core.Results;

/// <summary>
/// Default implementation of <see cref="IResult{TError}"/>.
/// </summary>
/// <typeparam name="T">The type of error this result may contain.</typeparam>
public record Result<T> : IResult<T>
{
	/// <inheritdoc />
	public bool IsSuccess { get; }

	/// <inheritdoc />
	public T Error { get; }

	public Result()
		: this(true, default!)
	{
	}

	public Result(T error)
		: this(false, error)
	{
	}

	protected Result(bool isSuccess, T error)
	{
		IsSuccess = isSuccess;
		Error = error;
	}
}

public static class Result
{
	public static Result<T> Return<T>(T error)
	{
		return new Result<T>(error);
	}

	public static Result<T> Return<T>()
	{
		return new Result<T>();
	}

	public static Result<IEnumerable<KeyValuePair<string, string>>> Common()
	{
		return new Result<IEnumerable<KeyValuePair<string, string>>>();
	}

	public static Result<IEnumerable<KeyValuePair<string, string>>> Return(
		params KeyValuePair<string, string>[] errors)
		=> Return<IEnumerable<KeyValuePair<string, string>>>(errors);

	public static Result<IEnumerable<KeyValuePair<string, string>>> Return(
		params string[] errors)
		=> Return<IEnumerable<KeyValuePair<string, string>>>(
			errors
				.Select(x => new KeyValuePair<string, string>(string.Empty, x))
				.ToArray());
}
