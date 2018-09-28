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
      List<Speciality> allSpecialities = Speciality.GetAll();
      Dictionary<string, object> model = new Dictionary<string, object>() {};
      model.Add("stylist", foundStylist);
      model.Add("list", allStylists);
      model.Add("specialityList", allSpecialities);
      return View(model);
    }

    //Find stylist by name
    [HttpGet("stylists/find-by-name")]
    public ActionResult SearchByName(string stylistName)
    {
      Stylist foundStylist = Stylist.FindByName(stylistName);
      int id = foundStylist.GetId();
      return RedirectToAction("Details", new { id = id });
    }

    //Edit details for specific stylist from homepage.
    [HttpPost("/home/stylists/{id}/edit")]
    public ActionResult EditDetails(int id, string stylistName)
    {
      Stylist editStylist = Stylist.Find(id);
      editStylist.Edit(stylistName);
      return RedirectToAction("Index", "Home");
    }

    //Deletes all stylists from database
    [HttpGet("/home/delete/stylists")]
    public ActionResult DeleteAll()
    {
      Stylist.DeleteAll();
      return RedirectToAction("Index", "Home");
    }

    //Add a speciality to stylist from stylist details page.
    [HttpPost("/stylists/{stylistId}/speciality/add")]
    public ActionResult AddSpecialityFromDetails(int stylistId, int specialityId)
    {
      Stylist foundStylist = Stylist.Find(stylistId);
      Speciality foundSpeciality = Speciality.Find(specialityId);
      if (!foundSpeciality.CheckIfExists(foundStylist.GetId()))
      {
        foundStylist.AddSpeciality(foundSpeciality);
      }
      return RedirectToAction("Details", new { id = stylistId });
    }
  }
}