using Day39CaseStudy.DataAccess;
using Day39CaseStudy.DataAccess.Models;
using Day39CaseStudy.Services.DbService.Interfaces;

namespace Day39CaseStudy.Services.DbService;

public class CrudBrandService : ICrudService<Brand>
{
    public void Add(Brand brand)//insert operation 
    {
        using var context = new SampleStoreDbContext();

        context.Brands.Add(brand);
        context.SaveChanges();
    }

    public IEnumerable<Brand> GetAll() //display all 
    {
        using var context = new SampleStoreDbContext();

        //return context.Brands.ToList();
        var brand = from b in context.Brands.ToList()
                    select b;
        return brand;

    }

    public void Update(Brand brand)//update operation 
    {
        using var context = new SampleStoreDbContext();

        context.Brands.Update(brand);
        context.SaveChanges();
    }

    public Brand GetByName(string brandName)//Display get by name 
    {
        using var context = new SampleStoreDbContext();

        //var brand = context.Brands.SingleOrDefault(b => b.BrandName == brandName);

        var brand = from b in context.Brands
                    where b.BrandName == brandName
                    select b;
        //return brand.FirstOrDefault();

        return brand.First();
    }

    public void Delete(int brandId)//delete operation 
    {
        using var context = new SampleStoreDbContext();

        //var brand = context.Brands.Find(brandId);

        var brand = from b in context.Brands
                    where b.BrandId == brandId
                    select b;

        if (brand == null)
        {
            Console.WriteLine($"BrandId {brandId} not found");
            return;
        }

        context.Brands.Remove(brand.First());
        context.SaveChanges();
    }
}






