using Abp.Authorization;
using TesterProject.Authorization.Roles;
using TesterProject.Authorization.Users;

namespace TesterProject.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
