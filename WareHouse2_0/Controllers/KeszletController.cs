using Microsoft.AspNetCore.Mvc;
using API.Data;
using API.Entities;
using Microsoft.EntityFrameworkCore;


namespace WareHouse2._0.Controllers
{
    public class KeszletController : Controller
    {
        private readonly DataContext _context;

        public KeszletController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<ViewModelKeszlet> inventoryList = GetInventoryList();
            return View(inventoryList);
        }

        public IActionResult Create()
        {
            return View();
        }
        public List<ViewModelKeszlet> GetInventoryList()
        {
            const string sql = @"
                                SELECT 
                                    a.Name AS TermekNev,
                                    a.Description AS TermekLeiras,
                                    a.Price AS TermekAra,
                                    k.Quantity AS TermekMennyisege,
                                    t.Address AS TelephelyCime
                                FROM 
                                    Keszlet k
                                INNER JOIN 
                                    Aruk a ON k.ProductId = a.Id
                                INNER JOIN 
                                    Telephelyek t ON k.WarehouseId = t.Id
                                ORDER BY 
                                    a.Name;";

            // SQL lekérdezés futtatása a DbContext-en keresztül
            var inventoryList = _context.ViewModelKeszlet.FromSqlRaw(sql).ToList();

            return inventoryList;

        }
    }
}
