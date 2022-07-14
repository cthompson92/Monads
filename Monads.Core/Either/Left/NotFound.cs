namespace Monads.Core.Either.Left;

/// <summary>
/// A special type of <see cref="L{TLeft,TRight}"/> which indicates
/// that a requested resource could not be found or does not exist.
/// </summary>
/// <typeparam name="TLeft">The type of error.</typeparam>
/// <typeparam name="TRight"></typeparam>
public class NotFound <TLeft, TRight>
	: L<TLeft, TRight>
{
	public NotFound(TLeft error) : base(error) { }
}

/// <summary>
/// A common form of <see cref="NotFound{TLeft,TRight}"/> which uses
/// a collection of key-value pairs as the left (error) type.
/// </summary>
/// <typeparam name="TRight"></typeparam>
public class NotFound <TRight>
	: NotFound<IEnumerable<KeyValuePair<string, string>>, TRight>, IEither<TRight>
{
	public NotFound(IEnumerable<KeyValuePair<string, string>> error) : base(error) { }
}

public static class NotFound
{
	public static NotFound<TRight> Create<TRight>(
		params KeyValuePair<string, string>[] errors)
	{
		return new NotFound<TRight>(errors);
	}

	public static NotFound<TRight> Create<TRight>(
		IEnumerable<KeyValuePair<string, string>> errors)
	{
		return new NotFound<TRight>(errors);
	}

	public static NotFound<TRight> Create<TRight>(
		string errorMessage)
	{
		return Create<TRight>(new KeyValuePair<string, string>("", errorMessage));
	}
}
