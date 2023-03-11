using System.Collections;
using IO.Swagger.Models;

namespace DxMood.Models {
    public class PatientDto
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
        /// Gets or Sets DateOfBirth
        /// </summary>
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Foreign Key
        /// </summary>
        public Guid DoctorId { get; set; }

        /// <summary>
        /// Navigation Property
        /// </summary>
        public virtual Doctor Doctor { get; set; }

        /// <summary>
        /// Gets or Sets Results
        /// </summary>
        public List<Result> Results { get; set; }

        /// <summary>
        /// Gets or Sets Notes
        /// </summary>
        public List<Note> Notes { get; set; }

  }
}