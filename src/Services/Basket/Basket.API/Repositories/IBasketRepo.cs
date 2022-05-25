using Basket.API.Entities;
using System.Threading.Tasks;

namespace Basket.API.Repositories
{
    public interface IBasketRepo
    {
        Task<ShoppingCart> GetBasket(string userName);
        Task<ShoppingCart> updatebasket(ShoppingCart basket);
        Task DeleteBasket(string userName);
    }
}
