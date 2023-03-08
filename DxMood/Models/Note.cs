/*
 * DxMood
 *
 * This API will allow our frontend JavaScript files to make API calls to display each individual patients and create results to tie to each patient 
 *
 * OpenAPI spec version: 1
 * 
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */
using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace IO.Swagger.Models
{ 
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public partial class Note : IEquatable<Note>
    { 
        /// <summary>
        /// Gets or Sets Id
        /// </summary>

        [DataMember(Name="id")]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or Sets Content
        /// </summary>

        [DataMember(Name="content")]
        public string Content { get; set; }

        /// <summary>
        /// Gets or Sets Date
        /// </summary>

        [DataMember(Name="date")]
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or Sets PatientId
        /// </summary>

        [DataMember(Name="patientId")]
        public Guid PatientId { get; set; }

        /// <summary>
        /// Gets or Sets Patient
        /// </summary>

        [DataMember(Name="patient")]
        public virtual Patient Patient { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class Note {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  Content: ").Append(Content).Append("\n");
            sb.Append("  Date: ").Append(Date).Append("\n");
            sb.Append("  PatientId: ").Append(PatientId).Append("\n");
            sb.Append("  Patient: ").Append(Patient).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="obj">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Note)obj);
        }

        /// <summary>
        /// Returns true if Note instances are equal
        /// </summary>
        /// <param name="other">Instance of Note to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(Note other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return 
                (
                    Id == other.Id ||
                    Id != null &&
                    Id.Equals(other.Id)
                ) && 
                (
                    Content == other.Content ||
                    Content != null &&
                    Content.Equals(other.Content)
                ) && 
                (
                    Date == other.Date ||
                    Date != null &&
                    Date.Equals(other.Date)
                ) && 
                (
                    PatientId == other.PatientId ||
                    PatientId != null &&
                    PatientId.Equals(other.PatientId)
                ) && 
                (
                    Patient == other.Patient ||
                    Patient != null &&
                    Patient.Equals(other.Patient)
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                var hashCode = 41;
                // Suitable nullity checks etc, of course :)
                    if (Id != null)
                    hashCode = hashCode * 59 + Id.GetHashCode();
                    if (Content != null)
                    hashCode = hashCode * 59 + Content.GetHashCode();
                    if (Date != null)
                    hashCode = hashCode * 59 + Date.GetHashCode();
                    if (PatientId != null)
                    hashCode = hashCode * 59 + PatientId.GetHashCode();
                    if (Patient != null)
                    hashCode = hashCode * 59 + Patient.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
        #pragma warning disable 1591

        public static bool operator ==(Note left, Note right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Note left, Note right)
        {
            return !Equals(left, right);
        }

        #pragma warning restore 1591
        #endregion Operators
    }
}
