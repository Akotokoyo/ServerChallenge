using Microsoft.EntityFrameworkCore;
using Server.Configurations;
using ServerArchitecture.Context;
using ServerArchitecture.Entities.UserEntities;
using ServerArchitecture.Exceptions;

namespace ServerArchitecture.Services
{
    public class ShopService
    {
        public const int CASH_CURRENCY_ID = 1000;
        public const int ACF_CURRENCY_ID = 1001;
        private readonly ServerContext _context;

        public ShopService(ServerContext context)
        {
            _context = context;
        }

        #region Shop Method
        public virtual async Task<bool> PurchaseAsync(int userId, int itemId, int itemAmount, ICollection<Item> _configItems)
        {
            Item item = _configItems.FirstOrDefault(i => i.Id == itemId);
            UserItems userItemCash = await _context.UserItems.FirstAsync(ui => ui.UserId == userId && ui.ItemId == CASH_CURRENCY_ID);
            
            if(userItemCash == null) { throw new NotEnoughCurrencyException(); }
            if(userItemCash.ItemAmount < item.Cost) { throw new NotEnoughCurrencyException(); }

            UserItems newUserItem = new UserItems { UserId = userId, ItemId = itemId, ItemAmount = itemAmount };
            await _context.SaveChangesAsync();
            
            return true;
        }
        #endregion
    }
}
