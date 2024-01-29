using Toyer.Logic.Exceptions.FailResponses.Abstract;

namespace Toyer.Logic.Exceptions.FailResponses.Derived.DeviceType;

public sealed class DeviceTypesNotFoundException(IEnumerable<int> deviceTypesIds)
    : NotFoundException($"Device types: [{string.Join(", ", deviceTypesIds)}] not found.");
