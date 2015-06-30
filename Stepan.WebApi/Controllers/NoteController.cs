using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Stepan.WebApi.Filters;
using Stepan.Common.Service;
using Stepan.Common;
using Ninject;
using Stepan.Repository.Models;

namespace Stepan.WebApi.Controllers
{
    //[Authorize]
    [RoutePrefix("api/Note")]
    public class NoteController : ApiController
    {
        StepanEktron_oldEntities db = new StepanEktron_oldEntities();
        readonly INoteServices service;
        public NoteController(INoteServices service)
        {
            this.service = service;
        }

       [AntiForgeryActionFilter]
        [RouteAttribute("GetAll")]
        [HttpGet]
        /// <summary>
        ///  Get all Notes.
        /// </summary>
        public IHttpActionResult GetAll()
        {
            try
            {
                var data = service.GetNotes();
                return Ok(data);
            }
            catch (Exception exp)
            {
                ExceptionLogging.SendExcepToDB(exp);
                return BadRequest(exp.Message);
            }
        }

        [AntiForgeryActionFilter]
        [RouteAttribute("GetById")]
        [HttpGet]
        /// <summary>
        ///  Get Notes according to parameter(id).
        /// </summary>
        /// <param name="id">The id of note</param>
        public IHttpActionResult GetById(long id)
        {
            try
            {
                if (id != 0)
                {
                    var data = service.GetNote(id);
                    //int check = data.Count();
                    //if (check == 0)
                    //{
                    //    throw new System.Exception("Note not Found in Database according to ID");
                    //}
                    return Ok(data);
                }
                else
                {
                    throw new ArgumentNullException("Please specify parameter");
                }
            }
            catch (Exception exp)
            {
                ExceptionLogging.SendExcepToDB(exp);
                return BadRequest(exp.Message);
            }
        }
        [AntiForgeryActionFilter]
        [RouteAttribute("Edit")]
        [HttpPut]
        /// <summary>
        ///  Update Notes according to parameter(note).
        /// </summary>
        /// <param name="note.Id">The note fields.</param>

        public IHttpActionResult Edit(NoteEx note)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var data = service.Update(note);
                    
                    //if (data == false)
                    //{
                    //    throw new System.Exception("Note not Found in Database according to ID");
                    //}
                    return Ok(data);
                }
                else
                {
                    throw new Exception("Update operation failed");
                }
            }
            catch (Exception exp)//DbUpdateConcurrencyException
            {
                ExceptionLogging.SendExcepToDB(exp);
                return BadRequest(exp.Message);
            }
        }
        [AntiForgeryActionFilter]
        [RouteAttribute("Create")]
        [HttpPost]
        /// <summary>
        ///  Create Notes according to parameter(note).
        /// </summary>
        /// <param name="note">The collection of the note fields.</param>

        public IHttpActionResult PostNote(NoteEx note)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var data = service.Add(note);
                    //if (data == false)
                    //{
                    //    throw new System.Exception("Note is not Created in Database");
                    //}
                    return Ok(data);
                }
                else
                {
                    throw new Exception("Create operation failed");
                }
            }
            catch (Exception exp)//DbUpdateConcurrencyException
            {                
                ExceptionLogging.SendExcepToDB(exp);
                return BadRequest(exp.Message);
            }
        }
        
        [AntiForgeryActionFilter]
        [RouteAttribute("Delete")]
        [HttpDelete]
        /// <summary>
        /// Delete Notes according to parameter(id).
        /// </summary>
        /// <param name="id">The id of the Notes.</param>
        public IHttpActionResult Delete(long id)
        {
            try
            {
                service.Remove(id);
                return Ok("deleted successfully");
                //if (data == true)
                //{
                //    return Ok("Note Deleted Succesfully");
                //}
                //else
                //{
                //    throw new Exception("Delete operation failed");
                //}
            }
            catch (Exception exp)
            {
                ExceptionLogging.SendExcepToDB(exp);
                return BadRequest(exp.Message);
            } 
        }

        [AntiForgeryActionFilter]
        [RouteAttribute("SearchNote")]
        [HttpGet]
        /// <summary>
        /// Delete Notes according to parameter(id).
        /// </summary>
        /// <param name="id">The id of the Notes.</param>
        public IHttpActionResult SearchNote(string parameter)
        {
            try
            {
                var data = service.SearchNote(parameter);
                int check = data.Count();
                if (check != 0 )
                {
                    return Ok(data);
                }
                else
                {
                    throw new Exception("Search operation failed");
                }
            }
            catch (Exception exp)
            {
                ExceptionLogging.SendExcepToDB(exp);
                return BadRequest(exp.Message);
            }
        }
    }
}