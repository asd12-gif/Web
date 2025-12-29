using Lab9.DTOs;
namespace Lab9.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllAsync();
        Task<ProductDto?> GetByIdAsync(int id);
        Task CreateAsync(CreateProductDto dto); 
        Task UpdateAsync(int id, CreateProductDto dto);
        Task DeleteAsync(int id);
    }
}