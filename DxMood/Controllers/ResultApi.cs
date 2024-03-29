/*
 * DxMood
 *
 * This API will allow our frontend JavaScript files to make API calls to display each individual Results and create results to tie to each Result 
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
using DxMood.Services.Interfaces;

namespace IO.Swagger.Controllers
{ 
    /// <summary>
    /// Controller for Results
    /// </summary>
    [ApiController]
    public class ResultApiController : ControllerBase
    { 
        private readonly DxMoodDbContext _dbContext;
        private readonly IDxMoodService _dxMoodService;

        public ResultApiController(DxMoodDbContext dbContext, IDxMoodService dxMoodService)
        {
            _dbContext = dbContext;
            _dxMoodService = dxMoodService;
        }

        /// <summary>
        /// Retrieve an object of Result
        /// </summary>
        /// <remarks>This will retrieve a Result for them to view their result and notes</remarks>
        /// <param name="id">id of a Result</param>
        /// <response code="200">Successful return of Result</response>
        /// <response code="400">bad request</response>
        /// <response code="404">could not find this Result</response>
        [HttpGet]
        [Route("/result/{id}")]
        [ValidateModelState]
        public async Task<IActionResult> GetResult([FromRoute][Required]Guid id)
        { 
            Result? result = await _dbContext.Results.FindAsync(id);

            if(result is null)
            {
                return NotFound();
            }

            Patient? patientResults = await _dbContext.Patients.FindAsync(result.PatientId);
            if(patientResults is not null) 
            {
                result.Patient = patientResults;
            }

            return Ok(result);
        }

        /// <summary>
        /// Deletes the Result with the specified ID.
        /// </summary>
        /// <param name="id">id of a Result</param>
        /// <response code="204">Result was deleted.</response>
        /// <response code="400">bad request</response>
        /// <response code="404">Could not find this Result</response>
        [HttpDelete]
        [Route("/result/{id}")]
        [ValidateModelState]
        public async Task<IActionResult> ResultIdDelete([FromRoute][Required]Guid id)
        { 
            Result? result = await _dbContext.Results.FindAsync(id);

            if (result is null)
            {
                return NotFound();
            }

            _dbContext.Results.Remove(result);

            await _dbContext.SaveChangesAsync();

            return Ok();
        }

        /// <summary>
        /// Update a Result
        /// </summary>
        /// <param name="id">id of a Result</param>
        /// <param name="body"></param>
        /// <response code="204">Result was updated.</response>
        /// <response code="400">bad request</response>
        /// <response code="404">Could not find this Result</response>
        [HttpPut]
        [Route("/result/{id}")]
        [ValidateModelState]
        public async Task<IActionResult> ResultIdPut([FromRoute][Required]Guid id, [FromBody]Result body)
        { 
            Result? result = await _dbContext.Results.FindAsync(id);

            if (result is null) 
            {
                return NotFound();
            }

            result.Phq9 = body.Phq9;
            result.Gad7 = body.Gad7;
            result.Isi = body.Isi;
            result.ASRS = body.ASRS;
            result.Diagnosis = body.Diagnosis;
            result.RecommendedMedication = body.RecommendedMedication;
            result.Note = body.Note;
            result.Date = body.Date;
            result.PatientId = body.PatientId;

            Patient? patientResults = await _dbContext.Patients.FindAsync(result.PatientId);
            if(patientResults is not null)
            {
                result.Patient = patientResults;
            }


            await _dbContext.SaveChangesAsync();

            return Ok(result);
        }

        /// <summary>
        /// Adds a new Result
        /// </summary>
        /// <param name="body"></param>
        /// <response code="200">OK</response>
        /// <response code="400">bad request</response>
        [HttpPost]
        [Route("/result")]
        [ValidateModelState]
        public async Task<IActionResult> ResultPost([FromBody]Result body)
        { 
            body.Id = Guid.NewGuid();
            Patient? patientResults = await _dbContext.Patients.FindAsync(body.PatientId);
            
            if(patientResults is not null) 
            {
                body.Patient = patientResults;
            }

            Doctor? patientsDr = await _dbContext.Doctors.FindAsync(body.Patient.DoctorId);

            if(patientsDr is not null) 
            {
                body.Patient.Doctor = patientsDr;
            }

            Result result = _dxMoodService.GetDiagnosis(body.Phq9, body.Gad7, body.Isi, body.ASRS);

            body.Diagnosis = result.Diagnosis;
            body.RecommendedMedication  = result.RecommendedMedication;

            await _dbContext.Results.AddAsync(body);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(ResultPost), new  {id = result.Id}, body);
        }
    }
}
