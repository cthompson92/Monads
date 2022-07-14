namespace Monads.Core.Either.Left;

/// <summary>
/// Default implementation of the left side of an <see cref="IEither{TLeft,TRight}"/>,
/// with no special or semantic meaning.
/// </summary>
/// <typeparam name="TLeft">The type of error.</typeparam>
/// <typeparam name="TRight"></typeparam>
public class L<TLeft, TRight>
	: IEither<TLeft, TRight>, IEquatable<L<TLeft, TRight>>
{
	/// <summary>
	/// The error value.
	/// </summary>
	public TLeft Error { get; }

	public L(TLeft error)
	{
		Error = error;
	}

	/// <inheritdoc />
	public bool IsRight => false;

	/// <inheritdoc />
	public bool IsSuccess => IsRight;

	/// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
	/// <param name="other">An object to compare with this object.</param>
	/// <returns>true if the current object is equal to the <paramref name="other">other</paramref> parameter; otherwise, false.</returns>
	public bool Equals(L<TLeft, TRight>? other)
	{
		if (ReferenceEquals(null, other))
		{
			return false;
		}

		if (ReferenceEquals(this, other))
		{
			return true;
		}

		return EqualityComparer<TLeft>.Default.Equals(Error, other.Error);
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

		return Equals((L<TLeft, TRight>)obj);
	}

	/// <summary>Serves as the default hash function.</summary>
	/// <returns>A hash code for the current object.</returns>
	public override int GetHashCode()
	{
		return EqualityComparer<TLeft>.Default.GetHashCode(Error);
	}

	/// <summary>Returns a value that indicates whether the values of two <see cref="T:Monads.Core.Either.Left.L`2" /> objects are equal.</summary>
	/// <param name="left">The first value to compare.</param>
	/// <param name="right">The second value to compare.</param>
	/// <returns>true if the <paramref name="left" /> and <paramref name="right" /> parameters have the same value; otherwise, false.</returns>
	public static bool operator ==(L<TLeft, TRight>? left, L<TLeft, TRight>? right)
	{
		return Equals(left, right);
	}

	/// <summary>Returns a value that indicates whether two <see cref="T:Monads.Core.Either.Left.L`2" /> objects have different values.</summary>
	/// <param name="left">The first value to compare.</param>
	/// <param name="right">The second value to compare.</param>
	/// <returns>true if <paramref name="left" /> and <paramref name="right" /> are not equal; otherwise, false.</returns>
	public static bool operator !=(L<TLeft, TRight>? left, L<TLeft, TRight>? right)
	{
		return !Equals(left, right);
	}
}

/// <summary>
/// A common form of <see cref="L{TLeft,TRight}"/> which uses
/// a collection of key-value pairs as the left (error) type.
/// </summary>
/// <typeparam name="TRight"></typeparam>
public class L <TRight>
	: L<IEnumerable<KeyValuePair<string, string>>, TRight>, IEither<TRight>
{
	public L(IEnumerable<KeyValuePair<string, string>> error) : base(error) { }
}

public static class L
{
	public static L<TRight> Create<TRight>(
		params KeyValuePair<string, string>[] errors)
	{
		return new L<TRight>(errors);
	}

	public static L<TRight> Create<TRight>(
		IEnumerable<KeyValuePair<string, string>> errors)
	{
		return new L<TRight>(errors);
	}

	public static L<TRight> Create<TRight>(
		string errorMessage)
	{
		return Create<TRight>(new KeyValuePair<string, string>("", errorMessage));
	}

	/// <summary>
	/// Generates a single error string from a collection of errors
	/// in the <paramref name="either"/>.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="either">The left <see cref="IEither{TLeft,TRight}"/> to retrieve the errors from.</param>
	/// <param name="separator">An optional separator to use between each joined string.</param>
	/// <returns></returns>
	/// <remarks>
	/// The error key-value pairs will be joined in the form
	/// <code>"Key: Value"</code> or <code>"Value"</code>
	/// when there is no useful key to display
	/// </remarks>
	public static string JoinErrors<T>(
		this L<IEnumerable<KeyValuePair<string, string>>, T> either,
		string separator = "")
	{
		return either
			.JoinErrors(
				x => string.IsNullOrWhiteSpace(x.Key)
					? x.Value
					: $"{x.Key}: {x.Value}",
				separator);
	}
}
