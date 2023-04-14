using System;
using NUnit.Framework;
using DxMood.Services;
using IO.Swagger.Models;
using DxMood.Services.Interfaces;

namespace DxMoodTests
{
    [TestFixture]
    public class DxMoodServiceTests
    {
        private DxMoodService _service;

        [SetUp]
        public void Setup()
        {
            _service = new DxMoodService();
        }

        [Test]
        public void TestGetDiagnosis_NoPsychiatricDx()
        {
            // Arrange
            int phq9 = 5;
            int gad7 = 5;
            int isi = 5;
            int asrs = 5;

            // Act
            Result result = _service.GetDiagnosis(phq9, gad7, isi, asrs);

            // Assert
            Assert.AreEqual("No Psychiatric Dx", result.Diagnosis);
            Assert.AreEqual("None", result.RecommendedMedication);
        }

        [Test]
        public void TestGetDiagnosis_MDD()
        {
            // Arrange
            int phq9 = 15;
            int gad7 = 5;
            int isi = 5;
            int asrs = 5;

            // Act
            Result result = _service.GetDiagnosis(phq9, gad7, isi, asrs);

            // Assert
            Assert.AreEqual("MDD", result.Diagnosis);
            Assert.AreEqual("SSRI or SNRI", result.RecommendedMedication);
        }

        [Test]
        public void TestGetDiagnosis_GAD()
        {
            // Arrange
            int phq9 = 5;
            int gad7 = 15;
            int isi = 5;
            int asrs = 5;

            // Act
            Result result = _service.GetDiagnosis(phq9, gad7, isi, asrs);

            // Assert
            Assert.AreEqual("GAD", result.Diagnosis);
            Assert.AreEqual("CBT, SSRI, SNRI", result.RecommendedMedication);
        }

        [Test]
        public void TestGetDiagnosis_Insomnia()
        {
            // Arrange
            int phq9 = 5;
            int gad7 = 5;
            int isi = 20;
            int asrs = 5;

            // Act
            Result result = _service.GetDiagnosis(phq9, gad7, isi, asrs);

            // Assert
            Assert.AreEqual("Insomnia", result.Diagnosis);
            Assert.AreEqual("CBT-I, Doxepin, Trazodone, Gabapentin", result.RecommendedMedication);
        }

        [Test]
        public void TestGetDiagnosis_AdultADHDInattentive()
        {
            // Arrange
            int phq9 = 5;
            int gad7 = 5;
            int isi = 5;
            int asrs = 15;

            // Act
            Result result = _service.GetDiagnosis(phq9, gad7, isi, asrs);

            // Assert
            Assert.AreEqual("Adult ADHD - inattentive (if A Positive) or impulsivity (if B Positive)", result.Diagnosis);
            Assert.AreEqual("CBT, Adderall (if A positive), Ritalin (if B Positive)", result.RecommendedMedication);
        }

        [Test]
        public void GetDiagnosis_WithMDDAndGAD()
        {
            // Arrange
            int phq9 = 20;
            int gad7 = 15;
            int isi = 5;
            int asrs = 2;

            // Act
            var result = _service.GetDiagnosis(phq9, gad7, isi, asrs);

            // Assert
            Assert.AreEqual("MDD + GAD", result.Diagnosis);
            Assert.AreEqual("CBT, SSRI, SNRI", result.RecommendedMedication);
        }

        /*[Test]
        public void Fail()
        {
            Assert.IsFalse(true);
        }*/
        
        //write more tests
    }
}
