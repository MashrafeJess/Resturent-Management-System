using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database;
using static Database.Context.Context;
namespace Business
{
    public class FoodService
    {
        ResturantContext context = new ResturantContext();
        public Result Add(Food food)
        {
            if (context.Food.Any(x => x.FoodName == food.FoodName))
                return new Result(false, "Food already exist!");
            context.Food.Add(food);
            return new Result().DBcommit(context, "Save Successfully!", null, food);
        }
        public Result Delete(Food food)
        {
            var existingFood = context.Food.Find(food.FoodId);
            if (existingFood == null)
                return new Result(false, "Food not found!");
            context.Food.Remove(existingFood);
            return new Result().DBcommit(context, "Delete Successfully!", null, food);
        }
        public Result Update(Food food)
        {
            if (!context.Food.Any(x => x.FoodId == food.FoodId))
                return new Result(false, "Food not exist!");
            context.Food.Update(food);
            return new Result().DBcommit(context, "Updated Successfully!", null, food);
        }
        public Result List()
        {
            var foods = context.Food.ToList();
            return new Result(true, "Success", foods);
        }
        public Result Single(int Id)
        {
            var food = context.Food.Where(x => x.FoodId == Id).FirstOrDefault();
            return new Result(true, "Success", food);
        }
    }
}
