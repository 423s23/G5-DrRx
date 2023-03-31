using IO.Swagger.Models;

namespace DxMood.Services.Interfaces
{
    /// <summary>
    /// Service will be creating results a patient and will be able to be updated
    /// </summary>
    public interface IDxMoodService
    {
        /// <summary>
        /// This method will take in the four scores and return a result with the four scores, the diagnosis, and the recommended treatment
        /// </summary>
        /// <param name="phq9">GAD 7 Score</param>
        /// <param name="gad7">GAD 8 Score</param>
        /// <param name="isi">GAD 7 Score</param>
        /// <param name="asrs">GAD 8 Score</param>
        /// <returns>ValidationList</returns>
        public Result GetDiagnosis(int phq9, int gad7, int isi, int asrs);
    }
}
