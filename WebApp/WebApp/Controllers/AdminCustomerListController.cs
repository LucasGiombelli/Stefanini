using System;
using System.Collections.Generic;
using System.Web.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class AdminCustomerListController : CustomerListController
    {
        protected override DummySession CreateDummySession(string userID)
        {
            return new DummySession() { UserID = userID };
        }

        protected override DummySession UpdateFilters(DummySession dummySession)
        {
            var selectedClassification = Request.Form["selectedClassification"];
            var selectedSeller = Request.Form["selectedSeller"];
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
                !string.IsNullOrEmpty(selectedSeller) ||
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
                    FilterSeller = selectedSeller,
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

        protected override ActionResult AppliedFilters(IEnumerable<Client> clients)
        {
            Func<IEnumerable<Client>, IEnumerable<CustomerWithSellerViewModel>> CreateCustomerListViewModel = (cs) =>
            {
                var customers = new List<CustomerWithSellerViewModel>();

                foreach (var client in cs)
                    customers.Add(new CustomerWithSellerViewModel(client));

                return customers;
            };

            return View("AdminCustomerList", CreateCustomerListViewModel(clients));
        }
    }
}