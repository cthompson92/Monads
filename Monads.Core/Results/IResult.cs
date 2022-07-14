using Monads.Core.Either;

namespace Monads.Core.Results;

/// <summary>
/// A result indicates whether an operation succeeded
/// without a value, e.g. <c>void</c>, or failed.
/// </summary>
/// <remarks>
/// <para>For a result which can include detailed failure information, use <see cref="IResult{TError}"/>.</para>
/// 
/// <para>
/// For operations which can fail with error information, but also return a success value,
/// use <see cref="IEither{TLeft,TRight}"/>.
/// </para>
///
/// <para>
/// For operations which may or may not return a value, but will not fail, use a nullable return type.
/// There is no Maybe type defined.
/// </para>
/// </remarks>
public interface IResult
{
	bool IsSuccess { get; }
}

/// <summary>
/// An <see cref="IResult"/> which includes failure information captured as
/// a <typeparamref name="TError"/>.
/// </summary>
/// <typeparam name="TError">The type of failure information that is when the operation fails.</typeparam>
/// <remarks>
/// <para>
/// For operations which can fail with error information, but also return a success value,
/// use <see cref="IEither{TLeft,TRight}"/>.
/// </para>
///
/// <para>
/// For operations which may or may not return a value, but will not fail, use a nullable return type.
/// There is no Maybe type defined.
/// </para>
/// </remarks>
public interface IResult<out TError> : IResult
{
	TError Error { get; }
}
