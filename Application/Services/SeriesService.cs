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
    public class SeriesService
    {
        private readonly SeriesRepository _seriesRepository;

        public SeriesService(ApplicationContext context) 
        {
            _seriesRepository = new SeriesRepository(context);
        }

        public async Task<List<SeriesViewModel>> GetAllSeries() 
        {
            var serie = await _seriesRepository.GetAllSeries();

            return serie.Select(series => new SeriesViewModel
            {
                Id = series.Id,
                Name = series.Name,
                ImageP = series.ImageP,
                URL = series.URL,
                ProducerName = series.Producer.Name,

                Genders = series.SeriesGenderList
                .Where(s => s.IsPrimary)
                .Select(s => s.Gender.Name)
                .ToList(),

                GendersSecondary= series.SeriesGenderList
                .Where(s => !s.IsPrimary)
                .Select (s => s.Gender.Name)
                .ToList()
            }).ToList();
        }
        public async Task CreateSerie(SaveSeriesViewModel saveSerie)
        {
            List<SeriesGender> genresPrimary = saveSerie.Genders
            .Select(genderId => new SeriesGender { GenderId = genderId, IsPrimary = true })
            .ToList();

            List<SeriesGender> genderSecondary = saveSerie.GendersSecondary
           .Select(genderId => new SeriesGender { GenderId = genderId, IsPrimary = false })
            .ToList();

            List<SeriesGender> AllGender = genresPrimary.Concat(genderSecondary).ToList();


            Series serie = new Series()
            {
                Name = saveSerie.Name,
                ImageP = saveSerie.ImageP,
                URL = saveSerie.URL,
                ProducerId = saveSerie.ProducerId,
                SeriesGenderList = AllGender
            };

            await _seriesRepository.AddAsync(serie);
        }
        public async Task UpdateSerie(SaveSeriesViewModel saveSerie)
        {
            Series Serie = await _seriesRepository.GetSerieById(saveSerie.Id);

            if (Serie == null)
            {
                throw new Exception("Serie not found");
            }

            Serie.Name = saveSerie.Name;
            Serie.ImageP = saveSerie.ImageP;
            Serie.URL = saveSerie.URL;
            Serie.ProducerId = saveSerie.ProducerId;

            var primaryGender = saveSerie.Genders
                .Select(genderId => new SeriesGender { GenderId = genderId, SerieId = saveSerie.Id, IsPrimary = true });

            var secondaryGender = saveSerie.GendersSecondary
                .Select(genderId => new SeriesGender { GenderId = genderId, SerieId = saveSerie.Id, IsPrimary = false });

            List<SeriesGender> allGenres = primaryGender.Concat(secondaryGender).ToList();

            Serie.SeriesGenderList = allGenres;

            await _seriesRepository.UpdateAsync(Serie);
        }

        public async Task DeleteSerie(int id) 
        {
            var serie = await _seriesRepository.GetSerieById(id);
            await _seriesRepository.Delete(serie);
        }

        public async Task<SaveSeriesViewModel> GetById(int id) 
        {
            var serie = await _seriesRepository.GetSerieById(id);
            SaveSeriesViewModel saveSeriesViewModel = new()
            {
                Id = serie.Id,
                Name = serie.Name,
                ImageP = serie.ImageP,
                URL = serie.URL,
                ProducerId = serie.ProducerId,
                
                Genders = serie.SeriesGenderList
                .Where(s => s.IsPrimary && s.Gender != null)
                .Select(s => s.Gender.Id)
                .ToList(),
                

                GendersSecondary = serie.SeriesGenderList
                .Where(s => !s.IsPrimary && s.Gender != null)
                .Select(s => s.Gender.Id)
                .ToList(),

            };
            return saveSeriesViewModel;
        }

        public async Task<SeriesViewModel> GetByName(string name) 
        {
            var serie= await _seriesRepository.GetSeriesByName(name);

            if(serie != null) 
            {
                SeriesViewModel saveSerieViewModel = new()
                {
                    Id = serie.Id,
                    Name = serie.Name,
                    ImageP = serie.ImageP,
                    URL = serie.URL,
                    ProducerName = serie.Producer.Name,
                    Genders = serie.SeriesGenderList.Select(s => s.Gender.Name)
                    .ToList()
                };
                return saveSerieViewModel;
            }
            else 
            {
                return null;
            }
        }

        public async Task<List<SeriesViewModel>> GetByProducer(ProducerViewModel producerViewModel) 
        {
            Producer producers=new Producer();
            producers.Name = producerViewModel.Name;
            producers.Id=producerViewModel.Id;
            var serie = await _seriesRepository.GetAllSeriesByProducer(producers);


            if(serie != null) 
            {
                return serie.Select(s => new SeriesViewModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    ImageP = s.ImageP,
                    URL = s.URL,
                    ProducerName = s.Producer.Name,
                    Genders = s.SeriesGenderList.Select(s => s.Gender.Name)
                    .ToList()
                }).ToList();
                
            }
            else 
            {
                return null;
            }
        }

        public async Task<List<SeriesViewModel>> GetSeriesByGender(GenderViewModel genderViewModel) 
        {
            Gender gender = new Gender();
            gender.Name = genderViewModel.Name;
            gender.Id = genderViewModel.Id;

            var serie= await _seriesRepository.GetAllSeriesByGender(gender);

            if(serie != null) 
            {
                return serie.Select(g => new SeriesViewModel
                {
                    Id = g.Serie.Id,
                    Name = g.Serie.Name,
                    ImageP = g.Serie.ImageP,
                    URL = g.Serie.URL,
                    ProducerName = g.Serie.Producer.Name,
                    Genders = g.Serie.SeriesGenderList.Select(g => g.Gender.Name)
                    .ToList()
                }).ToList();
            }
            else 
            {
                return null;
            }
        }


    }
}
