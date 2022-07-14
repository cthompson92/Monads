namespace Monads.Core.Results;

/// <summary>
/// A semantic form of <see cref="ServiceResult"/> which indicates
/// a failure because a required or requested resource was not found.
/// </summary>
public record NotFound : ServiceResult
{
	public NotFound(IEnumerable<KeyValuePair<string, string>> error)
		: base(false, error)
	{
	}

	public NotFound(params KeyValuePair<string, string>[] errors)
		: base(false, errors)
	{
	}

	public NotFound(string errorMessage)
		: this(new KeyValuePair<string, string>("", errorMessage))
	{
	}
}
