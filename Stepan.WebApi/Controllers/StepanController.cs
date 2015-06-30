using Stepan.Common;
using Stepan.Common.Service;
using Stepan.Repository;
using Stepan.Repository.Models;
using Stepan.WebApi.Filters;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Security;

namespace Stepan.WebApi.Controllers
{    
  [Authorize]
    [RoutePrefix("api/Stepan")]
    public class StepanController : ApiController
    {        
        readonly IProductService service;
       public StepanController(IProductService service)
        {
            this.service = service;
        }

       [AllowAnonymous]
       /// <summary>
       ///Login method.
       /// </summary>
       /// <param name="info">The emailAddess&password of the user.</param>
       [Route("Login")]
       [HttpPost]
       public IHttpActionResult Login(UserInfo info)
       {
           string emailAddress = info.EmailAddress;
           string password = info.Password;
           string antiForgeryToken = "";
           #region Check that request is from MobileDevice or PC
           if (HttpContext.Current.Request.Browser["IsMobileDevice"] == "true")
           {
               #region Hash DeviceId when user login
               IEnumerable<string> headerValues;
               string deviceId = "";
               if (Request.Headers.TryGetValues("DeviceId", out headerValues))
               {
                   deviceId = headerValues.First();
               }
               antiForgeryToken = Hash.Hashing(deviceId);
               #endregion
           }
           else
           {
               #region  Hash Ip Address when user login
               string hostName = Dns.GetHostName();
               Console.WriteLine(hostName);
               string ip = Dns.GetHostByName(hostName).AddressList[0].ToString();
               antiForgeryToken = Hash.Hashing(ip);
               #endregion

           }
           #endregion
           StepanMembershipProvider authorize = new StepanMembershipProvider();
           bool validate = authorize.ValidateUser(emailAddress, password);
           if (validate == true)
           {
               FormsAuthentication.SetAuthCookie(emailAddress, true);
               return Ok(antiForgeryToken);
           }
           else
           {
               return BadRequest("UserName and Password is Incorrect");
           }
       }

     [AntiForgeryActionFilter]
      /// <summary>
      /// Get all Products according to all Categories.
      /// </summary>    
      [HttpGet]
      [Route("GetAllCategories")]
      public IHttpActionResult GetAllCategories()
      {
          try
          {
              var data = service.GetAllCategories();
              return Ok(data);
          }
          catch (Exception exp)
          {
              ExceptionLogging.SendExcepToDB(exp);
              return BadRequest(exp.Message);
          }
      }

        [AntiForgeryActionFilter]
         /// <summary>
         /// Get all Products according to parameter(category).
         /// </summary>
         /// <param name="category">The CategoryName of the Product.</param>
         [HttpGet]
         [Route("GetCategory")]
         public IHttpActionResult GetCategory(string category)
         {
             try
             {
                 if (!string.IsNullOrEmpty(category))
                 {
                     var data = service.GetCategory(category);
                     int check = data.Count();
                     if (check == 0)
                     {
                         throw new System.Exception("Product Not Found in Database according to Category Name");
                     }
                     return Ok(data);
                 }
                 else
                 {
                     throw new ArgumentNullException("Please specify atleast one parameter");
                 }
             }
             catch (Exception exp)
             {
                 ExceptionLogging.SendExcepToDB(exp);
                 return BadRequest(exp.Message);
             }
         }

        [AntiForgeryActionFilter]
           ///<summary>
           ///Get all Products according to all Brands.
           ///</summary>
        [HttpGet]
        [Route("GetAllBrandNames")]
        public IHttpActionResult GetAllBrandNames()
        {
            try
            {
                var data = service.GetAllBrandNames();
                return Ok(data);
            }
            catch (Exception exp)
            {
                ExceptionLogging.SendExcepToDB(exp);
                return BadRequest(exp.Message);
            }
        }

