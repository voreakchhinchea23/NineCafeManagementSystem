using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NineCafeManagementSystem.Web.Data;

namespace NineCafeManagementSystem.Web.Controllers
{
    [Authorize]
    public class DeptsController(IDeptService _deptService) : Controller
    {
        
        // GET: Depts
        public async Task<IActionResult> Index()
        {
            return View(await _deptService.GetAllDeptsAsync());
        }

        // GET: Depts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dept = await _deptService.GetDeptByIdAsync(id.Value);
            if (dept == null)
            {
                return NotFound();
            }

            return View(dept);
        }

        // GET: Depts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Depts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DeptsCreateVM dept)
        {
            if(await _deptService.CustomerNameExistsAsync(dept.CustomerName))
            {
                ModelState.AddModelError(nameof(dept.CustomerName), "Customer name is already in use.");
            }
            if (ModelState.IsValid)
            {
                await _deptService.CreateDeptAsync(dept);
                return RedirectToAction(nameof(Index));
            }
            return View(dept);
        }

        // GET: Depts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dept = await _deptService.GetDeptByIdForEditAsync(id.Value);
            if (dept == null)
            {
                return NotFound();
            }
            return View(dept);
        }

        // POST: Depts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DeptsUpdateVM dept)
        {
            if (id != dept.Id)
            {
                return NotFound();
            }

            if (await _deptService.CustomerNameAlreadyInUseByAnotherAsync(id, dept.CustomerName))
            {
                ModelState.AddModelError(nameof(dept.CustomerName), "Customer name is already in use.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _deptService.UpdateDeptAsync(dept);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_deptService.DebtExists(dept.Id))
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
            return View(dept);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarkPaid(int id)
        {
            await _deptService.RemoveDeptAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
