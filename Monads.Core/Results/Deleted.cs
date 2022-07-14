namespace Monads.Core.Results;

/// <summary>
/// A semantic form of <see cref="ServiceResult"/> which indicates that
/// a resource was deleted.
/// </summary>
public record Deleted : ServiceResult
{
	protected Deleted()
		: base(true, Array.Empty<KeyValuePair<string, string>>())
	{
	}
}
