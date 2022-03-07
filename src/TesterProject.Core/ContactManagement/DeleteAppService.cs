using Abp.BackgroundJobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesterProject.ContactManagement
{
    public class DeleteAppService : IDeleteAppService
    {
        private readonly IBackgroundJobManager _backgroundJobManager;

        public DeleteAppService(IBackgroundJobManager backgroundJobManager)
        {
            _backgroundJobManager = backgroundJobManager;
        }

        public async Task DeleteOld()//Hard deletes deleted contacts in the database
        {
            await _backgroundJobManager.EnqueueAsync<DeleteOldJob,int>(30);
        }
    }
}
