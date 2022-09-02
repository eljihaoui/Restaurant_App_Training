﻿using Restaurant.DAL.Interfaces;
using Restaurant.Models;

namespace Restaurant.DAL.Implementations
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public RestaurantDBContext _db => _context as RestaurantDBContext;
        public CategoryRepository(RestaurantDBContext context) : base(context)
        {

        }
     
        public void Update(Category category)
        {
            Category categ = _db.Category.Find(category.Id);
            if (categ != null)
            {
                categ.Name = category.Name;
                categ.DisplayOrder = category.DisplayOrder;
            }
        }
    }
}
