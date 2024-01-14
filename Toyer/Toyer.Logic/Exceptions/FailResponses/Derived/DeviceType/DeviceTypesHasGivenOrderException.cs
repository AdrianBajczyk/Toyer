using Toyer.Logic.Exceptions.FailResponses.Abstract;

namespace Toyer.Logic.Exceptions.FailResponses.Derived.DeviceType;

public sealed class DeviceTypesHasGivenOrderException(IEnumerable<int> deviceTypesIds)
    : BadRequestException($"Device types: [{string.Join(", ", deviceTypesIds)}] has/have a member of given order");