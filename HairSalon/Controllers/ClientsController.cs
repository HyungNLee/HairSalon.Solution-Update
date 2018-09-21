using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;

namespace HairSalon.Controllers
{
  public class ClientsController : Controller
  {
    //Deletes a client from stylists details page.
    [HttpGet("stylists/{id}/details/{clientId}/delete")]
    public ActionResult Delete(int id, int clientId)
    {
      Client.Delete(clientId);
      return RedirectToAction("Details", "Stylists", new {id = id});
    }

    //Adds a client from modal in homepage.
    [HttpPost("/home/stylists/client/add")]
    public ActionResult AddClient(int stylistId, string clientName)
    {
      Client newClient = new Client(clientName, stylistId);
      newClient.Save();
      return RedirectToAction("Index", "Home");
    }

    //Adds client to a stylist from stylists details page.
    [HttpPost("/stylists/{stylistId}/client/add")]
    public ActionResult AddClientFromDetails(int stylistId, string clientName)
    {
      Client newClient = new Client(clientName, stylistId);
      newClient.Save();
      return RedirectToAction("Details", "Stylists", new { id = stylistId });
    }

    //Deletes a specific client from stylists details page.
    [HttpGet("/stylists/{stylistId}/clients/{clientId}/delete")]
    public ActionResult DeleteClientFromDetails(int stylistId, int clientId)
    {
      Client.Delete(clientId);
      return RedirectToAction("Details", "Stylists", new { id = stylistId });
    }
  }
}