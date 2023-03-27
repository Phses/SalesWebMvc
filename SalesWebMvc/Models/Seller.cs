namespace SalesWebMvc.Models
{
    public class Seller
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public double BaseSalary { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        public List<SalesRecord> SalesRecords { get; set; }

        public void addSales(SalesRecord sale)
        {
            SalesRecords.Add(sale);
        }

        public void removeSales(SalesRecord sale)
        {
            SalesRecords.Remove(sale);
        }

        public double totalSales(DateTime initial, DateTime final)
        {
            return SalesRecords.Where(sr => sr.Date >= initial && sr.Date <= final).Sum(sr => sr.Amount);
        }
    }
}
