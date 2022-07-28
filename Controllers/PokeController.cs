using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Login01.Models;


namespace Login01.Controllers
{
    public class PokeController : Controller
    {
        public readonly Login01.Data.AppCont _appCont;

        public PokeController(Login01.Data.AppCont appCont)
        {
            _appCont = appCont;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var all = await _appCont.Pokemons.ToListAsync();
            return View(all);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pokes = await _appCont.Pokemons.FirstOrDefaultAsync(x => x.Id == id);

            if (pokes == null)
            {
                return BadRequest();
            }
            return View(pokes);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Pokemon pokemon, IList<IFormFile> Img)
        {
            IFormFile img = Img.FirstOrDefault();
            MemoryStream ms = new MemoryStream();
            if (Img.Count > 0)
            {
                img.OpenReadStream().CopyTo(ms);
                pokemon.Image = ms.ToArray();
            }
            if (ModelState.IsValid)
            {
                _appCont.Pokemons.Add(pokemon);
                await _appCont.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(pokemon);

        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Poke = await _appCont.Pokemons.FirstOrDefaultAsync(x => x.Id == id);

            if (Poke == null)
            {
                return BadRequest();
            }

            return View(Poke);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, Pokemon pokemon, IList<IFormFile> Image)
        {
            if (id == null)
            {
                return NotFound();
            }
            var oldData = _appCont.Pokemons.AsNoTracking().FirstOrDefault(p => p.Id == id);
            IFormFile newImg = Image.FirstOrDefault();
            MemoryStream ms = new MemoryStream();
            if (Image.Count > 0)
            {
                newImg.OpenReadStream().CopyTo(ms);
                pokemon.Image = ms.ToArray();
            }
            else
            {
                pokemon.Image = oldData.Image;
            }

            if (ModelState.IsValid)
            {
                _appCont.Update(pokemon);
                await _appCont.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pokemon);

        }
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Poke = await _appCont.Pokemons.FirstOrDefaultAsync(p => p.Id == id);

            if (Poke == null)
            {
                return BadRequest();
            }

            return View(Poke);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var poke = await _appCont.Pokemons.FindAsync(id);
            _appCont.Pokemons.Remove(poke);
            await _appCont.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