        [AntiForgeryActionFilter]
           ///<summary>
           ///Get all Products according to parameter(brand).
           ///</summary>
          /// <param name="brand">The BrandName of the Product.</param>
        [HttpGet]
        [Route("GetBrandName")]
        public IHttpActionResult GetBrandName(string brand)
        {
            try
            {
                if (!string.IsNullOrEmpty(brand))
                {
                    var data = service.GetBrandName(brand);
                    int check = data.Count();
                    if (check == 0)
                    {
                        throw new System.Exception("Product Not Found in Database according to Brand Name");
                    }
                    return Ok(data);
                }
                else
                {
                    throw new ArgumentNullException("Please specify atleast one parameter");
                }
            }
            catch (Exception exp)
            {
                ExceptionLogging.SendExcepToDB(exp);
                return BadRequest(exp.Message);
            }
        }

        [AntiForgeryActionFilter]
         ///  <summary>
           ///Get all Products according to all ChemicalGroups.
          /// </summary>
        [HttpGet]
        [Route("GetAllChemicalGroups")]
        public IHttpActionResult GetAllChemicalGroups()
        {
            try
            {
                var data = service.GetAllChemicalGroups();
                return Ok(data);
            }
            catch (Exception exp)
            {
                ExceptionLogging.SendExcepToDB(exp);
                return BadRequest(exp.Message);
            }
        }

        [AntiForgeryActionFilter]
           ///<summary>
           ///Get all Products according to parameter(ChemicalGroup).
         ///  </summary>
           ///<param name="chemGroup">The ChemicalGroup of the Product.</param>
        [HttpGet]
        [Route("GetChemicalGroup")]
        public IHttpActionResult GetChemicalGroup(string chemGroup)
        {
            try
            {
                if (!string.IsNullOrEmpty(chemGroup))
                {
                    var data = service.GetChemicalGroup(chemGroup);
                    int check = data.Count();
                    if (check == 0)
                    {
                        throw new System.Exception("Product Not Found in Database according to Chemical Description");
                    }
                    return Ok(data);
                }
                else
                {
                    throw new ArgumentNullException("Please specify atleast one parameter");
                }
            }
            catch (Exception exp)
            {
                ExceptionLogging.SendExcepToDB(exp);
                return BadRequest(exp.Message);
            }
        }

        [AntiForgeryActionFilter]
          /// <summary>
           ///Get all Products according to all ChemicalName.
           ///</summary>
        [HttpGet]
        [Route("GetAllChemicalNames")]
        public IHttpActionResult GetAllChemicalNames()
        {
            try
            {
                var data = service.GetAllChemicalNames();
                return Ok(data);
            }
            catch (Exception exp)
            {
                ExceptionLogging.SendExcepToDB(exp);
                return BadRequest(exp.Message);
            }
        }

        [AntiForgeryActionFilter]
          /// <summary>
           ///Get all Products according to parameter(ChemicalName).
           ///</summary>
           ///<param name="name">The ChemicalName of the Product.</param>
        [HttpGet]
        [Route("GetChemicalName")]
        public IHttpActionResult GetChemicalName(string name)
        {
            try
            {
                if (!string.IsNullOrEmpty(name))
                {
                    var data = service.GetChemicalName(name);
                    int check = data.Count();
                    if (check == 0)
                    {
                        throw new System.Exception("Product Not Found in Database according to Chemical Name ");
                    }
                    return Ok(data);
                }
                else
                {
                    throw new ArgumentNullException("Please specify atleast one parameter");
                }
            }
            catch (Exception exp)
            {
                ExceptionLogging.SendExcepToDB(exp);
                return BadRequest(exp.Message);
            }
        }

