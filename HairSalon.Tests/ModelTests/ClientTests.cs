using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon.Models;

namespace HairSalon.Tests
{
  [TestClass]
  public class HairSalonTests : IDisposable
  {
    public HairSalonTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=hyung_lee_test;";
    }

    public void Dispose()
    {
      Client.DeleteAll();
      Stylist.DeleteAll();
    }

    [TestMethod]
    public void Client_DBStartsEmpty_Empty()
    {
      //Arrange
      int count = Client.GetAll().Count;

      //Assert
      Assert.AreEqual(0, count);
    }

    [TestMethod]
    public void Save_SavedClientHasSameValues_True()
    {
      //Arrange
      Client clientOne = new Client("Ryan", 1, 1);
      clientOne.Save();
      int id = clientOne.GetId();

      //Act
      Client savedClient = Client.Find(id);

      //Assert
      Assert.AreEqual(clientOne, savedClient);
    }

    [TestMethod]
    public void Edit_EditedItemHasDifferentValue_True()
    {
      //Arrange
      Client clientOne = new Client("Ryan", 1, 1);
      clientOne.Save();
      Client editClient = new Client("Steve", 2, 1);

      //Act
      clientOne.Edit("Steve", 2);

      //Assert
      Assert.AreEqual(clientOne, editClient);
    }

    [TestMethod]
    public void Delete_ClientIsDeletedFromDB_True()
    {
      //Arrange
      Client clientOne = new Client("Ryan", 1, 1);
      clientOne.Save();
      int id = clientOne.GetId();
      Client defaultValues = new Client ("", 0);

      //Act
      clientOne.Delete(id);
      Client notFound = Client.Find(id);

      //Assert
      Assert.AreEqual(notFound, defaultValues);
    }
  }
}