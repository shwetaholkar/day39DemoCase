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

        public void Add()
        {
            Console.WriteLine("Adding New Category");
            Console.WriteLine("----------------");

            Console.Write("Enter Category Name: ");
            var categoryNameText = Console.ReadLine();

            var category = new Category { CategoryName = categoryNameText };

            _categoryService.Add(category);
        }

        public IEnumerable<Category> GetAll()
        {
            return _categoryService.GetAll();
        }

        public void Update()
        {
            Console.WriteLine("Updating existing Category");
            Console.WriteLine("-----------------------");

            Console.Write("Enter category Name to Update: ");
            var categoryNameText = Console.ReadLine();

            var category = _categoryService.GetByName(categoryNameText);

            if (category == null)
            {
                Console.WriteLine($"Category Name {categoryNameText} not found!!");
                return;
            }

            Console.WriteLine($"Found Category: {category}");

            Console.Write("Enter Category Name to change: ");
            var changedCategoryNameText = Console.ReadLine();

            category.CategoryName = changedCategoryNameText;

            _categoryService.Update(category);
        }

        public void Delete()
        {
            Console.WriteLine("Deleting existing category");
            Console.WriteLine("-----------------------");

            Console.Write("Enter the Category Id to delete: ");
            var categoryIdText = Console.ReadLine();

            var categoryId = int.Parse(categoryIdText);

            try
            {
                _categoryService.Delete(categoryId);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Delete Category Failed!! {ex.Message}");
                Console.ResetColor();
            }
        }

        public void Show()
        {
            var categories = _categoryService.GetAll();

            Console.WriteLine("Category List");
            Console.WriteLine("----------");

            Console.WriteLine(Category.Header);
            Console.WriteLine("------------------");
            foreach (var category in categories)
            {
                Console.WriteLine(category);
            }
            Console.WriteLine("------------------");
        }
    }
}
