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
            var result = _service.GetDiagnosis(phq9, gad7, isi, asrs);

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
            var result = _service.GetDiagnosis(phq9, gad7, isi, asrs);

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
            var result = _service.GetDiagnosis(phq9, gad7, isi, asrs);

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
            var result = _service.GetDiagnosis(phq9, gad7, isi, asrs);

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
            var result = _service.GetDiagnosis(phq9, gad7, isi, asrs);

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

        [Test]
        public void GetDiagnosis_WithMDDAndInsomnia()
        {
            // Arrange
            int phq9 = 20;
            int gad7 = 7;
            int isi = 18;
            int asrs = 2;

            // Act
            var result = _service.GetDiagnosis(phq9, gad7, isi, asrs);

            // Assert
            Assert.AreEqual("MDD + Insomnia", result.Diagnosis);
            Assert.AreEqual("Mirtazapine, Doxepin, Trazodone", result.RecommendedMedication);
        }

        [Test]
        public void GetDiagnosis_WithMDD_GAD_Insomia()
        {
            // Arrange
            int phq9 = 20;
            int gad7 = 18;
            int isi = 18;
            int asrs = 2;

            // Act
            var result = _service.GetDiagnosis(phq9, gad7, isi, asrs);

            // Assert
            Assert.AreEqual("MDD + GAD + Insomnia", result.Diagnosis);
            Assert.AreEqual("Mirtazapine or Doxepin", result.RecommendedMedication);
        }

        [Test]
        public void GetDiagnosis_WithADHDAndInsomia()
        {
            // Arrange
            int phq9 = 9;
            int gad7 = 9;
            int isi = 18;
            int asrs = 15;

            // Act
            var result = _service.GetDiagnosis(phq9, gad7, isi, asrs);

            // Assert
            Assert.AreEqual("ADHD inattentive (if A Positive)/impulsivity (if B positive) + Insomnia", result.Diagnosis);
            Assert.AreEqual("CBT-i, Bupropion, or Strattera (for A Positive) or Not covered in diagnosis table if B Positive", result.RecommendedMedication);
        }

        [Test]
        public void GetDiagnosis_AllPositive()
        {
            // Arrange
            int phq9 = 20;
            int gad7 = 18;
            int isi = 18;
            int asrs = 18;

            // Act
            var result = _service.GetDiagnosis(phq9, gad7, isi, asrs);

            // Assert
            Assert.AreEqual("ADHD + GAD + MDD + Insomnia", result.Diagnosis);
            Assert.AreEqual("Not covered in diagnosis table", result.RecommendedMedication);
        }

        /*[Test]
        public void Fail()
        {
            Assert.IsFalse(true);
        }*/
        
        
    }
}
