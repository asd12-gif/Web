using Lab9.DTOs;
using Lab9.Models;
using Lab9.Repositories;
using Microsoft.AspNetCore.Http;

namespace Lab9.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly string _storagePath;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
            // Đường dẫn lưu file vật lý trên server
            _storagePath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");

            if (!Directory.Exists(_storagePath))
                Directory.CreateDirectory(_storagePath);
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            var products = await _repository.GetAllAsync();
            return products.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                ImageUrl = p.ImageUrl,
                CategoryId = p.CategoryId,
                CategoryName = p.Category?.Name ?? "Chưa phân loại"
            });
        }

        public async Task<ProductDto?> GetByIdAsync(int id)
        {
            var p = await _repository.GetByIdAsync(id);
            if (p == null) return null;

            return new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                ImageUrl = p.ImageUrl,
                CategoryId = p.CategoryId,
                CategoryName = p.Category?.Name ?? "Chưa phân loại"
            };
        }

        public async Task CreateAsync(CreateProductDto dto)
        {
            var product = new Product
            {
                Name = dto.Name,
                Price = dto.Price,
                CategoryId = dto.CategoryId
            };

            if (dto.Image != null)
            {
                string fileName = await SaveFile(dto.Image);
                product.ImageUrl = "/Uploads/" + fileName;
            }
            else
            {
                product.ImageUrl = dto.ImageUrl ?? "/Uploads/default.jpg";
            }

            await _repository.AddAsync(product);
        }
        public async Task UpdateAsync(int id, CreateProductDto dto)
        {
            var p = await _repository.GetByIdAsync(id);
            if (p != null)
            {
                p.Name = dto.Name;
                p.Price = dto.Price;
                p.CategoryId = dto.CategoryId;

                if (dto.Image != null)
                {
                    string fileName = await SaveFile(dto.Image);
                    p.ImageUrl = "/Uploads/" + fileName;
                }
                else if (!string.IsNullOrEmpty(dto.ImageUrl))
                {
                    p.ImageUrl = dto.ImageUrl;
                }

                await _repository.UpdateAsync(p);
            }
        }

        public async Task DeleteAsync(int id) => await _repository.DeleteAsync(id);

        private async Task<string> SaveFile(IFormFile file)
        {
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            string filePath = Path.Combine(_storagePath, fileName);
            using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);
            return fileName;
        }
    }
}