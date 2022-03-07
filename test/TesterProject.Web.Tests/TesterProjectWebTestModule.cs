using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using TesterProject.EntityFrameworkCore;
using TesterProject.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace TesterProject.Web.Tests
{
    [DependsOn(
        typeof(TesterProjectWebMvcModule),
        typeof(AbpAspNetCoreTestBaseModule)
    )]
    public class TesterProjectWebTestModule : AbpModule
    {
        public TesterProjectWebTestModule(TesterProjectEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
        } 
        
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(TesterProjectWebTestModule).GetAssembly());
        }
        
        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(TesterProjectWebMvcModule).Assembly);
        }
    }
}