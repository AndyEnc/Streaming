using Application.Services;
using Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ITLATv.Controllers
{
    public class ITLAMovieController : Controller
    {
        private readonly SeriesService _seriesService;
        private readonly GenderService _genderService;
        private readonly ProducerService _producerService;

        public ITLAMovieController(SeriesService seriesService, GenderService genderService, ProducerService producerService)
        {
            _seriesService = seriesService;
            _genderService = genderService;
            _producerService = producerService;
        }

       

        public async Task<IActionResult> Index() 
        {
            List<ProducerViewModel> producers = await _producerService.GetAllProducer();
            ViewBag.producers = producers;

            List<GenderViewModel> Gender= await _genderService.GetAllGender();
            ViewBag.Genders = Gender;

            return View(await _seriesService.GetAllSeries());
        }
        public async Task<IActionResult> ContainerVideo(int id)
        {
            List<GenderViewModel> Gender = await _genderService.GetAllGender();
            ViewBag.Genders = Gender;
            return View("ContainerVideo", await _seriesService.GetById(id));


        }

        public async Task<IActionResult> SearchName(string name) 
        {
            List<ProducerViewModel> producers= await _producerService.GetAllProducer();
            ViewBag.producers = producers;

            List<GenderViewModel> genders = await _genderService.GetAllGender();
            ViewBag.Genders = genders;

            var SerieViewModel = await _seriesService.GetByName(name);
            List<SeriesViewModel> series = new List<SeriesViewModel> {  SerieViewModel };

            return View("Index", series);



        }

        public async Task<IActionResult> SearchProducer(ProducerViewModel producer) 
        {
            List<ProducerViewModel> producers = await _producerService.GetAllProducer();
            ViewBag.producers = producers;

            List<GenderViewModel> genders = await _genderService.GetAllGender();
            ViewBag.Genders = genders;

            var serieViewModel = await _seriesService.GetByProducer(producer);

            return View("Index", serieViewModel);
        }
        public async Task<IActionResult> SearchGender(GenderViewModel gender)
        {
            List<GenderViewModel> genreList = await _genderService.GetAllGender();
            ViewBag.Genders = genreList;

            List<ProducerViewModel> producersList = await _producerService.GetAllProducer();
            ViewBag.producers = producersList;

            var serieViewModel = await _seriesService.GetSeriesByGender(gender);

            return View("Index", serieViewModel);
        }




    }
}
