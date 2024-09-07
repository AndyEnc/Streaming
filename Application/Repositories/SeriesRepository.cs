using Database;
using Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public class SeriesRepository
    {
        private readonly ApplicationContext _context;

        public SeriesRepository(ApplicationContext context) 
        {
            _context = context;
        }

        public async Task AddAsync(Series series) 
        {
            await _context.Series.AddAsync(series);
            await _context.SaveChangesAsync();  
        }

        public async Task UpdateAsync(Series series) 
        {
            _context.Entry(series).State=EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task Delete(Series series) 
        {
             _context.Set<Series>().Remove(series);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Series>> GetAllSeries() 
        {
            return await _context.Set<Series>()
                .Include(x => x.Producer)
                .Include(x => x.SeriesGenderList)
                .ThenInclude(Gender => Gender.Gender)
                .ToListAsync();
        }
        public async Task<Series> GetSeriesByName(string name) 
        {
            var serie = await _context.Set<Series>()
                .Include(x => x.Producer)
                .Include(x => x.SeriesGenderList)
                .ThenInclude(Gender=> Gender.Gender)
                .ToListAsync();

            return serie.FirstOrDefault(s => s.Name.Equals(name, StringComparison.OrdinalIgnoreCase));  
        }
        public async Task<Series> GetSerieById(int id)
        {
            var serieId = await _context.Series.Include(x => x.SeriesGenderList).FirstOrDefaultAsync(i => i.Id == id);
            return serieId;
        }

        public async Task<List<Series>> GetAllSeriesByProducer(Producer producer) 
        {
            return await _context.Series
                .Include(x => x.SeriesGenderList)
                .ThenInclude(Gender => Gender.Gender)
                .Where(s => s.ProducerId == producer.Id)
                .ToListAsync();
        }

        public async Task<List<SeriesGender>> GetAllSeriesByGender(Gender gender) 
        {
            return await _context.SeriesGenders
                .Include(x => x.Serie)
                .Include(x => x.Serie.Producer)
                .Where(s => s.GenderId == gender.Id)
                .ToListAsync();
        }


    }
}
