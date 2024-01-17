using Toyer.Logic.Exceptions.FailResponses.Abstract;

namespace Toyer.Logic.Exceptions.FailResponses.Derived.Device;

public sealed class DeviceIsAssignedException(string deviceId) 
    : BadRequestException($"The device with the identifier {deviceId} is in assigned state. To change user, first unassign device from its current account.");
