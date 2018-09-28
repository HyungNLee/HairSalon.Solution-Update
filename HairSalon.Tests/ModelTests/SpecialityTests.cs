using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon.Models;

namespace HairSalon.Tests
{
  [TestClass]
  public class SpecialityTests : IDisposable
  {
    public SpecialityTests()
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
    public void Speciality_DBstartsEmpty_Empty()
    {
      int count = Speciality.GetAll().Count;

      Assert.AreEqual(0,count);
    }

    [TestMethod]
    public void Speciality_HasSameValues_True()
    {
      //Arrange
      Speciality One = new Speciality("Braiding");
      Speciality Two = new Speciality("Braiding");

      //Assert
      Assert.AreEqual(One, Two);
    }

    [TestMethod]
    public void Speciality_FindMatchingSpeciality_True()
    {
      //Arrange
      Speciality specialityOne = new Speciality("Cutting");
      specialityOne.Save();
      int id = specialityOne.GetId();

      //Act
      Speciality foundSpeciality = Speciality.Find(id);

      //Assert
      Assert.AreEqual(specialityOne, foundSpeciality);
    }

    [TestMethod]
    public void Speciality_SpecialityDeletedFromDB_True()
    {
      //Arrange
      Speciality specialityOne = new Speciality("Cutting");
      specialityOne.Save();
      int id = specialityOne.GetId();
      Speciality defaultSpeciality = new Speciality("", 0);

      //Act
      Speciality.Delete(id);
      Speciality notFound = Speciality.Find(id);

      //Assert
      Assert.AreEqual(notFound, defaultSpeciality);
    }
    [TestMethod]
    public void Speciality_EditedSpecialityHasDifferentValue_True()
    {
      //Arrange
      Speciality specialityOne = new Speciality ("Cutting");
      Speciality specialityTwo = new Speciality ("Braiding");
      specialityOne.Save();
      int id = specialityOne.GetId();

      //Act
      specialityOne.Edit("Braiding");
      Speciality foundSpeciality = Speciality.Find(id);

      //Assert
      Assert.AreEqual(specialityTwo, foundSpeciality);
    }
  }
}