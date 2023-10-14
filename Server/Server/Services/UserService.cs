using Microsoft.EntityFrameworkCore;
using ServerArchitecture.Context;
using ServerArchitecture.Entities.UserEntities;
using ServerArchitecture.Exceptions;

namespace ServerArchitecture.Services
{
    public class UserService
    {
        private readonly ServerContext _context;

        public UserService(ServerContext context)
        {
            _context = context;
        }

        #region Create Method
        public virtual async Task<User> CreateAsync(User user)
        {
            var serverContext = new ServerContext();
            user.Level = 1;

            serverContext.Users.Add(user);

            await serverContext.SaveChangesAsync();
            return user;
        }
        #endregion

        #region Get Method
        public virtual async Task<User> GetAsync(int id)
        {
           
            User user = await _context.Users.SingleOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                throw new EntityNotFoundException(typeof(User), id);
            }

            return user;
        }

        #endregion


    }
}
