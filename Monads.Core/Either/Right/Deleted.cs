namespace Monads.Core.Either.Right;

/// <summary>
/// A special type of <see cref="R{TLeft,TRight}"/> which indicates
/// that a resource was deleted.
/// </summary>
/// <typeparam name="TLeft"></typeparam>
/// <typeparam name="TRight">The type of value.</typeparam>
public class Deleted <TLeft, TRight> : R<TLeft, TRight>
{
	public Deleted(TRight value) : base(value) { }
}

/// <summary>
/// A common form of <see cref="Deleted{TLeft,TRight}"/> which uses
/// a collection of key-value pairs as the left (error) type.
/// </summary>
/// <typeparam name="TRight">The type of value.</typeparam>
public class Deleted <TRight>
	: Deleted<IEnumerable<KeyValuePair<string, string>>, TRight>, IEither<TRight>
{
	public Deleted(TRight value) : base(value) { }
}

public static class Deleted
{
	public static Deleted<TRight> Create<TRight>(
		TRight value)
	{
		return new Deleted<TRight>(value);
	}
}
