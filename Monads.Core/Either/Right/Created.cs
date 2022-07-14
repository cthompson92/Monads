namespace Monads.Core.Either.Right;

/// <summary>
/// A special type of <see cref="R{TLeft,TRight}"/> which indicates
/// that a new resource was created.
/// </summary>
/// <typeparam name="TLeft"></typeparam>
/// <typeparam name="TRight">The type of value.</typeparam>
public class Created <TLeft, TRight> : R<TLeft, TRight>
{
	public Created(TRight value) : base(value) { }
}

/// <summary>
/// A common form of <see cref="Created{TLeft,TRight}"/> which uses
/// a collection of key-value pairs as the left (error) type.
/// </summary>
/// <typeparam name="TRight">The type of value.</typeparam>
public class Created <TRight>
	: Created<IEnumerable<KeyValuePair<string, string>>, TRight>, IEither<TRight>
{
	public Created(TRight value) : base(value) { }
}

public static class Created
{
	public static Created<TRight> Create<TRight>(
		TRight value)
	{
		return new Created<TRight>(value);
	}
}
