using api.App_Utility;
using api.App_Utility.Data;
using api.Authorization;
using api.Models.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace training_api.Controllers
{
    [RoutePrefix("api/users")]
    public class UsersController : ApiController
    {
        private ErrorLogs _logger = new ErrorLogs();
        private UsersUtils _userUtils = new UsersUtils();

        [Authorizations]
        [Route("profile")]
        [HttpGet]
        public IHttpActionResult getProfile()
        {

            try
            {
                int user_id = Convert.ToInt32(new AuthTokenParser(this.Request).ReadValue("userid"));

                var result = _userUtils.getById(user_id);
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
        [HttpGet]
        public IHttpActionResult getAll()
        {

            try
            {
                var result = _userUtils.getAll();

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

                var result = _userUtils.getById(id);
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
        public IHttpActionResult create([FromBody] UsersCreateDTO dto)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.SelectMany(x => x.Errors).Select(s => s.ErrorMessage).FirstOrDefault());
            try
            {
                int user_id = Convert.ToInt32(new AuthTokenParser(this.Request).ReadValue("userid"));

                string unValidate = _userUtils.validateUsername(dto.username);
                if (unValidate != null)
                    return BadRequest(unValidate);

                var emailValidate = _userUtils.validateEmail(dto.email);
                if (emailValidate != null)
                    return BadRequest(emailValidate);

                _userUtils.create(dto, user_id);

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
        public IHttpActionResult update(int id, [FromBody] UsersUpdateDTO dto)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.SelectMany(x => x.Errors).Select(s => s.ErrorMessage).FirstOrDefault());
            try
            {

                int user_id = Convert.ToInt32(new AuthTokenParser(this.Request).ReadValue("userid"));

                var user = _userUtils.getById(id);
                if (user == null)
                    return BadRequest("User not found!");

                if (user.Email != dto.email)
                {
                    var emailValidate = _userUtils.validateEmail(dto.email);
                    if (emailValidate != null)
                        return BadRequest(emailValidate);
                }

                _userUtils.update(dto, id, user_id);

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

                var user = _userUtils.getById(id);
                if (user == null)
                    return BadRequest("User not found!");


                _userUtils.changeStatus(id, user_id, status);

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