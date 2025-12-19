
namespace NineCafeManagementSystem.Web.Controllers
{
    [Authorize]
    public class PriceTiersController(IPriceTierService _priceTierService) : Controller
    {
        
        // GET: PriceTiers
        public async Task<IActionResult> Index()
        {
            return View(await _priceTierService.GetAllAsync());
        }

        // GET: PriceTiers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var priceTier = await _priceTierService.GetDetailByIdAsync(id.Value);
            if (priceTier == null)
            {
                return NotFound();
            }

            return View(priceTier);
        }

        // GET: PriceTiers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PriceTiers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PriceTierCreateVM model)
        {
            // adding custom validation
            if (await _priceTierService.CheckIfPriceTierAlreadyExistsAsync(model.PriceRiel) || model.PriceRiel <= 0)
            {
                ModelState.AddModelError(nameof(model.PriceRiel), "Price Already Exists OR Cannot Be Less Than Zero.");
            }
            

            if (ModelState.IsValid)
            {
                await _priceTierService.CreateAsync(model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }


        // -- Leave it for now -- 
        // Uncomment in Version 2
        /*
        // GET: PriceTiers/Edit/5
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var priceTier = await _priceTierService.GetUpdateByIdAsync(id.Value);
            if (priceTier == null)
            {
                return NotFound();
            }
            return View(priceTier);
        }

        // POST: PriceTiers/Edit/5
        [Authorize(Roles = Roles.Admin)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PriceTierUpdateVM model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }
            // adding custom validation
            if (await _priceTierService.CheckIfPriceTierAlreadyExistsAsync(model.PriceRiel) 
                || model.PriceRiel <= 0)
            {
                ModelState.AddModelError(nameof(model.PriceRiel), "Price Already Exists OR Cannot Be Less Than Zero.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _priceTierService.EditAsync(model);   
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_priceTierService.PriceTierExists(model.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: PriceTiers/Delete/5
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var priceTier = await _priceTierService.GetDetailByIdAsync(id.Value);
            if (priceTier == null)
            {
                return NotFound();
            }

            return View(priceTier);
        }

        [Authorize(Roles = Roles.Admin)]
        // POST: PriceTiers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _priceTierService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }
        */
        
    }
}
