using EasyWorkDataAccess.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Win32.SafeHandles;
using System;
using System.Data.Entity;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace ApiEasyWork.Providers
{
    public class UserStoreApiEasyWork :
    IUserStore<trs_usuario>, IUserPasswordStore<trs_usuario>,
    IUserSecurityStampStore<trs_usuario>, IUserEmailStore<trs_usuario>
    {
        readonly EasyWorkDBEntities context = new EasyWorkDBEntities();
        bool disposed = false;
        readonly SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);


        public Task CreateAsync(trs_usuario user)
        {
            user.fecha_registro = DateTime.Now;
            user.fecha_actualizacion = DateTime.Now;
            context.trs_usuario.Add(user);
            return context.SaveChangesAsync();
        }


        public Task DeleteAsync(trs_usuario user)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                handle.Dispose();
            }
            disposed = true;
        }
        public Task<trs_usuario> FindByIdAsync(string userId)
        {
            Task<trs_usuario> task =
            context.trs_usuario.Where(apu => apu.cod_usuario == userId).FirstOrDefaultAsync();

            return task;
        }

        public Task<trs_usuario> FindByNameAsync(string userName)
        {
            Task<trs_usuario> task =
            context.trs_usuario.Where(apu => apu.username == userName)
                           .FirstOrDefaultAsync();

            return task;
        }

        public Task<string> GetPasswordHashAsync(trs_usuario user)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            return Task.FromResult(user.password);
        }

        public Task<string> GetSecurityStampAsync(trs_usuario user)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            return Task.FromResult(user.username);
        }

        public Task<bool> HasPasswordAsync(trs_usuario user)
        {
            return Task.FromResult(user.password != null);
        }

        public Task SetPasswordHashAsync(trs_usuario user, string passwordHash)
        {
            user.password = passwordHash;
            return Task.FromResult(0);
        }

        public Task SetSecurityStampAsync(trs_usuario user, string stamp)
        {
            return Task.FromResult(0);
        }

        public Task UpdateAsync(trs_usuario user)
        {
            if (context.trs_usuario.Any(x => x.id_usuario == user.id_usuario))
            {
                var usuario = context.trs_usuario.FirstOrDefault(x => x.id_usuario == user.id_usuario);
                usuario.password = user.password;
                return context.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentNullException("user");
            }

        }

        public Task SetEmailAsync(trs_usuario user, string email)
        {
            return Task.FromResult(0);
        }

        public Task<string> GetEmailAsync(trs_usuario user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            return Task.FromResult("email@null.com");
        }

        public Task<bool> GetEmailConfirmedAsync(trs_usuario user)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            return Task.FromResult(true);
        }

        public Task SetEmailConfirmedAsync(trs_usuario user, bool confirmed)
        {
            return Task.FromResult(0);
        }

        public Task<trs_usuario> FindByEmailAsync(string email)
        {
            Task<trs_usuario> task = null;

            return task;
        }
    }
}