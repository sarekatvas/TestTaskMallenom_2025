using WebAPI.Data;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using Core.Interfaces;


namespace WebAPI.Repositories
{
    public class ImageRepository:IImageRepository
    {

        private readonly AppDbContext _appDbContext;
      
        public ImageRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<Image>> GetAllAsync()
        {
            return await _appDbContext.Images.AsNoTracking().ToListAsync();
        }

        public async Task<Image> AddAsync(Image image)
        {
            await _appDbContext.Images.AddAsync(image);
            await _appDbContext.SaveChangesAsync();
            return image;
        }

        public async Task DeleteAsync(int id)
        {
            var image = await _appDbContext.Images.FindAsync(id);

            if (image == null) return;
            
            _appDbContext.Images.Remove(image);
            await _appDbContext.SaveChangesAsync();
            
        }
    }
}
