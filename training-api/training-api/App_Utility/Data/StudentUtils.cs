using api.App_Utility;
using api.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using training_api.Models.DTO;

namespace training_api.App_Utility.Data
{
    public class StudentUtils : DBInterface
    {
        private ErrorLogs _logger = new ErrorLogs();

        public List<StudentReturnDTO> getAll()
        {
            List<StudentReturnDTO> result = new List<StudentReturnDTO>();
            DataTable dt = getByIdentifier("all", "");
            foreach (DataRow row in dt.Rows)
            {

                StudentReturnDTO dto = new StudentReturnDTO();
                dto.Id = Convert.ToInt32(row["id"].ToString());
                dto.Firstname = row["firstname"].ToString();
                dto.Lastname = row["lastname"].ToString();
                dto.Middlename = row["middlename"].ToString();
                dto.Gender = row["gender"].ToString();
                dto.Age = row["age"].ToString();
                dto.Address = row["address"].ToString();
                dto.Course = row["course"].ToString();
                dto.IsActive = Convert.ToBoolean(row["isActive"].ToString());
                if (row["createdDate"].ToString() != "")
                    dto.CreatedDate = Convert.ToDateTime(row["createdDate"].ToString());
                if (row["modifiedDate"].ToString() != "")
                    dto.ModifiedDate = Convert.ToDateTime(row["modifiedDate"].ToString());
                result.Add(dto);
            }

            return result;
        }

        public StudentReturnDTO getById(int? id)
        {
            StudentReturnDTO result = new StudentReturnDTO();
            DataTable dt = getByIdentifier("id", id.ToString());
            if (dt.Rows.Count == 0)
                return null;

            foreach (DataRow row in dt.Rows)
            {

                result.Id = Convert.ToInt32(row["id"].ToString());
                result.Firstname = row["firstname"].ToString();
                result.Lastname = row["lastname"].ToString();
                result.Middlename = row["middlename"].ToString();
                result.Gender = row["gender"].ToString();
                result.Age = row["age"].ToString();
                result.Address = row["address"].ToString();
                result.Course = row["course"].ToString();
                result.IsActive = Convert.ToBoolean(row["isActive"].ToString());
                if (row["createdDate"].ToString() != "")
                    result.CreatedDate = Convert.ToDateTime(row["createdDate"].ToString());
                if (row["modifiedDate"].ToString() != "")
                    result.ModifiedDate = Convert.ToDateTime(row["modifiedDate"].ToString());
            }

            return result;
        }

        public void create(StudentCreateDTO dto, int user_id)
        {

            SqlParameterCollection _params = new SqlCommand().Parameters;
            _params.AddWithValue("@first_name", dto.firstName);
            _params.AddWithValue("@last_name", dto.lastName);
            _params.AddWithValue("@middle_name", dto.middleName);
            _params.AddWithValue("@gender", dto.gender);
            _params.AddWithValue("@age", dto.age);
            _params.AddWithValue("@address", dto.address);
            _params.AddWithValue("@course", dto.course);
            _params.AddWithValue("@user_id", user_id);

            this.ExecuteRead("dbo.sp_students_create", _params);


            if (!string.IsNullOrWhiteSpace(ErrorMessage))
            {
                _logger.createLogs($"UesrsUtils | update | {JsonConvert.SerializeObject(dto) + " " + ErrorMessage}");
            }
        }

        public void update(StudentUpdateDTO dto, int id, int user_id)
        {

            SqlParameterCollection _params = new SqlCommand().Parameters;
            _params.AddWithValue("@id", id);
            _params.AddWithValue("@first_name", dto.firstName);
            _params.AddWithValue("@last_name", dto.lastName);
            _params.AddWithValue("@middle_name", dto.middleName);
            _params.AddWithValue("@gender", dto.gender);
            _params.AddWithValue("@age", dto.age);
            _params.AddWithValue("@address", dto.address);
            _params.AddWithValue("@course", dto.course);
            _params.AddWithValue("@is_active", dto.is_active);
            _params.AddWithValue("@user_id", user_id);

            this.ExecuteRead("dbo.sp_students_update", _params);
            if (!string.IsNullOrWhiteSpace(ErrorMessage))
            {
                _logger.createLogs($"UsersUtils | update | {JsonConvert.SerializeObject(dto) + " " + ErrorMessage}");
            }

        }

        public void changeStatus(int id, int user_id, bool isActive)
        {

            SqlParameterCollection _params = new SqlCommand().Parameters;
            _params.AddWithValue("@id", id);
            _params.AddWithValue("@is_active", isActive);
            _params.AddWithValue("@user_id", user_id);
            this.ExecuteRead("dbo.sp_students_change_status", _params);
            if (!string.IsNullOrWhiteSpace(ErrorMessage))
            {
                _logger.createLogs($"UsersUtils | change Status | {"id: " + id.ToString() + " " + ErrorMessage}");
            }

        }

        public DataTable getByIdentifier(string identifier, string value)
        {
            SqlParameterCollection _params = new SqlCommand().Parameters;
            _params.AddWithValue("@identifier", identifier);
            _params.AddWithValue("@value", value);

            DataTable dt = this.ExecuteRead(@"dbo.sp_students_get", _params);

            return dt;
        }


        public string getToken(int userid)
        {
            // Create payload
            var _payloads = new Dictionary<string, string>();
            _payloads.Add("api_key", "sds");
            _payloads.Add("api_secret", "sdssecret");
            _payloads.Add("userid", userid.ToString());
            string token = JwtManager.GenerateToken(_payloads, 300);

            return token;
        }
    }
}