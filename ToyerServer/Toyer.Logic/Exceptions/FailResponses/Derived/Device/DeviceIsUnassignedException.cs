using Toyer.Logic.Exceptions.FailResponses.Abstract;

namespace Toyer.Logic.Exceptions.FailResponses.Derived.Device;

public sealed class DeviceIsUnassignedException(string deviceId)
    : BadRequestException($"The device with the identifier {deviceId} is in unassigned state. To use device assign it first.");