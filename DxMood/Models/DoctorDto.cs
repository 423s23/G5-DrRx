using System.Collections;
using IO.Swagger.Models;

namespace DxMood.Models {
    public class DoctorDto
    {
        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or Sets LastName
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or Sets FirstName
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or Sets UserName
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or Sets Password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or Sets Patients
        /// </summary>
        public List<PatientDto> Patients { get; set; }
  }
}