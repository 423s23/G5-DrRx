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
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.SwaggerGen;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using IO.Swagger.Attributes;

using Microsoft.AspNetCore.Authorization;
using IO.Swagger.Models;
using DxMood.Data;
using DxMood.Models;

namespace IO.Swagger.Controllers
{ 
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    public class PatientApiController : ControllerBase
    { 
        private readonly DxMoodDbContext _dbContext;

        public PatientApiController(DxMoodDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Retrieve an object of patient
        /// </summary>
        /// <remarks>This will retrieve a patient for them to view their result and notes</remarks>
        /// <param name="id">id of a patient</param>
        /// <response code="200">Successful return of patient</response>
        /// <response code="400">bad request</response>
        /// <response code="404">could not find this patient</response>
        [HttpGet]
        [Route("/patient/{id}")]
        [ValidateModelState]
        public async Task<IActionResult> GetPatient([FromRoute][Required]Guid id)
        { 
            Patient? patient = await _dbContext.Patients.FindAsync(id);
            if(patient is null)
            {
                return NotFound();
            }

            PatientDto patientDto = toPatientDto(patient);

            return Ok(patientDto);
        }

        /// <summary>
        /// Deletes the patient with the specified ID.
        /// </summary>
        /// <param name="id">id of a patient</param>
        /// <response code="204">Patient was deleted.</response>
        /// <response code="400">bad request</response>
        /// <response code="404">Could not find this patient</response>
        [HttpDelete]
        [Route("/patient/{id}")]
        [ValidateModelState]
        public async Task<IActionResult> PatientIdDelete([FromRoute][Required]Guid id)
        { 
            Patient? patient = await _dbContext.Patients.FindAsync(id);

            if (patient is null)
            {
                return NotFound();
            }

            _dbContext.Patients.Remove(patient);

            await _dbContext.SaveChangesAsync();

            return Ok();
        }

        /// <summary>
        /// Update a Patient
        /// </summary>
        /// <param name="id">id of a patient</param>
        /// <param name="body"></param>
        /// <response code="204">Patient was updated.</response>
        /// <response code="400">bad request</response>
        /// <response code="404">Could not find this patient</response>
        [HttpPut]
        [Route("/patient/{id}")]
        [ValidateModelState]
        public async Task<IActionResult> PatientIdPut([FromRoute][Required]Guid id, [FromBody]Patient body)
        { 
            Patient? patient = await _dbContext.Patients.FindAsync(id);

            if (patient is null) 
            {
                return NotFound();
            }

            patient.LastName = body.LastName;
            patient.FirstName = body.FirstName;
            patient.DoctorId = body.DoctorId;

            await _dbContext.SaveChangesAsync();

            PatientDto patientDto = toPatientDto(patient);
            
            return Ok(patientDto);
        }

        /// <summary>
        /// Adds a new patient
        /// </summary>
        /// <param name="body"></param>
        /// <response code="200">OK</response>
        /// <response code="400">bad request</response>
        [HttpPost]
        [Route("/patient")]
        [ValidateModelState]
        public async Task<IActionResult> PatientPost([FromBody]Patient body)
        { 
            body.Id = Guid.NewGuid();

            PatientDto patientDto = toPatientDto(body);

            body.Doctor = patientDto.Doctor;

            await _dbContext.Patients.AddAsync(body);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(PatientPost), new  {id = patientDto.Id}, patientDto);
        }

        private PatientDto toPatientDto(Patient patient)
        {
            PatientDto patientDto = new PatientDto()
            {
                Id = patient.Id,
                LastName = patient.LastName,
                FirstName = patient.FirstName,
                DoctorId = patient.DoctorId,
                Results = new List<Result>(),
            };

            Doctor? patientsDr = _dbContext.Doctors.Find(patientDto.DoctorId);
            if(patientsDr is not null)
            {
                patientDto.Doctor = patientsDr;
            }
            else
            {
                throw new ArgumentNullException();
            }

            foreach(Result r in _dbContext.Results) 
            {
                if(r.PatientId == patientDto.Id)
                {
                    patientDto.Results.Add(r);
                }
            }

            return patientDto;
        }
    }
}
