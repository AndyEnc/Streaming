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
    public class GenderService
    {
        private readonly GenderRepository _genderRepository;

        public GenderService(ApplicationContext context) 
        {
            _genderRepository = new GenderRepository(context);
        }

        public async Task<List<GenderViewModel>> GetAllGender() 
        {
            var gender= await _genderRepository.GetAllAsync();

            return gender.Select(g => new GenderViewModel { Id = g.Id, Name = g.Name }).ToList();
        }

        public async Task CreateGender(SaveGenderViewModel saveGender) 
        {
            Gender gender = new Gender { Name = saveGender.Name };

            await _genderRepository.AddAsync(gender);
        }
        public async Task UpdateGender(SaveGenderViewModel SaveGender)
        {
            Gender gender = await _genderRepository.GetById(SaveGender.Id);
            gender.Id = SaveGender.Id;
            gender.Name = SaveGender.Name;

            await _genderRepository.UpdateAsync(gender);
        }

        public async Task DeleteGender(int id) 
        {
            var gender= await _genderRepository.GetById(id);
            await _genderRepository.Delete(gender);
        }

        public async Task<SaveGenderViewModel> GetGenderById(int id) 
        {
            var gender= await _genderRepository.GetById(id);
            SaveGenderViewModel saveGender = new()
            {
                Id = gender.Id,
                Name = gender.Name
                
            };
            return saveGender;
        }

        public async Task<SaveGenderViewModel> GetGenderByName(string name) 
        {
            var serie = await _genderRepository.GetGenderByName(name);
            SaveGenderViewModel saveGender = new()
            {
                Name = serie.Name,
                Id = serie.Id
            };
            return saveGender;
        }


    }

}
