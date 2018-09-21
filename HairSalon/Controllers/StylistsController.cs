using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;

namespace HairSalon.Controllers
{
  public class StylistsController : Controller
  {
    [HttpGet("/home/stylists/{id}/delete")]
    public ActionResult Delete(int id)
    {
      Stylist.Delete(id);
      return RedirectToAction("Index", "Home");
    }

    [HttpPost("/home/stylists/client/add")]
    public ActionResult AddClient(int stylistId, string clientName)
    {
      Client newClient = new Client(clientName, stylistId);
      newClient.Save();
      return RedirectToAction("Index", "Home");
    }

    [HttpPost("/home/stylists/add")]
    public ActionResult AddClient(string stylistName)
    {
      Stylist newStylist = new Stylist(stylistName);
      newStylist.Save();
      return RedirectToAction("Index", "Home");
    }

    [HttpGet("/home/stylists/{id}/add")]
    public ActionResult Add(int id)
    {
      Stylist newStylist = Stylist.Find(id);
      return PartialView("AddModal", newStylist);
    }
  }
}