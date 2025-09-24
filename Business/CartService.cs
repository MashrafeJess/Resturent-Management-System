using Database;
using static Database.Context.Context;
namespace Business
{
    public class CartService
    {
        ResturantContext context = new ResturantContext();
        public Result Add(Cart cart)
        {
            var x = context.Food.Where(x => x.FoodId == cart.FoodId).FirstOrDefault();
            if (cart.Qty > x.Qty)
            {
                return new Result(false, "Product QUantity Exceed");
            }
            context.Cart.Add(cart);
            return new Result().DBcommit(context, "Added to cart Successfully!", null, cart);
        }
        public Result Delete(int Id)
        {
            var existingCart = context.Cart.Where(x=>x.CartId==Id).FirstOrDefault();
            if (existingCart == null)
                return new Result(false, "Cart item not found!");
            context.Cart.Remove(existingCart);
            return new Result().DBcommit(context, "Removed from cart Successfully!", null, existingCart);
        }
        public Result Update(Cart cart)
        {
            if (!context.Cart.Any(x => x.CartId == cart.CartId))
                return new Result(false, "Cart item not exist!");
            context.Cart.Update(cart);
            return new Result().DBcommit(context, "Cart updated Successfully!", null, cart);
        }
        public Result List()
        {
            var carts = context.JoinCart.ToList();
            return new Result(true, "Success", carts);
        }
        public Result UpdateQty(int FoodId, int Qty)
        {
            var item = context.Cart.FirstOrDefault(x => x.FoodId == FoodId);
            var food = context.Food.FirstOrDefault(x => x.FoodId == FoodId);
            if (item != null && item.Qty > 0)
            {
                if(food.Qty <= item.Qty)
                {
                    context.Cart.Update(item);
                    return new Result().DBcommit(context, "Product Quantity Updated", null, item);
                } 
            }
            return new Result(false, "Quantity Exceed");
        }
    }
}
