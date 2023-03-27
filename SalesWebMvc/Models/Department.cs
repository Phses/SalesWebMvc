namespace SalesWebMvc.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Seller> Sellers { get; set; } = new List<Seller>();

        public void addSeller(Seller seller)
        {
            Sellers.Add(seller);
        }

        public double totalSales(DateTime initial, DateTime final)
        {
            return Sellers.Sum(s => s.totalSales(initial, final));
        }
    }
}
