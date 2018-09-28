using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon.Models;

namespace HairSalon.Tests
{
  [TestClass]
  public class StylistTests : IDisposable
  {
    public StylistTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=hyung_lee_test;";
    }

    public void Dispose()
    {
      Client.DeleteAll();
      Stylist.DeleteAll();
      Speciality.DeleteAll();
    }

    [TestMethod]
    public void Stylist_DBstartsEmpty_Empty()
    {
      int count = Stylist.GetAll().Count;

      Assert.AreEqual(0,count);
    }

    [TestMethod]
    public void Stylist_HasSameValues_True()
    {
      //Arrange
      Stylist stylistOne = new Stylist("AJ");
      Stylist stylistTwo = new Stylist("AJ");

      //Assert
      Assert.AreEqual(stylistOne, stylistTwo);
    }

    [TestMethod]
    public void Stylist_FindMatchingStylist_True()
    {
      //Arrange
      Stylist stylistOne = new Stylist("Hyewon");
      stylistOne.Save();
      int id = stylistOne.GetId();

      //Act
      Stylist foundStylist = Stylist.Find(id);

      //Assert
      Assert.AreEqual(stylistOne, foundStylist);
    }

    [TestMethod]
    public void Stylist_StylistDeletedFromDB_True()
    {
      //Arrange
      Stylist stylistOne = new Stylist("Chan");
      stylistOne.Save();
      int id = stylistOne.GetId();
      Stylist defaultStylist = new Stylist("", 0);

      //Act
      Stylist.Delete(id);
      Stylist notFound = Stylist.Find(id);

      //Assert
      Assert.AreEqual(notFound, defaultStylist);
    }
    [TestMethod]
    public void Stylist_EditedStylistHasDifferentValue_True()
    {
      //Arrange
      Stylist stylistOne = new Stylist ("Meria");
      Stylist stylistTwo = new Stylist ("Skye");
      stylistOne.Save();
      int id = stylistOne.GetId();

      //Act
      stylistOne.Edit("Skye");
      Stylist foundStylist = Stylist.Find(id);

      //Assert
      Assert.AreEqual(stylistTwo, foundStylist);
    }
  }
}