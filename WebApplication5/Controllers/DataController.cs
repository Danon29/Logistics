using Logistics.DBContext;
using Logistics.Models.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logistics.Controllers
{
    [Authorize(Roles = "admin, user")]
    public class DataController : Controller
    {
        private LogisticsContext db;

        public int _idSender;
        public int _idRecipient;
        public DateTime now;
        public Price _price;

        public DataController(LogisticsContext context)
        {
            db = context;
        }

        
        public async Task<IActionResult> Index()
        {
            return View(await db.Orders.ToListAsync());
        }

        
        [HttpGet]
        public IActionResult AddOrderSender() 
        {
            return View();
        }

        
        [HttpPost] 
        public async Task<IActionResult> AddOrderSenderPost(Sender model) 
        {
            db.Senders.Add(model);
            
            await db.SaveChangesAsync();

            _idSender = model.SenderId;

            return RedirectToAction("AddOrderRecipientGet", new { _idSender });
        }

        
        [HttpGet]
        public IActionResult AddOrderRecipientGet()
        {
            Console.WriteLine(_idSender);
            return View();
        }

        
        [HttpPost]
        public async Task<IActionResult> AddOrderRecipient(Recipient model)
        {
            db.Recipients.Add(model);

            await db.SaveChangesAsync();

            _idRecipient = model.RecipientId;
            now = DateTime.Now;
            return RedirectToAction("CreateOrder", new {_idSender,_idRecipient, now});
        }

        
        public IActionResult CreateOrder() 
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
            return View();
        }


        
        [HttpPost]
        public IActionResult CreateOrderPost(Order model)
        {
            _price = db.Prices.FirstOrDefault(x => x.ProductId == model.ProductId);

            model.TotalCost = model.Quantity * _price.CostPerKg;

            model.Date = DateTime.Now;
           
            db.Add(model);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult TimeTable()
        {
            return View(db.FlightCities.ToList());
        }
    }
}
