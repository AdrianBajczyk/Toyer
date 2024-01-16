using Toyer.Logic.Exceptions.FailResponses.Abstract;

namespace Toyer.Logic.Exceptions.FailResponses.Derived.DeviceType;

public sealed class DeviceTypesTableEmptyException()
    : NotFoundException("There are no device types yet.");
