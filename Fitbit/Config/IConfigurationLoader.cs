using Fitbit.Api.Config;

namespace Fitbit.Config
{
    public interface IConfigurationLoader
    {
        FitBitConfiguration GetConfiguration();
    }
}