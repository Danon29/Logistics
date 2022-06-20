using Logistics.DBContext;
using Logistics.Models.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Logistics.Controllers
{
   // [Authorize(Roles = "admin")]
    public class DataAdminController : Controller
    {
        private LogisticsContext db;

        public DataAdminController(LogisticsContext context)
        {
            db = context;
        }

        
        public IActionResult AdminPage()
        {
            //string role = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultRoleClaimType).Value;
            return View();
        }


        [HttpGet]
        public IActionResult CreateFlight() 
        {
            SelectList Drivers = new SelectList(db.Drivers, "DriverId", "Surname");
            SelectList Cars = new SelectList(db.Cars, "CarId", "CarNumber");
            ViewBag.Drivers = Drivers;  
            ViewBag.Cars = Cars;
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> CreateFlightPost(Flight model) 
        {
            db.Flights.Add(model);
            await db.SaveChangesAsync();
            return RedirectToAction("AdminPage");  
        } 


        [HttpGet]
        public IActionResult CreateOrderFlight() 
        {
            SelectList Orders = new SelectList(db.Orders, "ContractNumber", "ContractNumber");
            SelectList Flights = new SelectList(db.Flights, "FlightId", "FlightId");
            ViewBag.Orders = Orders;
            ViewBag.Flights = Flights;

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> CreateOrderFlightPost(FlightOrder model) 
        {
            
            db.FlightOrders.Add(model);
            await db.SaveChangesAsync();
            return RedirectToAction("AdminPage");
        }


        [HttpGet]
        public IActionResult CreateFlightCity()
        {
            SelectList Cities = new SelectList(db.CitiesTos, "CitieToId", "Name");
            SelectList Flights = new SelectList(db.Flights, "FlightId", "FlightId");
            ViewBag.Flights = Flights;
            ViewBag.City = Cities;

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> CreateFlightCityPost(FlightCity model)
        {
            db.FlightCities.Add(model);
            await db.SaveChangesAsync();
            return RedirectToAction("AdminPage");
        }

    }

}
