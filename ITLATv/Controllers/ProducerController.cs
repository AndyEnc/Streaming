using Application.Services;
using Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ITLATv.Controllers
{
    public class ProducerController : Controller
    {
        public ProducerService _producer;
        public ProducerController(ProducerService producer)
        {
            _producer = producer;
        }

        public async Task <IActionResult> Index()
        {
            return View(await _producer.GetAllProducer());
        }

        public IActionResult Create()
        {
            return View("CreateEditProducer", new SaveProducerViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaveProducerViewModel saveProducer)
        {
            if (!ModelState.IsValid)
            {
                return View("CreateEditProducer", saveProducer);
            }

            await _producer.CreateProducer(saveProducer);
            return RedirectToRoute(new { controller = "Producer", action = "Index" });
        }

        public async Task<IActionResult> Update(int id)
        {
            return View("CreateEditProducer", await _producer.GetProducerById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Update(SaveProducerViewModel saveProducer)
        {
            if (!ModelState.IsValid)
            {
                return View("CreateEditProducer", saveProducer);
            }

            await _producer.UpdateProducer(saveProducer);
            return RedirectToRoute(new { controller = "Producer", action = "Index" });
        }

        public async Task<IActionResult> DeleteProducer(int id)
        {
            return View(await _producer.GetProducerById(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            await _producer.DeleteProducer(id);
            return RedirectToRoute(new { controller = "Producer", action = "Index" }); ;
        }
    }
}
