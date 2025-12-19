
namespace NineCafeManagementSystem.Web.Controllers
{
    [Authorize]
    public class SuppliersController(ISupplierService _supplierService) : Controller
    {

        // GET: Suppliers
        public async Task<IActionResult> Index()
        {
            return View(await _supplierService.GetAllSuppliersAsync());
        }

        // GET: Suppliers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplier = await _supplierService.GetSupplierByIdAsync(id.Value);
            if (supplier == null)
            {
                return NotFound();
            }

            return View(supplier);
        }

        // GET: Suppliers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Suppliers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SupplierCreateVM supplier)
        {
            if(await _supplierService.PhoneNumberExistsAsync(supplier.ContactInfo))
            {
                ModelState.AddModelError(nameof(supplier.ContactInfo), "Phone number is already in use.");
            }
            
            if (ModelState.IsValid)
            {
                await _supplierService.CreateSupplierAsync(supplier);
                return RedirectToAction(nameof(Index));
            }
            return View(supplier);
        }

        // GET: Suppliers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var supplier = await _supplierService.GetSupplierByIdForEditAsync(id.Value);
            if (supplier == null)
            {
                return NotFound();
            }
            return View(supplier);
        }

        // POST: Suppliers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SupplierUpdateVM supplier)
        {
            if (id != supplier.Id)
            {
                return NotFound();
            }
            if(await _supplierService.IsNumberAlreadyUsedByAnotherSupplierAsync(id, supplier.ContactInfo))
            {
                ModelState.AddModelError(nameof(supplier.ContactInfo), "Phone number is already in use by another user.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                   await _supplierService.EditSupplierAsync(supplier);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_supplierService.SupplierExists(supplier.Id))
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
            return View(supplier);
        }

        [Authorize(Roles = Roles.Admin)]
        // GET: Suppliers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = Roles.Admin)]
        // POST: Suppliers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            await _supplierService.RemoveSupplierAysnc(id);
            return RedirectToAction(nameof(Index));
        }

        
    }
}