        [AntiForgeryActionFilter]
         ///<summary>
         ///Get all Products according to parameter(Search Parameter).
         ///</summary>
         ///<param name="query">The SearchParameter(ProductName,BrandName,CategoryName,ChemicalDescription) of the Product.</param>
        [HttpGet]
        [Route("GetSearchResult")]
        public IHttpActionResult GetSearchResult(string query)
        {
            try
            {
                if (!string.IsNullOrEmpty(query))
                {
                    var data = service.GetSearchResult(query);
                    int check = data.Count();
                    if (check == 0)
                    {
                        throw new System.Exception("Product Not Found in Database according to search parameter");
                    }
                    return Ok(data);
                }
                else
                {
                    throw new ArgumentNullException("Please specify atleast one parameter");
                }
            }
            catch (Exception exp)
            {
                ExceptionLogging.SendExcepToDB(exp);
                return BadRequest(exp.Message);
            }
        }

        [AntiForgeryActionFilter]
           ///<summary>
           ///Get all Brands according to parameter(category).
           ///</summary>
          /// <param name="category">The Category of the Product.</param>
        [HttpGet]
        [Route("GetSubCategoryOfCategory")]
        public IHttpActionResult GetSubCategoryOfCategory(string category)
        {
            try
            {
                if (!string.IsNullOrEmpty(category))
                {
                    var data = service.GetSubCategoryOfCategory(category);
                    int check = data.Count();
                    if (check == 0)
                    {
                        throw new System.Exception("Product Not Found in Database according to Category Name ");
                    }
                    return Ok(data);
                }
                else
                {
                    throw new ArgumentNullException("Please specify atleast one parameter");
                }
            }
            catch (Exception exp)
            {
                ExceptionLogging.SendExcepToDB(exp);
                return BadRequest(exp.Message);
            }
        }

        [AntiForgeryActionFilter]
           ///<summary>
           ///Get all Description of Products according to parameter(brand).
           ///</summary>
           ///<param name="brand">The BrandName of the Product.</param>
        [HttpGet]
        [Route("GetDescriptionOfSubCategory")]
        public IHttpActionResult GetDescriptionOfSubCategory(string brand)
        {
            try
            {
                if (!string.IsNullOrEmpty(brand))
                {
                    var data = service.GetDescriptionOfSubCategory(brand);
                    int check = data.Count();
                    if (check == 0)
                    {
                        throw new System.Exception("Product Not Found in Database according to Brand Name");
                    }

                    return Ok(data);
                }
                else
                {
                    throw new ArgumentNullException("Please specify atleast one parameter");
                }
            }
            catch (Exception exp)
            {
                ExceptionLogging.SendExcepToDB(exp);
                return BadRequest(exp.Message);
            }
        }

        [AntiForgeryActionFilter]
          /// <summary>
           ///Get all Information of Products according to HashTags(parameter).
        /// </summary>
           ///<param name="parameter">The HashTags of the Product.</param>
        [HttpGet]
        [Route("GetData")]
        public IHttpActionResult GetData(string parameter)
        {
            try
            {
                if (!string.IsNullOrEmpty(parameter))
                {
                    var data = service.GetData(parameter);
                    int check = data.Count();
                    if (check == 0)
                    {
                        throw new System.Exception("Product Not Found in Database according to Category Name ");
                    }
                    return Ok(data);
                }
                else
                {
                    throw new ArgumentNullException("Please specify atleast one parameter");
                }
            }
            catch (Exception exp)
            {
                ExceptionLogging.SendExcepToDB(exp);
                return BadRequest(exp.Message);
            }
        }

        // StepanEktron_oldEntities db = new StepanEktron_oldEntities();
        //[AntiForgeryActionFilter]
        ///// <summary>
        ///// Get all Information of Products according to HashTags(parameter).
        ///// </summary>
        ///// <param name="parameter">The HashTags of the Product.</param>
        //[HttpGet]
        //[Route("GetNotes")]
        //public IHttpActionResult GetNotes(long id)
        //{
        //    try
        //    {
        //        if (id != 0)
        //        {
        //            var data = service.GetNotes(id);
        //            return Ok(data);
        //        }
        //        else
        //        {
        //            throw new ArgumentNullException("Id can not be 0");
        //        }
        //    }
        //    catch (Exception exp)
        //    {
        //        ExceptionLogging.SendExcepToDB(exp);
        //        return BadRequest(exp.Message);
        //    }
        //}


