using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using TesterProject.Authorization;

namespace TesterProject
{
    [DependsOn(
        typeof(TesterProjectCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class TesterProjectApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<TesterProjectAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(TesterProjectApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
