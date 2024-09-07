using Database.Models;
using Database;
using Microsoft.EntityFrameworkCore;
namespace Application.Repositories
{
    public class GenderRepository
    {
        private readonly ApplicationContext _context;

        public GenderRepository(ApplicationContext context) 
        {
            _context = context;
        }

         public async Task AddAsync(Gender gender) 
        {
            await _context.Genders.AddAsync(gender);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Gender gender) 
        {
            _context.Entry(gender).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task Delete(Gender gender) 
        {
            _context.Set<Gender>().Remove(gender);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Gender>> GetAllAsync() 
        {
            return await _context.Set<Gender>().ToListAsync();
        }

        public async Task<Gender> GetById(int id) 
        {
            var serieId = await _context.Genders.FirstOrDefaultAsync(i => i.Id == id);
            return serieId;
        }

        public async Task<Gender> GetGenderByName(string name) 
        {
            var GenderName= await _context.Genders.FirstOrDefaultAsync(N => N.Name == name);
            return GenderName;
        }

    }
   
}
