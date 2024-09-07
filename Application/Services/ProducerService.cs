using Application.Repositories;
using Application.ViewModels;
using Database;
using Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ProducerService
    {
        private readonly ProducerRepository _producerRepository;

        public ProducerService( ApplicationContext context) 
        {
            _producerRepository = new ProducerRepository(context);
        }

        public async Task<List<ProducerViewModel>> GetAllProducer()
        {


            var prodctList = await _producerRepository.GetAllProductoraAsync();


            return prodctList.Select(producer => new ProducerViewModel
            {

                Id = producer.Id,
                Name= producer.Name


            }).ToList();

        }

        public async Task CreateProducer(SaveProducerViewModel saveProducer) 
        {
            Producer producer = new() { Id = saveProducer.Id,
                Name = saveProducer.Name };
            await _producerRepository.AddAsync(producer);
        }

        public async Task UpdateProducer(SaveProducerViewModel saveproducer) 
        {
            Producer producer = await _producerRepository.GetProducerById(saveproducer.Id);
            producer.Name = saveproducer.Name;
            producer.Id= saveproducer.Id;

            await _producerRepository.UpdateProducer(producer);
        }
        public async Task DeleteProducer(int id) 
        {
            var producer = await _producerRepository.GetProducerById(id);
            await _producerRepository.Delete(producer);
        }
        public async Task<SaveProducerViewModel> GetProducerById(int id) 
        {
            var producer = await _producerRepository.GetProducerById(id);

            SaveProducerViewModel savep = new SaveProducerViewModel();

        savep.Name = producer.Name;
            savep.Id = producer.Id;
            return savep;
        }

    }
}
