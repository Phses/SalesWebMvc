using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data;
using SalesWebMvc.Models;
using SalesWebMvc.Services.Exceptions;

namespace SalesWebMvc.Services
{
    public class SellerService
    {
        private readonly SalesWebMvcContext _context;

        public SellerService(SalesWebMvcContext context)
        {
            _context = context;
        }

        public async Task<List<Seller>> FindAllAsync()
        {
            return await _context.Sellers.ToListAsync();
        }

        public async Task InsertAsync(Seller obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<Seller> FindByIdAsync(int Id)
        {
            return await _context.Sellers.Include(s => s.Department).FirstOrDefaultAsync(s => s.Id == Id);
        }

        public async Task DeleteAsync(int Id)
        {
           var obj = await _context.Sellers.FindAsync(Id);
           _context.Sellers.Remove(obj);
           await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Seller obj)
        {
            bool hasAny = await _context.Sellers.AnyAsync(s => s.Id == obj.Id);
            if (!hasAny)
            {
                throw new NotFoundException("Não há nenhum seller com o id informado");
            }
            try
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();
            } catch(DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
