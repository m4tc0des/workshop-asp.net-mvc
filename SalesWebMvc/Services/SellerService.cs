using SalesWebMvc.Models;
using SalesWebMvc.Data;
using Microsoft.Identity.Client;
using System.Collections.Generic;
using SalesWebMvc.Models.Entities;
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
            return _context.Seller.ToList();
        }
    }
}
