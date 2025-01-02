using Grilo.Application.Repositories;
using Grilo.Domain.Dtos.Category;
using Grilo.Domain.Entities;
using Grilo.Infra.Database;
using Microsoft.EntityFrameworkCore;

namespace Grilo.Infra.Repositories
{
    public class CategoryRepository(DatabaseContext context) : ICategoryRepository
    {
        private readonly DatabaseContext _context = context;
        public async Task<bool> CheckCategoryByTitle(string title)
        {
            bool result = await _context.Category.FirstOrDefaultAsync(item => item.Title == title) is not null;
            return result;
        }

        public async Task<IList<GetAllCategoriesDTO>> Get(string? title)
        {
            IList<GetAllCategoriesDTO> result =
                await _context.Category.Select(item => new GetAllCategoriesDTO()
                {
                    Id = item.Id,
                    Title = item.Title,
                    CreatedAt = item.CreatedAt
                })
                .Where(item => string.IsNullOrEmpty(title) || item.Title == title)
                .ToListAsync();

            return result;
        }

        public async Task Save(CategoryEntity input)
        {
            _context.Category.Add(input);
            await _context.SaveChangesAsync();
        }
    }
}