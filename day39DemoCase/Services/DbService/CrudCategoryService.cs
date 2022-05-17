using Day39CaseStudy.DataAccess;
using Day39CaseStudy.DataAccess.Models;
using Day39CaseStudy.Services.DbService.Interfaces;

namespace Day39CaseStudy.Services.DbService
{
    public class CrudCategoryService : ICrudService<Category>
    {
        public void Add(Category category)
        {
            using var context = new SampleStoreDbContext();

            context.Categories.Add(category);
            context.SaveChanges();
        }

        public IEnumerable<Category> GetAll()
        {
            using var context = new SampleStoreDbContext();

            //return context.Categories.ToList();
            var category = from c in context.Categories.ToList()
                           select c;
            return category;
        }

        public void Update(Category category)
        {
            using var context = new SampleStoreDbContext();

            context.Categories.Update(category);
            context.SaveChanges();
        }

        public Category GetByName(string categoryName)
        {
            using var context = new SampleStoreDbContext();

            //var category = context.Categories.SingleOrDefault(c => c.CategoryName == categoryName);

            var category = from c in context.Categories
                           where c.CategoryName == categoryName
                           select c;
            return category.First();
        }

        public void Delete(int categoryId)
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

            context.Categories.Remove(category.First());
            context.SaveChanges();
        }
    }
}



