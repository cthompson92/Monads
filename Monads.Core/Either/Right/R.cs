namespace Monads.Core.Either.Right;

/// <summary>
/// Default implementation of the right side of an <see cref="IEither{TLeft,TRight}"/>,
/// with no special or semantic meaning.
/// </summary>
/// <typeparam name="TLeft"></typeparam>
/// <typeparam name="TRight">The type of value contained.</typeparam>
public class R<TLeft, TRight>
	: IEither<TLeft, TRight>, IEquatable<R<TLeft, TRight>>
{
	/// <summary>
	/// The success value.
	/// </summary>
	public TRight Value { get; }

	public R(TRight value)
	{
		Value = value;
	}

	/// <inheritdoc />
	public bool IsRight => true;

	/// <inheritdoc />
	public bool IsSuccess => IsRight;

	/// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
	/// <param name="other">An object to compare with this object.</param>
	/// <returns>true if the current object is equal to the <paramref name="other">other</paramref> parameter; otherwise, false.</returns>
	public bool Equals(R<TLeft, TRight>? other)
	{
		if (ReferenceEquals(null, other))
		{
			return false;
		}

		if (ReferenceEquals(this, other))
		{
			return true;
		}

		return EqualityComparer<TRight>.Default.Equals(Value, other.Value);
	}

	/// <summary>Determines whether the specified object is equal to the current object.</summary>
	/// <param name="obj">The object to compare with the current object.</param>
	/// <returns>true if the specified object  is equal to the current object; otherwise, false.</returns>
	public override bool Equals(object? obj)
	{
		if (ReferenceEquals(null, obj))
		{
			return false;
		}

		if (ReferenceEquals(this, obj))
		{
			return true;
		}

		if (obj.GetType() != this.GetType())
		{
			return false;
		}

		return Equals((R<TLeft, TRight>)obj);
	}

	/// <summary>Serves as the default hash function.</summary>
	/// <returns>A hash code for the current object.</returns>
	public override int GetHashCode()
	{
		return EqualityComparer<TRight>.Default.GetHashCode(Value);
	}

	/// <summary>Returns a value that indicates whether the values of two <see cref="T:Monads.Core.Either.Right.R`2" /> objects are equal.</summary>
	/// <param name="left">The first value to compare.</param>
	/// <param name="right">The second value to compare.</param>
	/// <returns>true if the <paramref name="left" /> and <paramref name="right" /> parameters have the same value; otherwise, false.</returns>
	public static bool operator ==(R<TLeft, TRight>? left, R<TLeft, TRight>? right)
	{
		return Equals(left, right);
	}

	/// <summary>Returns a value that indicates whether two <see cref="T:Monads.Core.Either.Right.R`2" /> objects have different values.</summary>
	/// <param name="left">The first value to compare.</param>
	/// <param name="right">The second value to compare.</param>
	/// <returns>true if <paramref name="left" /> and <paramref name="right" /> are not equal; otherwise, false.</returns>
	public static bool operator !=(R<TLeft, TRight>? left, R<TLeft, TRight>? right)
	{
		return !Equals(left, right);
	}
}

/// <summary>
/// A common form of <see cref="R{TLeft,TRight}"/> which uses
/// a collection of key-value pairs as the left (error) type.
/// </summary>
/// <typeparam name="TRight">The type of value contained.</typeparam>
public class R<TRight>
	: R<IEnumerable<KeyValuePair<string, string>>, TRight>, IEither<TRight>
{
	/// <summary>
	/// A common form of <see cref="R{TLeft,TRight}"/> which uses
	/// a collection of key-value pairs as the left (error) type.
	/// </summary>
	/// <param name="value"></param>
	public R(TRight value) : base(value) { }
}

public static class R
{
	public static R<IEnumerable<KeyValuePair<string, string>>, TRight> Create<TRight>(
		TRight value)
	{
		return new R<IEnumerable<KeyValuePair<string, string>>, TRight>(value);
	}

	public static R<TRight> Common<TRight>(TRight value)
	{
		return new R<TRight>(value);
	}
}
