using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;

namespace HairSalon.Controllers
{
  public class HomeController : Controller
  {
    [HttpGet("/")]
    public ActionResult Index(int activeId = 1)
    {
      string stylistsTab = "nav-link active";
      string stylistsDiv = "tab-pane fade show active";
      string clientsTab = "nav-link";
      string clientsDiv = "tab-pane fade";
      string specialitiesTab = "nav-link";
      string specialitiesDiv = "tab-pane fade";

      if (activeId == 2)
      {
        stylistsTab = "nav-link";
        stylistsDiv = "tab-pane fade";
        clientsTab = "nav-link active";
        clientsDiv = "tab-pane fade show active";
        specialitiesTab = "nav-link";
        specialitiesDiv = "tab-pane fade";
      }
      else if (activeId == 3)
      {
        stylistsTab = "nav-link";
        stylistsDiv = "tab-pane fade";
        clientsTab = "nav-link";
        clientsDiv = "tab-pane fade";
        specialitiesTab = "nav-link active";
        specialitiesDiv = "tab-pane fade show active";
      }

      List<Stylist> allStylists = Stylist.GetAll();
      List<Client> allClients = Client.GetAll();

      Dictionary<string, object> model = new Dictionary<string, object>{};
      model.Add("stylistList", allStylists);
      model.Add("clientList", allClients);

      model.Add("stylistsTab", stylistsTab);
      model.Add("stylistsDiv", stylistsDiv);
      model.Add("clientsTab", clientsTab);
      model.Add("clientsDiv", clientsDiv);
      model.Add("specialitiesTab", specialitiesTab);
      model.Add("specialitiesDiv", specialitiesDiv);

      return View(model);
    }
  }
}
