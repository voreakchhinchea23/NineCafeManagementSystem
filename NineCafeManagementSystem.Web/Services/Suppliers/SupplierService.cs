
using Microsoft.AspNetCore.Identity;

namespace NineCafeManagementSystem.Web.Services.Suppliers
{
    public class SupplierService(ApplicationDbContext _context) : ISupplierService
    {
        public async Task CreateSupplierAsync(SupplierCreateVM model)
        {
            var entity = new Supplier
            {
                Name = model.Name,
                ContactInfo = model.ContactInfo,
                Address = model.Address,
                ProductType = model.ProductType
            };

            _context.Suppliers.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task EditSupplierAsync(SupplierUpdateVM model)
        {
            await _context.Suppliers
                .Where(q => q.Id == model.Id)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(e => e.Name , model.Name)
                    .SetProperty(e => e.Address, model.Address)
                    .SetProperty(e => e.ContactInfo, model.ContactInfo)
                    .SetProperty(e => e.ProductType, model.ProductType)
                );
        }

        public async Task<List<SupplierReadOnlyVM>> GetAllSuppliersAsync()
        {
            return await _context.Suppliers
                .Select(q => new SupplierReadOnlyVM
                {
                    Id = q.Id,
                    Name = q.Name,
                    ContactInfo = q.ContactInfo,
                    Address = q.Address,
                    ProductType = q.ProductType,
                }).ToListAsync();
        }

        public async Task<SupplierReadOnlyVM?> GetSupplierByIdAsync(int id)
        {
            return await _context.Suppliers
                .Where(q => q.Id == id)
                .Select(q => new SupplierReadOnlyVM
                {
                    Id = q.Id,
                    Name = q.Name,
                    ContactInfo = q.ContactInfo,
                    Address = q.Address,
                    ProductType = q.ProductType
                }).FirstOrDefaultAsync();
        }

        public async Task<SupplierUpdateVM?> GetSupplierByIdForEditAsync(int id)
        {
            return await _context.Suppliers
                .Where(q => q.Id == id)
                .Select(q => new SupplierUpdateVM
                {
                    Id = q.Id,
                    Name = q.Name,
                    ContactInfo = q.ContactInfo,
                    Address = q.Address,
                    ProductType = q.ProductType,
                }).FirstOrDefaultAsync();
        }

        public async Task<bool> PhoneNumberExistsAsync(string contact)
        {
            return await _context.Suppliers.AnyAsync(q => q.ContactInfo ==  contact);
        }
        public async Task<bool> IsNumberAlreadyUsedByAnotherSupplierAsync(int supplierId, string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                return false;
            }

            var normalized = phoneNumber.Trim();
            return await _context.Suppliers
                .AnyAsync(q => q.Id != supplierId
                && !string.IsNullOrEmpty(q.ContactInfo)
                && q.ContactInfo.Trim() == normalized
            );
        }

        public async Task RemoveSupplierAysnc(int id)
        {
            var entity = await _context.Suppliers.FindAsync(id);
            if (entity != null)
            {
                _context.Suppliers.Remove(entity);
                await _context.SaveChangesAsync();
            } 
        }

        public bool SupplierExists(int id)
        {
            return _context.Suppliers.Any(p => p.Id == id);
        }
    }
}
