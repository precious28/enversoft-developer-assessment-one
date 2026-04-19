namespace SupplierApp.API.Models
{
    public class Supplier
    {
        public int Id { get; set; }
        public int Code { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Telephone { get; set; } = string.Empty;
    }
}
