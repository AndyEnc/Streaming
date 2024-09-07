using Application.Services;
using Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ITLATv.Controllers
{
    public class GenderController : Controller
    {
        private readonly GenderService _gender;

        public GenderController(GenderService gender)
        {
            _gender = gender;
        }


        public async Task <IActionResult> Index()
        {
            return View(await _gender.GetAllGender());
        }

        public IActionResult Create() 
        {
            return View("CreateEditGender", new SaveGenderViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> Create(SaveGenderViewModel saveGender)
        {
            if (!ModelState.IsValid)
            {
                return View("CreateEditGenre", saveGender);
            }

            await _gender.CreateGender(saveGender);
            return RedirectToRoute(new { controller = "Gender", action = "Index" });
        }
        public async Task <IActionResult> Update(int id)
        {
            return View("CreateEditGender", await _gender.GetGenderById(id));
        }
        [HttpPost]
        public async Task<IActionResult> Update(SaveGenderViewModel saveGender)
        {
            if (!ModelState.IsValid)
            {
                return View("CreateEditGenre", saveGender);
            }

            await _gender.UpdateGender(saveGender);
            return RedirectToRoute(new { controller = "Gender", action = "Index" });
        }

        public async Task <IActionResult> DeleteGender(int id) 
        {
            return View(await _gender.GetGenderById(id));
        }
        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            await _gender.DeleteGender(id);
            return RedirectToRoute(new { controller = "Gender", action = "Index" }); ;
        }

    }
}
