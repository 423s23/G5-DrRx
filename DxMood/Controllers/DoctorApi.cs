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
using DxMood.Models;
using DxMood.Data;

namespace IO.Swagger.Controllers
{ 
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    public class DoctorApiController : ControllerBase
    { 
       private readonly DxMoodDbContext _dbContext;

        public DoctorApiController(DxMoodDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        /// <summary>
        /// Deletes the doctor with the specified ID.
        /// </summary>
        /// <param name="id">id of doctor</param>
        /// <response code="204">Doctor was deleted.</response>
        /// <response code="400">bad request</response>
        /// <response code="404">Could not find this doctor</response>
        [HttpDelete]
        [Route("/doctor/{id}")]
        [ValidateModelState]
        public async Task<IActionResult> DoctorIdDelete([FromRoute][Required]Guid id)
        { 
            Doctor? doctor = await _dbContext.Doctors.FindAsync(id);

            if (doctor is null)
            {
                return NotFound();
            }

            _dbContext.Doctors.Remove(doctor);

            await _dbContext.SaveChangesAsync();

            return Ok();
        }

        /// <summary>
        /// gets the doctor with the specified id
        /// </summary>
        [HttpGet]
        [Route("/doctor/{id}")]
        [ValidateModelState]
        public async Task<IActionResult> GetDoctor([FromRoute][Required]Guid id)
        { 
            Doctor? doctor = await _dbContext.Doctors.FindAsync(id);
            if(doctor is null)
            {
                return NotFound();
            }

            DoctorDto doctorDto = toDoctorDto(doctor);

            return Ok(doctorDto);
        }

        /// <summary>
        /// Update a Doctor
        /// </summary>
        /// <param name="id">id of doctor</param>
        /// <param name="body"></param>
        /// <response code="204">Doctor was updated.</response>
        /// <response code="400">bad request</response>
        /// <response code="404">Could not find this doctor</response>
        [HttpPut]
        [Route("/doctor/{id}")]
        [ValidateModelState]
        public async Task<IActionResult> DoctorIdPut([FromRoute][Required]Guid id, [FromBody]Doctor body)
        { 
            Doctor? doctor = await _dbContext.Doctors.FindAsync(id);

            if (doctor is null) 
            {
                return NotFound();
            }

            doctor.LastName = body.LastName;
            doctor.FirstName = body.FirstName;
            doctor.UserName = body.UserName;
            doctor.Password = body.Password;

            await _dbContext.SaveChangesAsync();

            DoctorDto doctorDto = toDoctorDto(doctor);

            return Ok(doctorDto);
        }

        /// <summary>
        /// Adds a new doctor
        /// </summary>
        /// <param name="body"></param>
        /// <response code="200">OK</response>
        /// <response code="400">bad request</response>
        [HttpPost]
        [Route("/doctor")]
        [ValidateModelState]
        public async Task<IActionResult> DoctorPost([FromBody]Doctor body)
        {
            body.Id = Guid.NewGuid();

            DoctorDto doctorDto = toDoctorDto(body);

            await _dbContext.Doctors.AddAsync(body);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(DoctorPost), new  {id = doctorDto.Id}, doctorDto);
        }

        /// <summary>
        /// Retrieve an object of doctor
        /// </summary>
        /// <remarks>This will retrieve a doctor for them to view their patients</remarks>
        /// <param name="username">username of doctor</param>
        /// <param name="password">password of doctor</param>
        /// <response code="200">Successful return of doctor</response>
        /// <response code="400">bad request</response>
        /// <response code="404">could not find this doctor</response>
        [HttpPost]
        [Route("/doctor/login")]
        [ValidateModelState]
        public virtual IActionResult GetDoctor([FromBody]Doctor body)
        { 
            Doctor? doctor = body;

            foreach (Doctor dr in _dbContext.Doctors)
            {
                if(dr.UserName == doctor.UserName && dr.Password == doctor.Password)
                {
                    doctor = dr;
                }
            }

            if(doctor is null)
            {
                return NotFound();
            }

            DoctorDto doctorDto = toDoctorDto(doctor);

            return Ok(doctorDto);
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

        private DoctorDto toDoctorDto(Doctor doctor)
        {
            DoctorDto doctorDto = new DoctorDto()
            {
                Id = doctor.Id,
                LastName = doctor.LastName,
                FirstName = doctor.FirstName,
                UserName = doctor.UserName,
                Password = doctor.Password,
                Patients = new List<PatientDto>()
            };

            foreach(Patient p in _dbContext.Patients) 
            {
                if(p.DoctorId == doctor.Id)
                {
                    PatientDto patientDto = toPatientDto(p);
                    doctorDto.Patients.Add(patientDto);
                }
            }
            return doctorDto;
        }
    }
}
