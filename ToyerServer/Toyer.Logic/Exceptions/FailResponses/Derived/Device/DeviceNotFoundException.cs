using Toyer.Logic.Exceptions.FailResponses.Abstract;

namespace Toyer.Logic.Exceptions.FailResponses.Derived.Device;

public sealed class DeviceNotFoundException(string deviceId)
        : NotFoundException($"The device with the identifier {deviceId} was not found.");
