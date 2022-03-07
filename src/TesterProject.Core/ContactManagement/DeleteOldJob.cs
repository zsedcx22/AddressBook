using Abp.BackgroundJobs;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using TesterProject.ContactManagement.Entities;

namespace TesterProject.ContactManagement
{
    public class DeleteOldJob : BackgroundJob<int>, ITransientDependency
    {
        private readonly IRepository<Contact> _contactRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        public DeleteOldJob(IRepository<Contact> contactRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            _contactRepository = contactRepository;
            _unitOfWorkManager = unitOfWorkManager;
        }
        public override void Execute(int args)
        {
            var oldList = _contactRepository.GetAll().Where(e => (DateTime.Now.Date - e.DeletionTime.Value.Date).Days == 30);
            using (_unitOfWorkManager.Current.DisableFilter(AbpDataFilters.SoftDelete))
            {
                foreach (Contact del in oldList)
                {
                    _contactRepository.Delete(del);
                }
            }
        }
    }
}