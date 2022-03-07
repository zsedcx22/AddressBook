using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Threading.BackgroundWorkers;
using Abp.Threading.Timers;
using Abp.Timing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesterProject.ContactManagement.Entities;

namespace TesterProject.ContactManagement
{
    public class DeleteOldDeletedContactPassiveWorker : PeriodicBackgroundWorkerBase, ISingletonDependency
    {
        private readonly IRepository<Contact> _contactRepository;

        public DeleteOldDeletedContactPassiveWorker(AbpTimer timer, IRepository<Contact> contactRepository) : base(timer)
        {
            _contactRepository = contactRepository;
            Timer.Period = 5000;
        }

        [UnitOfWork]
        protected override void DoWork()
        {
            using (CurrentUnitOfWork.DisableFilter(AbpDataFilters.SoftDelete))
            {
                var oneMonthAgo = Clock.Now.Subtract(TimeSpan.FromDays(30));
                var deletedContacts = _contactRepository.GetAll().Where(c => c.IsDeleted == true);
                foreach (Contact del in deletedContacts)
                {
                    _contactRepository.Delete(del);
                }
                //--hard delete these contacts--
            }
        }

    }
}
