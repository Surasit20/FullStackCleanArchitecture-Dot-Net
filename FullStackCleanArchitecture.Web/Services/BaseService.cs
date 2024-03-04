using FullStackCleanArchitecture.Web.Configuration;
using Microsoft.Extensions.Options;

namespace FullStackCleanArchitecture.Web.Services
{

    public interface IBaseService
    {
        AppSetting GetAppSettings();
    }

    public class BaseService : IBaseService
    {
        protected readonly AppSetting _appSettings;


        public BaseService(IOptions<AppSetting> appSettings)
        {
            this._appSettings = appSettings.Value;
        }

        public AppSetting GetAppSettings()
        {
            return this._appSettings;
        }
    }
}
