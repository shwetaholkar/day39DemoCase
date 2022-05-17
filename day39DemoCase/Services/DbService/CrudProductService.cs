using Day39CaseStudy.DataAccess;
using Day39CaseStudy.DataAccess.Models;
using Day39CaseStudy.Services.DbService.Interfaces;

namespace Day39CaseStudy.Services.DbService;

public class CrudProductService : ICrudService<Product>
{
    public void Add(Product product)
    {
        using var context = new SampleStoreDbContext();

        context.Products.Add(product);
        context.SaveChanges();
    }

    public IEnumerable<Product> GetAll()
    {
        using var context = new SampleStoreDbContext();

        //return context.Products
        //    .Include("Brand")
        //    .Include("Category")
        //    .OrderBy(p => p.BrandId)
        //        .ThenBy(p=> p.ProductId)
        //    .ToList();

        var product = (from p in context.Products
                       join b in context.Brands
                       on p.BrandId equals b.BrandId
                       join c in context.Categories
                       on p.CategoryId equals c.CategoryId
                       orderby p.BrandId
                       select new Product
                       {
                           ProductId = p.ProductId,
                           ProductName = p.ProductName,
                           BrandId = p.BrandId,
                           Brand = b,
                           CategoryId = p.CategoryId,
                           Category = c,
                           ModelYear = p.ModelYear,
                           ListPrice = p.ListPrice

                       }).ToList();

        return product;
    }

    //public IEnumerable<Product> GetAllBrandWise()
    //{
    //    // select brand id , brand name,ProductId, ProductName, CategoryId, CategoryName, ModelYear, ListPrice
    //    // from Brand b
    //    // inner join  product p
    //    // on b.brandId = p.brandId 
    //    //inner join Category c
    //    //on c.categoryId = p.categoryID

    //    using var context = new SampleStoreDbContext();

    //    var product = (from p in context.Products
    //                   join b in context.Brands
    //                   on p.BrandId equals b.BrandId
    //                   join c in context.Categories
    //                   on p.CategoryId equals c.CategoryId
    //                   orderby p.BrandId
    //                   select new Product
    //                   {
    //                       BrandId = p.BrandId,
    //                       Brand = b,
    //                       ProductId = p.ProductId,
    //                       ProductName = p.ProductName,
    //                       CategoryId = p.CategoryId,
    //                       Category = c,
    //                       ModelYear = p.ModelYear,
    //                       ListPrice = p.ListPrice

    //                   }).ToList();
    //    return product;
    //}

    public void Update(Product product)
    {
        using var context = new SampleStoreDbContext();

        context.Products.Update(product);
        context.SaveChanges();
    }

    public Product GetByName(string productName)
    {
        using var context = new SampleStoreDbContext();

        //var product = context.Products.SingleOrDefault(b => b.ProductName == productName);

        var category = from c in context.Products
                       where c.ProductName == productName
                       select c;
        return category.First();
    }

    public void Delete(int productId)
    {
        using var context = new SampleStoreDbContext();

        // var product = context.Products.Find(productId);

        var product = from p in context.Products
                      where p.ProductId == productId
                      select p;

        if (product == null)
        {
            Console.WriteLine($"ProductId {productId} not found");
            return;
        }

        context.Products.Remove(product.First());
        context.SaveChanges();
    }
}

