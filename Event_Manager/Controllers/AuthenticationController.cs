using Dapper;
using Event_Manager.BearerToken;
using Event_Manager.Dapper;
using Event_Manager.Data.Request.Authentication;
using Event_Manager.Data.Response;
using Event_Manager.Data.Response.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Serialization;

namespace Event_Manager.Controllers
{
    [Route("api/Authentication")]
    [ApiController]
    public class AuthenticationController : JWTAuthorizedController
    {
        private readonly DapperContext _context;
        public AuthenticationController(DapperContext context)
        {
            _context = context;
        }

        [HttpPost("RegisterNewUser")]
        public IActionResult RegisterNewUser(RegisterNewUserRequest request)
        {
            const int HoursUntilExpiration = 48;
            BaseResponse response = new BaseResponse();

            //TODO: Add validation for email address

            string insertUserQuery = "" +
                $"IF \n" +
                $"( \n" +
                $"  SELECT COUNT(*) \n" +
                $"  FROM [RegistrationRequests] \n" +
                $"  WHERE [registrationEMail] = @UserEmail \n" +
                $"  AND \n" +
                $"  ( \n" +
                $"    ( \n" +
                $"      [registrationConfirmed] = 0 \n" +
                $"      AND [requestExpirationDate] > GETDATE() \n  " +
                $"    ) \n" +
                $"    OR \n" +
                $"    ( \n" +
                $"      [registrationConfirmed] = 1 \n" +
                $"    ) \n" +
                $"  ) \n" +
                $") = 0 \n" +
                $"" +
                $"INSERT INTO[dbo].[RegistrationRequests] \n" +
                $"([registrationCode] \n" +
                $",[registrationEMail] \n" +
                $",[requestExpirationDate]) \n" +
                $" VALUES \n" +
                $"(@RegistrationCode \n" +
                $",@UserEmail \n" +
                $",@ExpirationDateTime \n " +
                $") \n";

            string registrationCode = GenerateRegistrationCode();
            DateTime expirationDateTime = DateTime.Now.AddHours(HoursUntilExpiration);

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("UserEmail", request.Email, DbType.String);
            parameters.Add("RegistrationCode", registrationCode, DbType.String);
            parameters.Add("ExpirationDateTime", expirationDateTime, DbType.DateTime);

            using (IDbConnection connection = _context.CreateConnection())
            {
                int affectedRows = connection.Execute(insertUserQuery, parameters);

                if (affectedRows < 1)
                {
                    response.ResultState = enumResultState.NoRowAffected;
                    response.ResultMessage = "Email already Ex";
                    response.ResultItem = null;
                }
                else
                {
                    //TODO: Send email with registration code

                    RegisterNewUserResponse registerNewUserResponse = new RegisterNewUserResponse
                    {
#if DEBUG
                        RegistrationCode = registrationCode,
#endif
                        ExpirationDateTime = expirationDateTime.ToString(ReponseStandards.DateTimeFormat),
                        RegisteredEmail = request.Email
                    };

                    response.ResultState = enumResultState.Success;
                    response.ResultMessage = "User request registered successfully";
                    response.ResultItem = registerNewUserResponse;
                }

            }

            return Ok(response);
        }

        private string GenerateRegistrationCode()
        {
            string registrationCode = "";

            for (int i = 0; i < 6; i++)
            {
                //generate a random number from 0 to 35
                int randomNumber = new System.Random().Next(0, 35);
                //if the random number is less than 10, add it to the registration code
                if (randomNumber < 10)
                {
                    registrationCode += randomNumber.ToString();
                }
                //if the random number is greater than 10, add the corresponding letter to the registration code
                else
                {
                    registrationCode += ((char)(randomNumber + 55)).ToString();
                }
            }

            return registrationCode;
        }
    }
}
