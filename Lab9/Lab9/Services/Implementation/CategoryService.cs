using Lab9.DTOs;
using Lab9.Models;
using Lab9.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Lab9.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;

        public CategoryService(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllAsync()
        {
            var categories = await _repository.GetAllAsync();
            return categories.Select(c => new CategoryDto { Id = c.Id, Name = c.Name });
        }

        public async Task<CategoryDto?> GetByIdAsync(int id)
        {
            var c = await _repository.GetByIdAsync(id);
            return c == null ? null : new CategoryDto { Id = c.Id, Name = c.Name };
        }

        public async Task CreateAsync(CategoryDto dto)
        {
            await _repository.AddAsync(new Category { Name = dto.Name });
        }

        public async Task UpdateAsync(int id, CategoryDto dto)
        {
            var category = await _repository.GetByIdAsync(id);
            if (category != null)
            {
                category.Name = dto.Name;
                await _repository.UpdateAsync(category);
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var category = await _repository.GetByIdAsync(id);
            if (category == null) return false;

            try
            {
                await _repository.DeleteAsync(id);
                return true;
            }
            catch (Exception)
            {
                // Nếu database có ràng buộc Restrict, nó sẽ quăng lỗi vào đây khi còn sản phẩm
                throw new Exception("Không thể xóa danh mục do vi phạm ràng buộc dữ liệu (vẫn còn sản phẩm)!");
            }
        }
    }
}