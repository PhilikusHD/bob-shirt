using Bob.Core.Domain;
using Bob.Core.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bob.Core.Services
{
#nullable enable
    public class ProductService
    {
        private readonly ProductRepository m_ProductRepository;

        public ProductService(ProductRepository repository)
        {
            m_ProductRepository = repository;
        }

        public async Task<Product?> GetProductByIdAsync(int productId)
        {
            return await m_ProductRepository.GetByIdAsync(productId);
        }

        public async Task<IReadOnlyList<Product>> GetAllProductsAsync()
        {
            return await m_ProductRepository.GetAllAsync();
        }

        public async Task AddProductAsync(Product product)
        {
            await m_ProductRepository.AddAsync(product);
        }

        public async Task UpdateProductAsync(int product)
        {
            await m_ProductRepository.UpdateAsync(product);
        }

        public async Task DeleteProductAsync(int productId) => await m_ProductRepository.DeleteAsync(productId);
    }
}
