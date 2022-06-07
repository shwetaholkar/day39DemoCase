using Day39CaseStudy.DataAccess.Models;
using Day39CaseStudy.Services.DbService.Interfaces;
using Day39CaseStudy.Services.Factory;

namespace Day39CaseStudy.Services.UserInterface;

public class UserInterfaceCrudProductService
{
    readonly ICrudService<Product> _productService;
    readonly ICrudService<Brand> _brandService;

    public UserInterfaceCrudProductService()
    {
        _productService = CrudFactory.Create<Product>();
        _brandService = CrudFactory.Create<Brand>();
    }

    public async Task AddAsync()
    {
        var product = new Product();

        Console.WriteLine("Adding New Product");
        Console.WriteLine("----------------");

        Console.Write("Enter Product Name: ");
        var productNameText = Console.ReadLine();
        product.ProductName = productNameText;

        Console.Write("Enter Brand Id: ");
        var brandIdText = Console.ReadLine();
        product.BrandId = int.Parse(brandIdText);

        Console.Write("Enter CategoryId: ");
        var categoryIdText = Console.ReadLine();
        product.CategoryId = int.Parse(categoryIdText);

        Console.Write("Enter Model Year: ");
        var modelYearText = Console.ReadLine();
        product.ModelYear = short.Parse(modelYearText);

        Console.Write("Enter List Price: ");
        var listPriceText = Console.ReadLine();
        product.ListPrice = int.Parse(listPriceText);

        try
        {
           await _productService.AddAsync(product);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error Adding product: {ex.Message}");
        }
    }

    public async Task<IEnumerable<Product>> GetAll()
    {
        return await _productService.GetAllAsync();
    }

    public async Task UpdateAsync()
    {
        Console.WriteLine("Updating existing Product");
        Console.WriteLine("-----------------------");

        Console.Write("Enter Product Name to Update: ");
        var productNameText = Console.ReadLine();

        var product =await _productService.GetByNameAsync(productNameText);

        if (product == null)
        {
            Console.WriteLine($"Product Name {productNameText} not found!!");
            return;
        }

        Console.WriteLine($"Found Product: {product}");
        Console.WriteLine("-------------------------------------------------------");

        Console.Write("Enter Product Name to change: ");
        product.ProductName = Console.ReadLine();

        Console.Write("Enter Brand Id to change: ");
        var brandIdText = Console.ReadLine();
        product.BrandId = int.Parse(brandIdText);

        Console.Write("Enter CategoryId to change: ");
        var categoryIdText = Console.ReadLine();
        product.CategoryId = int.Parse(categoryIdText);

        Console.Write("Enter Model Year to change: ");
        var modelYearText = Console.ReadLine();
        product.ModelYear = short.Parse(modelYearText);

        Console.Write("Enter List Price to change: ");
        var listPriceText = Console.ReadLine();
        product.ListPrice = int.Parse(listPriceText);

        await _productService.UpdateAsync(product);
    }

    public async Task DeleteAsync()
    {
        Console.WriteLine("Deleting existing Product");
        Console.WriteLine("-----------------------");

        Console.Write("Enter the Product Id to delete: ");
        var productIdText = Console.ReadLine();
        var productId = int.Parse(productIdText);

         await _productService.DeleteAsync(productId);
    }

    public async Task Show()
    {
        var products =await _productService.GetAllAsync();
        var brands = await _brandService.GetAllAsync();

        //Console.WriteLine("Product List");
        //Console.WriteLine("--------------------------------------------------------------------------------------------------------------------------------------------------------------");

        //Console.WriteLine(Product.Header);
        //Console.WriteLine("--------------------------------------------------------------------------------------------------------------------------------------------------------------");
        //foreach (var product in products)
        //{
        //    Console.WriteLine(product);
        //}
        //Console.WriteLine("------------------");


        Console.WriteLine("Brand Wise Product Display");

        foreach (var brand in brands)
        {
            Console.WriteLine("------------------------------");
            Console.WriteLine(Brand.Header);
            Console.WriteLine("------------------------------");
            Console.WriteLine(brand.ToString());
            Console.WriteLine("--------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine(Product.Header);
            Console.WriteLine("--------------------------------------------------------------------------------------------------------------------");

            foreach (var product in products)
            {
                if (brand.BrandId == product.BrandId)
                {
                    Console.WriteLine(product);
                }

                continue;

            }
            Console.WriteLine();
            Console.WriteLine();

        }
        Console.WriteLine("-------------------------------------------------------------------------------------------------------------------------");
        Console.WriteLine();

    }
}










