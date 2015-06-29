using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class CustomerListController : Controller
    {
        public ActionResult Index()
        {
            var dummySession = (DummySession)TempData["DummySession"];
            if (dummySession == null || dummySession.UserID == null)
                return RedirectToAction("Login", "Home");

            TempData["DummySession"] = CreateDummySession(dummySession.UserID);

            TempData.Keep("DummySession");

            return ApplyFilters();
        }

        protected virtual DummySession CreateDummySession(string userID)
        {
            return new DummySession()
            {
                UserID = userID,
                FilterSeller = userID
            };
        }

        public ActionResult ApplyFilters()
        {
            var dummySession = (DummySession)TempData["DummySession"];
            if (dummySession == null || dummySession.UserID == null)
                return RedirectToAction("Login", "Home");

            dummySession = UpdateFilters(dummySession);

            TempData["DummySession"] = dummySession;

            var context = new WebAppContext();
            var customers = context.GetCustomers();

            if (!string.IsNullOrEmpty(dummySession.FilterName))
                customers = customers.Where(c => c.Name.IndexOf(dummySession.FilterName) >= 0);

            if (!string.IsNullOrEmpty(dummySession.FilterCity))
                customers = customers.Where(c => c.Region.CityID.ToString() == dummySession.FilterCity);

            if (!string.IsNullOrEmpty(dummySession.FilterRegion))
                customers = customers.Where(c => c.Region.ID.ToString() == dummySession.FilterRegion);

            if (!string.IsNullOrEmpty(dummySession.FilterClassification))
                customers = customers.Where(c => c.ClassificationID.ToString() == dummySession.FilterClassification);

            if (!string.IsNullOrEmpty(dummySession.FilterSeller))
                customers = customers.Where(c => c.SellerID.ToString() == dummySession.FilterSeller);

            if (!string.IsNullOrEmpty(dummySession.FilterGender))
                customers = customers.Where(c => c.Gender == dummySession.FilterGender);

            if (dummySession.FilterDateMin > 0)
                customers = customers.Where(c => int.Parse(c.LastPurchase.ToString("yyyyMMdd")) >= dummySession.FilterDateMin);

            if (dummySession.FilterDateMax > 0)
                customers = customers.Where(c => int.Parse(c.LastPurchase.ToString("yyyyMMdd")) <= dummySession.FilterDateMax);

            TempData.Keep("DummySession");

            ViewBag.ViewModel = new FiltersViewModel(context);
            return AppliedFilters(customers);
        }

        protected virtual DummySession UpdateFilters(DummySession dummySession)
        {
            var selectedClassification = Request.Form["selectedClassification"];
            var selectedRegion = Request.Form["selectedRegion"];
            var selectedCity = Request.Form["selectedCity"];
            var selectedGender = Request.Form["selectedGender"];
            var dateMin = Request.Form["dateMin"];
            var dateMax = Request.Form["dateMax"];
            var name = Request.Form["name"];

            if (!string.IsNullOrEmpty(name) ||
                !string.IsNullOrEmpty(selectedCity) ||
                !string.IsNullOrEmpty(selectedRegion) ||
                !string.IsNullOrEmpty(selectedClassification) ||
                !string.IsNullOrEmpty(dateMin) ||
                !string.IsNullOrEmpty(dateMax) ||
                !string.IsNullOrEmpty(selectedGender))
            {
                dummySession = new DummySession()
                {
                    UserID = dummySession.UserID,
                    FilterName = name,
                    FilterCity = selectedCity,
                    FilterRegion = selectedRegion,
                    FilterClassification = selectedClassification,
                    FilterSeller = dummySession.UserID,
                    FilterGender = selectedGender
                };

                DateTime date;
                if (!string.IsNullOrEmpty(dateMin) && DateTime.TryParse(dateMin, out date))
                    dummySession.FilterDateMin = int.Parse(date.ToString("yyyyMMdd"));

                if (!string.IsNullOrEmpty(dateMax) && DateTime.TryParse(dateMax, out date))
                    dummySession.FilterDateMax = int.Parse(date.ToString("yyyyMMdd"));
            }

            return dummySession;
        }

        protected virtual ActionResult AppliedFilters(IEnumerable<Client> clients)
        {
            Func<IEnumerable<Client>, IEnumerable<CustomerViewModel>> CreateCustomerListViewModel = (cs) =>
                {
                    var customers = new List<CustomerViewModel>();

                    foreach (var client in cs)
                        customers.Add(new CustomerViewModel(client));

                    return customers;
                };

            return View("CustomerList", CreateCustomerListViewModel(clients));
        }

        public JsonResult GetRegions(string id)
        {
            var context = new WebAppContext();
            var filteredRegions = context.Regions.Where(r=>r.CityID.ToString() == id); 

            var regions = new List<SelectListItem>();

            regions.Add(new SelectListItem() { Value = "", Text = "" });
            foreach (var region in filteredRegions)
                regions.Add(new SelectListItem() { Value = region.ID.ToString(), Text = region.Name });

            return Json(new SelectList(regions, "Value", "Text"));
        }
    }
}