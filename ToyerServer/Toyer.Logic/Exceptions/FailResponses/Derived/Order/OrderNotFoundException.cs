using Microsoft.AspNetCore.Http;
using Toyer.Logic.Exceptions.FailResponses.Abstract;

namespace Toyer.Logic.Exceptions.FailResponses.Derived.Order;

public sealed class OrderNotFoundException(int orderId)
        : NotFoundException($"The order with the identifier {orderId} was not found.");
