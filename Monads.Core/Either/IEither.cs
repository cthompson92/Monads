using Monads.Core.Results;

namespace Monads.Core.Either;

/// <summary>
/// <para>
/// An either will have a <typeparamref name="TLeft"/> left (error) value,
/// or a <typeparamref name="TRight"/> right (success/correct) value.
/// </para>
///
/// <para>
/// Conventionally, the right side of the Either is the success value, because
/// right = correct.
/// </para>
/// </summary>
/// <typeparam name="TLeft">The type of value when the operation failed.</typeparam>
/// <typeparam name="TRight">The type of value when the operation succeeded.</typeparam>
/// <remarks>
/// <para>
/// For operations which do not have a value to return, but which may fail, use <see cref="IResult"/> or
/// <see cref="IResult{TError}"/>.
/// </para>
///
/// <para>
/// For operations which may or may not return a value, but will not fail, use a nullable return type.
/// There is no Maybe type defined.
/// </para>
/// </remarks>
public interface IEither<out TLeft, out TRight> : IResult
{
	bool IsRight { get; }
}

/// <summary>
/// A common form of <see cref="IEither{TLeft,TRight}"/> which uses
/// a collection of key-value pairs as the left (error) type.
/// </summary>
/// <typeparam name="TRight">The type of value when the operation succeeded.</typeparam>
public interface IEither<out TRight> : IEither<IEnumerable<KeyValuePair<string, string>>, TRight>
{
}
