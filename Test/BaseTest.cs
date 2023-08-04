using DataAccess.EF.Concrete;
using DataAccess.EF.Contexts;

namespace Test
{
    public class BaseTest
    {
        protected readonly CampaignRepository _campaignRepository;
        protected readonly CartItemRepository _cartItemRepository;
        protected readonly CartRepository _cartRepository;
        protected readonly CategoryRepository _categoryRepository;
        protected readonly OrderItemRepository _orderItemRepository;
        protected readonly OrderRepository _orderRepository;
        protected readonly ProductRepository _productRepository;

        public BaseTest()
        {
            var dbContext = new ECommerceContext();
            _campaignRepository = new CampaignRepository(dbContext);
            _cartItemRepository = new CartItemRepository(dbContext);
            _cartRepository = new CartRepository(dbContext);
            _categoryRepository = new CategoryRepository(dbContext);
            _orderItemRepository = new OrderItemRepository(dbContext);
            _orderRepository = new OrderRepository(dbContext);
            _productRepository = new ProductRepository(dbContext);
        }

        protected string GetRandomString()
        {
            return Guid.NewGuid().ToString().ToUpper();
        }
    }
}
