namespace Monads.Core.Either.Left;

/// <summary>
/// A special type of <see cref="L{TLeft,TRight}"/> which indicates
/// that the requested operation could not be completed due to an
/// authorization failure.
/// </summary>
/// <typeparam name="TLeft">The type of error.</typeparam>
/// <typeparam name="TRight"></typeparam>
public class Unauthorized <TLeft, TRight>
	: L<TLeft, TRight>
{
	public Unauthorized(TLeft error) : base(error) { }
}

/// <summary>
/// A common form of <see cref="Unauthorized{TLeft,TRight}"/> which uses
/// a collection of key-value pairs as the left (error) type.
/// </summary>
/// <typeparam name="TRight"></typeparam>
public class Unauthorized <TRight>
	: Unauthorized<IEnumerable<KeyValuePair<string, string>>, TRight>, IEither<TRight>
{
	public Unauthorized(IEnumerable<KeyValuePair<string, string>> error) : base(error) { }
}

public static class Unauthorized
{
	public static Unauthorized<TRight> Create<TRight>(
		params KeyValuePair<string, string>[] errors)
	{
		return new Unauthorized<TRight>(errors);
	}

	public static Unauthorized<TRight> Create<TRight>(
		IEnumerable<KeyValuePair<string, string>> errors)
	{
		return new Unauthorized<TRight>(errors);
	}

	public static Unauthorized<TRight> Create<TRight>(
		string errorMessage)
	{
		return Create<TRight>(new KeyValuePair<string, string>("", errorMessage));
	}
}
