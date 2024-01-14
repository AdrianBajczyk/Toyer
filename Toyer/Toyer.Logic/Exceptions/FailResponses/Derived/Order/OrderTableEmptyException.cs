using Toyer.Logic.Exceptions.FailResponses.Abstract;

namespace Toyer.Logic.Exceptions.FailResponses.Derived.Order;

public sealed class OrderTableEmptyException()
        : NotFoundException($"There are no orders yet.");