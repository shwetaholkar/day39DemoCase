using Day39CaseStudy.DataAccess;
using Day39CaseStudy.DataAccess.Models;
using Day39CaseStudy.Services.DbService.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Day39CaseStudy.Services.DbService;

public class CrudBrandService : ICrudService<Brand>
{
    public async Task AddAsync(Brand brand)//insert operation 
    {
        using var context = new SampleStoreDbContext();

        await context.Brands.AddAsync(brand);
        await context.SaveChangesAsync();
    }

    public async Task <IEnumerable<Brand>> GetAllAsync() //display all 
    {
        using var context = new SampleStoreDbContext();

        //return context.Brands.ToList();
        var brand = from b in context.Brands
                    select b;
        return await brand.ToListAsync();

    }

    public async Task UpdateAsync(Brand brand)//update operation 
    {
        using var context = new SampleStoreDbContext();

        context.Brands.Update(brand);
       await context.SaveChangesAsync();
    }

    public async Task<Brand> GetByNameAsync(string brandName)//Display get by name 
    {
        using var context = new SampleStoreDbContext();

        //var brand = context.Brands.SingleOrDefault(b => b.BrandName == brandName);

        var brand = from b in context.Brands
                    where b.BrandName == brandName
                    select b;
        //return brand.FirstOrDefault();

        return await brand.FirstAsync();
    }

    public async Task  DeleteAsync(int brandId)//delete operation 
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

        context.Brands.Remove(await brand.FirstOrDefaultAsync());
        await context.SaveChangesAsync();
    }

    
}






