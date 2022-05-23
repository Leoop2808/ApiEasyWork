
using ApiEasyWork.Providers;
using EasyWorkDataAccess.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace ApiEasyWork.App_Start
{
    public class ApplicationUserManager : UserManager<trs_usuario>
    {
        public ApplicationUserManager(IUserStore<trs_usuario> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var manager = new ApplicationUserManager(new UserStoreApiEasyWork());
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<trs_usuario>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                //RequireUniqueEmail = true
            };
            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 8,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = new DataProtectorTokenProvider<trs_usuario>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }
}