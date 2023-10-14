using Microsoft.AspNetCore.Mvc;
using ServerArchitecture.Services;
using Server.Configurations;
using ServerArchitecture.Context;
using System;
using Newtonsoft.Json.Linq;

namespace ServerArchitecture.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShopController : Controller
    {
        private readonly ServerContext _context;
        private readonly ShopService _shopService;
        private readonly ICollection<Item> _configItems; 

        public ShopController(ServerContext context)
        {
            _context = context;
            _shopService = new ShopService(context);

            string path = "./Sheet/Items.txt";
            var lines = System.IO.File.ReadLines(path);
            
            string[] splitRow;
            ICollection<Item>? items = null;
            foreach (var line in lines)
            {
                splitRow = line.Split(",");
                Item item = new Item
                {
                    Id = int.Parse(splitRow[0]),
                    Name = splitRow[1],
                    ItemType = splitRow[2],
                    Cost = int.Parse(splitRow[3])
                };
                items.Append(item);
            }

            _configItems = items;
        }

        [HttpGet(Name = "GetAllItems")]
        public ICollection<Item> GetAvailableItems() {
            return _configItems;
        }

        [HttpGet]
        [Route("PurchaseItem/{userId}/{itemId}/{itemAmount}")]
        public async Task<bool> PurchaseItemAsync(int userId, int itemId, int itemAmount) {
            return await _shopService.PurchaseAsync(userId, itemId, itemAmount, _configItems);
        }
    }
}
