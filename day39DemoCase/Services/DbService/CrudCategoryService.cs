using Day39CaseStudy.DataAccess;
using Day39CaseStudy.DataAccess.Models;
using Day39CaseStudy.Services.DbService.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Day39CaseStudy.Services.DbService
{
    public class CrudCategoryService : ICrudService<Category>
    {
        public async Task AddAsync(Category category)
        {
            using var context = new SampleStoreDbContext();

            await context.Categories.AddAsync(category);
            await context.SaveChangesAsync();
        }

        public async Task< IEnumerable<Category>> GetAllAsync()
        {
            using var context = new SampleStoreDbContext();

            //return context.Categories.ToList();
            var category = from c in context.Categories
                           select c;
            return await category.ToListAsync();
        }

        public async Task UpdateAsync(Category category)
        {
            using var context = new SampleStoreDbContext();

            context.Categories.Update(category);
            await context.SaveChangesAsync();
        }

        public async Task<Category> GetByNameAsync(string categoryName)
        {
            using var context = new SampleStoreDbContext();

            //var category = context.Categories.SingleOrDefault(c => c.CategoryName == categoryName);

            var category = from c in context.Categories
                           where c.CategoryName == categoryName
                           select c;

            return await category.FirstAsync();
        }
        
        public async Task DeleteAsync(int categoryId)
        {
            using var context = new SampleStoreDbContext();

            //var category = context.Categories.Find(categoryId);

            var category = from c in context.Categories
                           where c.CategoryId == categoryId
                           select c;

            if (category == null)
            {
                Console.WriteLine($"CategoryId {categoryId} not found");
                return;
            }

            context.Categories.Remove(await category.FirstOrDefaultAsync());

            await context.SaveChangesAsync(); 
        }
    }
}



