using Day39CaseStudy.DataAccess.Models;
using Day39CaseStudy.Services.DbService.Interfaces;
using Day39CaseStudy.Services.Factory;

namespace Day39CaseStudy.Services.UserInterface
{
    public class UserInterfaceCrudCategoryService
    {

        readonly ICrudService<Category> _categoryService;

        public UserInterfaceCrudCategoryService()
        {
            _categoryService = CrudFactory.Create<Category>();
        }

        public async Task AddAsync()
        {
            Console.WriteLine("Adding New Category");
            Console.WriteLine("----------------");

            Console.Write("Enter Category Name: ");
            var categoryNameText = Console.ReadLine();

            var category = new Category { CategoryName = categoryNameText };

            await _categoryService.AddAsync(category);
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            return await _categoryService.GetAllAsync();
        }

        public async Task UpdateAsync()
        {
            Console.WriteLine("Updating existing Category");
            Console.WriteLine("-----------------------");

            Console.Write("Enter category Name to Update: ");
            var categoryNameText = Console.ReadLine();

            var category =await _categoryService.GetByNameAsync(categoryNameText);

            if (category == null)
            {
                Console.WriteLine($"Category Name {categoryNameText} not found!!");
                return;
            }

            Console.WriteLine($"Found Category: {category}");

            Console.Write("Enter Category Name to change: ");
            var changedCategoryNameText = Console.ReadLine();

            category.CategoryName = changedCategoryNameText;

           await _categoryService.UpdateAsync(category);
        }

        public async Task DeleteAsync()
        {
            Console.WriteLine("Deleting existing category");
            Console.WriteLine("-----------------------");

            Console.Write("Enter the Category Id to delete: ");
            var categoryIdText = Console.ReadLine();

            var categoryId = int.Parse(categoryIdText);

            try
            {
               await _categoryService.DeleteAsync(categoryId);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Delete Category Failed!! {ex.Message}");
                Console.ResetColor();
            }
        }

        public async Task ShowAsync()
        {
            var categories = await _categoryService.GetAllAsync();

            Console.WriteLine("Category List");
            Console.WriteLine("---------------------------------------");

            Console.WriteLine(Category.Header);
            Console.WriteLine("----------------------------------------");
            foreach (var category in categories)
            {
                Console.WriteLine(category);
            }
            Console.WriteLine("----------------------------------------");
        }
    }
}
