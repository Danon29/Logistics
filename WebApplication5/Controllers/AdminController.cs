using Logistics.DBContext;
using Logistics.Models.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Logistics.Controllers
{
    public class AdminController : Controller
    {
        private LogisticsContext db;
        public Price _price;
        public AdminController(LogisticsContext context)
        {
            db = context;
        }

        //Выводит список заказов
        public async Task<IActionResult> Index()
        {
            return View(await db.Orders.ToListAsync());
        }

        //Возвращает представление для метода Edit
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            SelectList CitiesFrom = new SelectList(db.CitiesFroms, "CityFromId", "Name");
            SelectList CitiesTo = new SelectList(db.CitiesTos, "CitieToId", "Name");
            SelectList Products = new SelectList(db.Prices, "ProductId", "Name");
            SelectList Senders = new SelectList(db.Senders, "SenderId", "Surname");
            SelectList Recipients = new SelectList(db.Recipients, "RecipientId", "Surname");
            ViewBag.CitiesFrom = CitiesFrom;
            ViewBag.CitiesTo = CitiesTo;
            ViewBag.Products = Products;
            ViewBag.Senders = Senders;
            ViewBag.Recipients = Recipients;

            if (id != null)
            {
                Order order = await db.Orders.FirstOrDefaultAsync(p => p.ContractNumber == id);
                if (order != null)
                {   
                    return View(order);
                } 
            }

            return NotFound();
        }

        //Редактирует Заказ
        [HttpPost]
        public IActionResult EditPost(Order model)
        {
            if (ModelState.IsValid)
            {
                _price = db.Prices.FirstOrDefault(x => x.ProductId == model.ProductId);
                model.TotalCost = model.Quantity * _price.CostPerKg;

                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Edit");
        }


        //Удаляет Заказ
        public async Task<IActionResult> Delete(int id)
        {
             Order order = await db.Orders
                 .Where(x=>x.ContractNumber == id).FirstOrDefaultAsync();

             db.Orders.Remove(order);

             await db.SaveChangesAsync();

             return RedirectToAction("Index");
        }

        //Выводит все записи из таблицы FlightOrder
        public async Task<IActionResult> Index2()
        {
            return View(await db.FlightOrders.ToListAsync());
        }


        //Удаляет записи в таблице FlightOrder
        public async Task<IActionResult> DeleteIndex2(int id)
        {

            FlightOrder flightOrder = await db.FlightOrders
                .Where(p=>p.FirstId == id).FirstOrDefaultAsync();

            db.FlightOrders.Remove(flightOrder);

            await db.SaveChangesAsync();

            return RedirectToAction("Index2");
        }


        public IActionResult TimeTable() 
        {
            return View(db.FlightCities.ToList());
        }
    }
}
