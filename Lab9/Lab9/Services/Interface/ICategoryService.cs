using Lab9.DTOs;

namespace Lab9.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllAsync();
        Task<CategoryDto?> GetByIdAsync(int id);
        Task CreateAsync(CategoryDto dto);
        Task UpdateAsync(int id, CategoryDto dto);

        // Đảm bảo là Task<bool>
        Task<bool> DeleteAsync(int id);
    }
}