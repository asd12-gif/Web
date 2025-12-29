using Lab9.Models;

namespace Lab9.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync(); 
        Task<Product?> GetByIdAsync(int id); 
        Task AddAsync(Product product); 
        Task UpdateAsync(Product product); 
        Task DeleteAsync(int id);
    }
}