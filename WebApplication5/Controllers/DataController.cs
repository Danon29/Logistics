using Logistics.DBContext;
using Logistics.Models.Data;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Logistics.Controllers
{
    public class DataController : Controller
    {
        private LogisticsContext db;

        public List<Sender> _id = new List<Sender>();
 
        public DataController(LogisticsContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            return View(db.Orders.ToList());
        }

        [HttpGet]
        public IActionResult AddOrderSender() 
        {
            return View();
        }

        [HttpPost] 
        public IActionResult AddOrderSenderPost(Sender model) 
        {
            db.Senders.Add(model);
            db.SaveChanges();
            _id[0] = db.Senders.FirstOrDefault(x => (x.Name == model.Name) && (x.Surname == model.Surname));
            return RedirectToAction("AddOrderRecipientGet");
        }

        [HttpGet]
        public IActionResult AddOrderRecipientGet()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddOrderRecipient(Recipient model)
        {
            db.Recipients.Add(model);
            db.SaveChanges();
            _id[1] = db.Senders.FirstOrDefault(x => (x.Name == model.Name) && (x.Surname == model.Surname));
            return RedirectToAction("Index");
        }
    }
}
