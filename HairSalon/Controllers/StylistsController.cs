using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;

namespace HairSalon.Controllers
{
  public class StylistsController : Controller
  {
    //Deletes a stylist from database from homepage.
    [HttpGet("/home/stylists/{id}/delete")]
    public ActionResult Delete(int id)
    {
      Stylist.Delete(id);
      return RedirectToAction("Index", "Home");
    }

    //Adds a stylist to database from homepage.
    [HttpPost("/home/stylists/add")]
    public ActionResult AddClient(string stylistName)
    {
      Stylist newStylist = new Stylist(stylistName);
      newStylist.Save();
      return RedirectToAction("Index", "Home");
    }

    //Click to get details page for one stylist.
    [HttpGet("/stylists/{id}/details")]
    public ActionResult Details(int id)
    {
      List<Stylist> allStylists = Stylist.GetAll();
      Stylist foundStylist = Stylist.Find(id);
      Dictionary<string, object> model = new Dictionary<string, object>() {};
      model.Add("stylist", foundStylist);
      model.Add("list", allStylists);
      return View(model);
    }

    //Edit details for specific stylist from homepage.
    [HttpPost("/home/stylists/{id}/edit")]
    public ActionResult EditDetails(int id, string stylistName)
    {
      Stylist editStylist = Stylist.Find(id);
      editStylist.Edit(stylistName);
      return RedirectToAction("Index", "Home");
    }
  }
}