namespace Monads.Core.Results;

/// <summary>
/// A semantic form of <see cref="ServiceResult"/> which indicates
/// a failure because of an authorization failure.
/// </summary>
public record Unauthorized : ServiceResult
{
	public Unauthorized(IEnumerable<KeyValuePair<string, string>> error)
		: base(false, error)
	{
	}

	public Unauthorized(params KeyValuePair<string, string>[] errors)
		: base(false, errors)
	{
	}

	public Unauthorized(string errorMessage)
		: this(new KeyValuePair<string, string>("", errorMessage))
	{
	}
}
