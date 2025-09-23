using Database;
using static Database.Context.Context;
namespace Business
{
    public class CartService
    {
        ResturantContext context = new ResturantContext();
        public Result Add(Cart cart)
        {
           
            context.Cart.Add(cart);
            return new Result().DBcommit(context, "Added to cart Successfully!", null, cart);
        }
        public Result Delete(Cart cart)
        {
            var existingCart = context.Cart.Find(cart.CartId);
            if (existingCart == null)
                return new Result(false, "Cart item not found!");
            context.Cart.Remove(existingCart);
            return new Result().DBcommit(context, "Removed from cart Successfully!", null, cart);
        }
        public Result Update(Cart cart)
        {
            if (!context.Cart.Any(x => x.CartId == cart.CartId))
                return new Result(false, "Cart item not exist!");
            context.Cart.Update(cart);
            return new Result().DBcommit(context, "Cart updated Successfully!", null, cart);
        }
        public Result List(int Id)
        {
            var carts = context.Cart.Where(c => c.CartId == Id).ToList();
            return new Result(true, "Success", carts);
        }

    }
}
