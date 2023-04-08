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

        public List<Seller> FindAll()
        {
            return _context.Sellers.ToList();
        }

        public void Insert(Seller obj)
        {
            _context.Add(obj);
            _context.SaveChanges();
        }

        public Seller FindById(int Id)
        {
            return _context.Sellers.Include(s => s.Department).FirstOrDefault(s => s.Id == Id);
        }

        public void Delete(int Id)
        {
           var obj = _context.Sellers.Find(Id);
           _context.Sellers.Remove(obj);
           _context.SaveChanges();
        }

        public void Update(Seller obj)
        {
            if (!_context.Sellers.Any(s => s.Id == obj.Id))
            {
                throw new NotFoundException("Não há nenhum seller com o id informado");
            }
            try
            {
                _context.Update(obj);
                _context.SaveChanges();
            } catch(DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
