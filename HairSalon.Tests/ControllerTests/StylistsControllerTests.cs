using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using HairSalon.Controllers;
using HairSalon.Models;

namespace HairSalon.Tests
{
  [TestClass]
  public class StylistsControllerTest
  {
    [TestMethod]
    public void Details_ReturnsCorrectView_True()
    {
      //Arrange
      Stylist newStylist = new Stylist("Ryan");
      newStylist.Save();
      Client newClient = new Client("Ryan", 1);
      newClient.Save();
      StylistsController controller = new StylistsController();

      //Act
      ActionResult detailsView = controller.Details(1);

      //Assert
      Assert.IsInstanceOfType(detailsView, typeof(ViewResult));
    }

    [TestMethod]
    public void Details_HasCorrectModelType_ItemList()
    {
      //Arrange
      Stylist newStylist = new Stylist("Ryan");
      newStylist.Save();
      Client newClient = new Client("Ryan", 1);
      newClient.Save();
      StylistsController controller = new StylistsController();
      ViewResult detailsView = controller.Details(1) as ViewResult;

      //Act
      var result = detailsView.ViewData.Model;

      //Assert
      Assert.IsInstanceOfType(result, typeof(Dictionary<string, object>));
    }
  }
}