namespace Monads.Core.Maybe;

public record Just<T> : IMaybe<T>
	where T : notnull
{
	private readonly T _value;

	public Just(T value)
	{
		_value = value;
	}

	public TResult Match<TResult>(TResult nothing, Func<T, TResult> just)
		where TResult : notnull
	{
		return just(_value);
	}
}

public static class Just
{
	public static Just<T> Return <T>(T value)
		where T: notnull
	{
		return new Just<T>(value);
	}
}
