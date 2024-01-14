using Toyer.Logic.Exceptions.FailResponses.Abstract;

namespace Toyer.Logic.Exceptions.FailResponses.Derived.DeviceType;

public sealed class DeviceTypeNotFoundException(int deviceTypeIds)
    : NotFoundException($"Device type with identifier {deviceTypeIds} was not found.");
