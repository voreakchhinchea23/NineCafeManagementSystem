

using DocumentFormat.OpenXml.Bibliography;

namespace NineCafeManagementSystem.Web.Controllers
{
    [Authorize(Roles = Roles.Admin)]
    public class ExpensesController(IExpenseService _expenseService) : Controller
    {
        // GET: Expenses
        public async Task<IActionResult> Index(int? year = null, int? month = null)
        {
            var now = DateTime.Now;
            var expenseYear = year ?? now.Year;
            var expenseMonth = month ?? now.Month;

            var expenses = await _expenseService.GetExpensesByMonthAsync(expenseYear, expenseMonth);

            // Prepare dropdowns
            ViewBag.Years = Enumerable.Range(now.Year - 2, 3);
            ViewBag.Months = Enumerable.Range(1, 12);

            // Pass data to view
            ViewBag.SelectedYear = expenseYear;
            ViewBag.SelectedMonth = expenseMonth;

            return View(expenses);
        }

        // GET: Expenses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expense = await _expenseService.GetExpenseByIdAsync(id.Value);
            if (expense == null)
            {
                return NotFound();
            }

            return View(expense);
        }

        // GET: Expenses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Expenses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ExpenseCreateVM expense)
        {
            if (ModelState.IsValid)
            {
                await _expenseService.CreateExpenseAsync(expense);
                return RedirectToAction(nameof(Index));
            }
            return View(expense);
        }

        // GET: Expenses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expense = await _expenseService.GetExpenseByIdForEditAsync(id);
            if (expense == null)
            {
                return NotFound();
            }
            return View(expense);
        }

        // POST: Expenses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ExpenseEditVM expense)
        {
            if (id != expense.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _expenseService.EditExpenseAsync(expense);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_expenseService.ExpenseExists(expense.Id))
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
            return View(expense);
        }

        // GET: Expenses/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var expense = await _expenseService.GetExpenseByIdAsync(id);
        //    if (expense == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(expense);
        //}

        // POST: Expenses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _expenseService.RemoveExpenseAysnc(id);
            return RedirectToAction(nameof(Index));
        }

        
        
    }
}
