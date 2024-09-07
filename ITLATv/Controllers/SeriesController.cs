using Application.Services;
using Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ITLATv.Controllers
{
    public class SeriesController : Controller
    {
        private readonly SeriesService _seriesService;
        private readonly GenderService _genderService;
        private readonly ProducerService _producerService;

        public SeriesController(SeriesService seriesService, GenderService genderService, ProducerService producerService)
        {
            _seriesService = seriesService;
            _genderService = genderService;
            _producerService = producerService;
        }

        public async Task <IActionResult> Index()
        {
            return View(await _seriesService.GetAllSeries());

        }

        public async Task<IActionResult> Create() 
        {
            List<ProducerViewModel> producersList = await _producerService.GetAllProducer();
            List<GenderViewModel> genreList = await _genderService.GetAllGender();
            ViewBag.producers = producersList;
            ViewBag.Genders = genreList;
            return View("SaveSerie", new SaveSeriesViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaveSeriesViewModel vm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.producers = await _producerService.GetAllProducer();
                    ViewBag.Genders = await _genderService.GetAllGender();
                    return View("Create", vm);
                }
                await _seriesService.CreateSerie(vm);
                return RedirectToRoute(new { controller = "Series", action = "Index" });
            }
            catch (Exception e)
            {
                return View("Index", e);
            }

        }

        public async Task<IActionResult> Update(int id) 
        {
            ViewBag.producers = await _producerService.GetAllProducer();
            ViewBag.Genders = await _genderService.GetAllGender();
            return View("SaveSerie", await _seriesService.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Update(SaveSeriesViewModel vm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.producers = await _producerService.GetAllProducer();
                    ViewBag.Genders = await _genderService.GetAllGender();
                    return View("Create", vm);
                }
                await _seriesService.UpdateSerie(vm);
                return RedirectToRoute(new { controller = "Series", action = "Index" });
            }
            catch (Exception e)
            {
                return View("Index", e);
            }

        }

        public async Task<IActionResult> Delete(int id) 
        {
            var serieToDelete = await _seriesService.GetById(id);
            var saveSeriesViewModel = new SaveSeriesViewModel
            {
                Id = serieToDelete.Id,
                Name = serieToDelete.Name,
            };

            return View("Delete", saveSeriesViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> DeletePost (int id)
        {
            await _seriesService.DeleteSerie(id);
            return RedirectToRoute(new { controller = "Series", action = "Index" });
        }
    }
}
