using api.App_Utility;
using api.Authorization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using training_api.App_Utility.Data;
using training_api.Models.DTO;

namespace training_api.Controllers
{
    [RoutePrefix("api/students")]
    public class StudentsController : ApiController
    {
        private ErrorLogs _logger = new ErrorLogs();
        private StudentUtils _studentUtils = new StudentUtils();

        [Authorizations]
        [Route("")]
        [HttpGet]
        public IHttpActionResult getAll()
        {

            try
            {
                var result = _studentUtils.getAll();

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.createLogs(ex);
                return InternalServerError(ex);
            }
        }

        [Authorizations]
        [Route("{id}")]
        [HttpGet]
        public IHttpActionResult getById(int id)
        {

            try
            {

                var result = _studentUtils.getById(id);
                if (result == null)
                    return BadRequest("User not found!");

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.createLogs(ex);
                return InternalServerError(ex);
            }
        }

        [Authorizations]
        [Route("")]
        [HttpPost]
        public IHttpActionResult create([FromBody] StudentCreateDTO dto)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.SelectMany(x => x.Errors).Select(s => s.ErrorMessage).FirstOrDefault());
            try
            {
                int user_id = Convert.ToInt32(new AuthTokenParser(this.Request).ReadValue("userid"));

                //string unValidate = _studentUtils.validateUsername(dto.username);
                //if (unValidate != null)
                //    return BadRequest(unValidate);

                //var emailValidate = _userUtils.validateEmail(dto.email);
                //if (emailValidate != null)
                //    return BadRequest(emailValidate);

                _studentUtils.create(dto, user_id);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.createLogs(ex, JsonConvert.SerializeObject(dto));
                return InternalServerError(ex);
            }

        }

        [Authorizations]
        [Route("{id}")]
        [HttpPut]
        public IHttpActionResult update(int id, [FromBody] StudentUpdateDTO dto)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.SelectMany(x => x.Errors).Select(s => s.ErrorMessage).FirstOrDefault());
            try
            {

                int user_id = Convert.ToInt32(new AuthTokenParser(this.Request).ReadValue("userid"));

                var user = _studentUtils.getById(id);
                if (user == null)
                    return BadRequest("User not found!");

                //if (user.Email != dto.email)
                //{
                //    var emailValidate = _userUtils.validateEmail(dto.email);
                //    if (emailValidate != null)
                //        return BadRequest(emailValidate);
                //}

                _studentUtils.update(dto, id, user_id);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.createLogs(ex, JsonConvert.SerializeObject(dto));
                return InternalServerError(ex);
            }
        }

        [Authorizations]
        [Route("change/{id}")]
        [HttpPut]
        public IHttpActionResult changeStatus(int id, bool status)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.SelectMany(x => x.Errors).Select(s => s.ErrorMessage).FirstOrDefault());

            try
            {


                int user_id = Convert.ToInt32(new AuthTokenParser(this.Request).ReadValue("userid"));

                var user = _studentUtils.getById(id);
                if (user == null)
                    return BadRequest("User not found!");


                _studentUtils.changeStatus(id, user_id, status);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.createLogs(ex, JsonConvert.SerializeObject(ex));
                return InternalServerError(ex);
            }
        }
    }
}