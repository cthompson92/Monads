using Monads.Core.Either;
using Monads.Core.Either.Left;

namespace Monads.Core.Results;

/// <summary>
/// A common form of <see cref="Result{TError}"/> which records errors using a
/// collection of key-value pairs.
/// </summary>
public record ServiceResult : Result<IEnumerable<KeyValuePair<string, string>>>
{
	protected ServiceResult(bool isSuccess, IEnumerable<KeyValuePair<string, string>> error)
		: base(isSuccess, error)
	{
	}

	public static ServiceResult Success()
	{
		return new ServiceResult(true, Array.Empty<KeyValuePair<string, string>>());
	}

	public static ServiceResult Failure(params KeyValuePair<string, string>[] errors)
	{
		return new ServiceResult(false, errors);
	}

	public static ServiceResult Failure(IEnumerable<KeyValuePair<string, string>> errors)
	{
		return new ServiceResult(false, errors);
	}

	public static ServiceResult Failure(string errorMessage)
	{
		return Failure(new KeyValuePair<string, string>("", errorMessage));
	}

	public static ServiceResult From<TRight>(
		IEither<IEnumerable<KeyValuePair<string, string>>, TRight> either)
	{
		if (either is L<IEnumerable<KeyValuePair<string, string>>, TRight> l)
		{
			return Failure(l.Error);
		}

		return Success();
	}

	public static implicit operator ServiceResult(KeyValuePair<string, string> errors)
		=> Failure(errors);

	public static implicit operator ServiceResult(string error)
		=> Failure(error);
}
