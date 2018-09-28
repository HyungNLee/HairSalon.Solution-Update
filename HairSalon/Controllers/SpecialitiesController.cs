using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;

namespace HairSalon.Controllers
{
  public class SpecialitiesController : Controller
  {
    //Deletes a stylist from database from homepage.
    [HttpGet("/home/specialities/{id}/delete")]
    public ActionResult Delete(int id)
    {
      Speciality.Delete(id);
      return RedirectToAction("Index", "Home", new { activeId = 3 });
    }

    //Adds a speciality to database from homepage.
    [HttpPost("/home/specialities/add")]
    public ActionResult AddClient(string specialityName)
    {
      Speciality newSpeciality = new Speciality(specialityName);
      newSpeciality.Save();
      return RedirectToAction("Index", "Home", new { activeId = 3 });
    }

    //Click to get details page for one speciality.
    [HttpGet("/specialities/{id}/details")]
    public ActionResult Details(int id)
    {
      List<Stylist> allStylists = Stylist.GetAll();
      Speciality foundSpeciality = Speciality.Find(id);
      List<Stylist> currentStylists = foundSpeciality.GetStylists();
      Dictionary<string, object> model = new Dictionary<string, object>() {};
      model.Add("speciality", foundSpeciality);
      model.Add("stylistList", allStylists);
      model.Add("currentList", currentStylists);
      return View(model);
    }

    //Edit details for specific speciality from homepage.
    [HttpPost("/home/specialities/{id}/edit")]
    public ActionResult EditDetails(int id, string specialityName)
    {
      Speciality editSpeciality = Speciality.Find(id);
      editSpeciality.Edit(specialityName);
      return RedirectToAction("Index", "Home", new { activeId = 3 });
    }

    //Deletes all specialities from database
    [HttpGet("home/delete/specialities")]
    public ActionResult DeleteAll()
    {
      Speciality.DeleteAll();
      return RedirectToAction("Index", "Home", new { activeId = 3 });
    }

    //Deletes a stylist from database from homepage.
    [HttpGet("/stylists/{stylistId}/specialities/{id}/delete")]
    public ActionResult DeleteFromStylistDetails(int id, int stylistId)
    {
      Speciality.Delete(id);
      return RedirectToAction("Details", "Stylists", new { id = stylistId });
    }

    //Adds stylist to speciality from speciality details page
    [HttpPost("/specialities/{specialityId}/stylist/add")]
    public ActionResult AddStylistFromSpecialityDetails(int specialityId, int stylistId)
    {
      Stylist foundStylist = Stylist.Find(stylistId);
      Speciality foundSpeciality = Speciality.Find(specialityId);
      if (!foundSpeciality.CheckIfExists(foundStylist.GetId()))
      {
        foundStylist.AddSpeciality(foundSpeciality);
      }
      return RedirectToAction("Details", new { id = specialityId });
    }
  }
}