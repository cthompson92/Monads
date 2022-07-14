namespace Monads.Core.Maybe;

public interface IMaybe<out T>
	where T: notnull
{
	TResult Match<TResult>(TResult nothing, Func<T, TResult> just)
			where TResult: notnull;
}
