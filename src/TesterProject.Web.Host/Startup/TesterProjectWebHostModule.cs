using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using TesterProject.Configuration;

namespace TesterProject.Web.Host.Startup
{
    [DependsOn(
       typeof(TesterProjectWebCoreModule))]
    public class TesterProjectWebHostModule: AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public TesterProjectWebHostModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(TesterProjectWebHostModule).GetAssembly());
        }
    }
}
