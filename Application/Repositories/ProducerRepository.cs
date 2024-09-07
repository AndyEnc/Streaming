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
    public class ProducerRepository
    {
        private readonly ApplicationContext _context;
        public ProducerRepository(ApplicationContext context) 
        { 
            _context = context;
        }

        public async Task AddAsync(Producer productora)
        {
            await _context.Producers.AddAsync(productora);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProducer(Producer producer) 
        {
            _context.Entry(producer).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task Delete(Producer producer) 
        {
            _context.Set<Producer>().Remove(producer);
            await _context.SaveChangesAsync();  
        }

        public async Task<List<Producer>> GetAllProductoraAsync() 
        {
            return await _context.Set<Producer>().ToListAsync();
        }
        public async Task<Producer> GetProducerById(int id)
        {
            var producerId = await _context.Producers.FirstOrDefaultAsync(p => p.Id == id);
            return producerId;
        }


    }
}