        //[AntiForgeryActionFilter]
        ///// <summary>
        ///// Get all Information of Products according to HashTags(parameter).
        ///// </summary>
        ///// <param name="parameter">The HashTags of the Product.</param>
        //[HttpPost]
        //[Route("CreateNotes")]
        //public IHttpActionResult CreateNotes(int pageId, int productId, string content)
        //{
        //    try
        //    {

        //        if (!string.IsNullOrEmpty(content) && pageId != 0 && productId != 0)
        //        {
        //            //var data = service.GetNotes(id);
        //            //return Ok(data);
        //            //Note data = new Note();
        //            //data.Notes_ProductId = productId;
        //            //data.Notes_PageId = pageId;
        //            //data.Notes_Content = content;
        //            //data.Notes_CreatedDate = DateTime.Now;
        //            // db.Notes.Add(data);
        //            // db.SaveChanges();
        //            return Ok("Note has been added successfully");
        //        }
        //        else
        //        {
        //            throw new ArgumentNullException("Please specify atleast one parameter");
        //        }
        //    }
        //    catch (Exception exp)
        //    {
        //        ExceptionLogging.SendExcepToDB(exp);
        //        return BadRequest(exp.Message);
        //    }
        //}

        // //[AntiForgeryActionFilter]
        // ///// <summary>
        // ///// Get all Information of Products according to HashTags(parameter).
        // ///// </summary>
        // ///// <param name="parameter">The HashTags of the Product.</param>
        // //[HttpPost]
        // //[Route("EditNotes")]
        // //public IHttpActionResult EditNotes(int id, int pageId, int productId, string content)
        // //{
        // //    try
        // //    {
        // //        if (!string.IsNullOrEmpty(content) && pageId != 0 && productId != 0 && id != 0)
        // //        {
        // //            Note data = db.Notes.Find(id);
        // //            data.Notes_ProductId = productId;
        // //            data.Notes_PageId = pageId;
        // //            data.Notes_Content = content;
        // //            data.Notes_CreatedDate = DateTime.Now;
        // //              db.Entry(data).State = EntityState.Modified;
        // //              try 
        // //              {
        // //                  db.SaveChanges();
        // //              }
        // //              catch (DbUpdateConcurrencyException)
        // //              {
        // //                  throw ;
        // //              }
        // //            return Ok(data);
        // //        }
        // //        else
        // //        {
        // //            throw new ArgumentNullException("Parameter not be empty");
        // //        }
        // //    }
        // //    catch (Exception exp)
        // //    {
        // //        ExceptionLogging.SendExcepToDB(exp);
        // //        return BadRequest(exp.Message);
        // //    }
        // //}

        // //[AntiForgeryActionFilter]
        // ///// <summary>
        // ///// Get all Information of Products according to HashTags(parameter).
        // ///// </summary>
        // ///// <param name="parameter">The HashTags of the Product.</param>
        // //[HttpPost]
        // //[Route("DeleteNotes")]
        // //public IHttpActionResult DeleteNotes(int id)
        // //{
        // //    try
        // //    {
        // //        if (id != 0)
        // //        {
        // //            Note data = db.Notes.Find(id);
        // //            db.Notes.Remove(data);
        // //            db.SaveChanges();
        // //            return Ok("Deleted successfully");
        // //        }
        // //        else
        // //        {
        // //            throw new ArgumentNullException("Parameter not be empty");
        // //        }
        // //    }
        // //    catch (Exception exp)
        // //    {
        // //        ExceptionLogging.SendExcepToDB(exp);
        // //        return BadRequest(exp.Message);
        // //    }
        // //}
    }
}
