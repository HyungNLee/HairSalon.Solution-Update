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
      Stylist foundStylist = Stylist.Find(id);
      return View(foundStylist);
    }
  }
}