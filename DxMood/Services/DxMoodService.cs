using System.Threading.Tasks;
using DxMood.Services.Interfaces;
using IO.Swagger.Models;

namespace DxMood.Services
{
    public class DxMoodService : IDxMoodService
    {
        public Result GetDiagnosis(int phq9, int gad7, int isi, int asrs)
        {
            // Calculate diagnosis and recommended treatment based on the scores
            string diagnosis = CalculateDiagnosis(phq9, gad7, isi, asrs);
            string treatment = CalculateTreatment(diagnosis);

            // Create a new result object with the scores, diagnosis, and recommended treatment
            Result result = new Result
            {
                Phq9 = phq9,
                Gad7 = gad7,
                Isi = isi,
                ASRS = asrs,
                Diagnosis = diagnosis,
                RecommendedMedication = treatment,
            };

            // Return the result
            return result;
        }

        // Helper methods to calculate the diagnosis based on the scores
        private string CalculateDiagnosis(int phq9, int gad7, int isi, int asrs)
        {
            // Perform diagnosis calculation here
            bool phq9Positive = phq9>10 ? true:false;
            bool gad7Positive = gad7>10 ? true:false;
            bool isiPositive = isi>15 ? true:false;
            bool asrsPositive = asrs>14 ? true:false;

            if (phq9Positive == false && gad7Positive == false && isiPositive == false && asrsPositive == false) {
                return("No Psychiatric Dx");
            } else if (phq9Positive == true && gad7Positive == false && isiPositive == false && asrsPositive == false) {
                return("MDD");
            } else if (phq9Positive == false && gad7Positive == true && isiPositive == false && asrsPositive == false) {
                return("GAD");
            } else if (phq9Positive == false && gad7Positive == false && isiPositive == true && asrsPositive == false) {
                return("Insomnia");
            } else if (phq9Positive == false && gad7Positive == false && isiPositive == false && asrsPositive == true) {
                return("Adult ADHD - inattentive (if A Positive) or impulsivity (if B Positive)");
            } else if (phq9Positive == true && gad7Positive == true && isiPositive == false && asrsPositive == false) {
                return("MDD + GAD");
            } else if (phq9Positive == true && gad7Positive == false && isiPositive == true && asrsPositive == false) {
                return("MDD + Insomnia");
            } else if (phq9Positive == true && gad7Positive == true && isiPositive == true && asrsPositive == false) {
                return("MDD + GAD + Insomnia");
            } else if (phq9Positive == false && gad7Positive == false && isiPositive == true && asrsPositive == true) {
                return("ADHD inattentive + Insomnia");
            } else if (phq9Positive == true && gad7Positive == true && isiPositive == true && asrsPositive == true) {
                return("PANIC!!!");
            } else {
                return("Invalid test results");
            }
        }

        // Helper method to calculate the recommended treatment based on the diagnosis
        private string CalculateTreatment(string diagnosis)
        {
            // Perform treatment calculation here
            return "Some treatment";
        }
    }
}
