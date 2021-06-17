using Autobooks.Data.Attributes;

namespace Autobooks.Data.Models
{
    [Table("Customers")]
    public class Customer : BaseEntity
    {
        public string Name { get; set; }
    }
}
