using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wackypedia.Models;
using System.Collections.Generic;
using System;

namespace Wackypedia.Tests
{
    [TestClass]
    public class SectionTest
    {
        [TestMethod]
        public void GetTitle_ReturnsTitle_String()
        {
            //Arrange
            Section section = new Section("Wackypedia", "Any Content");

            //Act
            var newTitle = section.GetTitle();

            //Assert
            Assert.IsInstanceOfType(newTitle, typeof(string));
        }

        //[TestMethod]
        //public void GetID_ReturnsExpectedID()
        //{
        //    //Arrange
        //    Section section = new Section("Wackypedia", "image", "Welcome to Wackypedia", 3);

        //    //Act
        //    int id = section.GetID();

        //    //Assert
        //    Assert.AreEqual(3, id);
        //}

        //[TestMethod]
        //public void GetAll_ReturnsAllSectionsInTheDatabase()
        //{
        //    //Arrange
        //    Section anySection = new Section("Wackypedia", "Welcome to Wackypedia");

        //    //Act
        //    anySection.Save();

        //    //Act
        //    List<Section> allSections = Section.GetAll();

        //    //Assert
        //    Assert.IsTrue(allSections.Count >= 1);
        //}

        //[TestMethod]
        //public void Save_SavesToDatabase_SectionList()
        //{
        //    //Arrange
        //    Section newSection = new Section("Wackypedia", "image", "Welcome to Wackypedia", 2);

        //    //Act
        //    newSection.Save();
        //    List<Section> allSections = Section.GetAll();

        //    bool sectionFound = false;
        //    foreach (Section actualSection in allSections)
        //    {
        //        if (actualSection.GetTitle() == newSection.GetTitle())
        //        {
        //            sectionFound = true;
        //            break;
        //        }
        //    }

        //    //Assert
        //    Assert.IsTrue(sectionFound);
        //}

        //[TestMethod]
        //public void Find_ReturnsSectionWithTheMatchingId()
        //{
        //    //Arrange
        //    string sectionTitle = "Section3";
        //    int sectionId = 1;
        //    Section expectedSection = new Section(sectionTitle, "image", "Welcome to Wackypedia", sectionId);

        //    //Act
        //    expectedSection.Save();
        //    Section actualSection = Section.Find(expectedSection.GetID());

        //    // Assert
        //    Assert.IsNotNull(actualSection);
        //    Assert.IsTrue(expectedSection.GetID() == actualSection.GetID());
        //    Assert.IsTrue(expectedSection.GetTitle() == actualSection.GetTitle());
        //}

        //[TestMethod]
        //public void Delete_RemovesTheSectionFromTheDatabase()
        //{
        //    //Arrange
        //    string sectionTitle = "Section4";
        //    int SectionId = 1;
        //    Section expectedSection = new Section(sectionTitle, "image", "Welcome to Wackypedia", 1);

        //    //Act
        //    expectedSection.Save();
        //    expectedSection.Delete(expectedSection.GetID());
        //    Section deletedSection = Section.Find(expectedSection.GetID());

        //    // Assert
        //    Assert.IsNull(deletedSection);
        //}
    }
}
