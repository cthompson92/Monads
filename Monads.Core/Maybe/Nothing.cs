namespace Monads.Core.Maybe;

public record Nothing<T> : IMaybe<T>
	where T : notnull
{
	public TResult Match <TResult>(TResult nothing, Func<T, TResult> just)
		where TResult: notnull
	{
		return nothing;
	}
}
