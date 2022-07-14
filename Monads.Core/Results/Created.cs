namespace Monads.Core.Results;

/// <summary>
/// A semantic form of <see cref="ServiceResult"/> which indicates that
/// a new resource was created.
/// </summary>
public record Created : ServiceResult
{
	protected Created()
		: base(true, Array.Empty<KeyValuePair<string, string>>())
	{
	}
}
