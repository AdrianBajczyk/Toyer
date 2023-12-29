using Toyer.Logic.Services.DeviceProvisioningService;

namespace Toyer.API.Extensions.Factories;

public static class GenerateDpsForEnrollmentFactory
{
    
}

public class DpsForEnrollmentFactory
{
    private readonly Func<IDpsClient> _factory;

    public DpsForEnrollmentFactory(Func<IDpsClient> factory)
    {
        _factory = factory;
    }
}