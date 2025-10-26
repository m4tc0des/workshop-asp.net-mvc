using SalesWebMvc.Controllers;
using SalesWebMvc.Models.Entities;
using SalesWebMvc.Services;

namespace SalesWebMvc.Models.ViewModels
{
    public class SellerFormViewModel
    {
        public Seller Seller { get; set; }
        public ICollection<Department> Departments { get; set; }
        
    }
}
