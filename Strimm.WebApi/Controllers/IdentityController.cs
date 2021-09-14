using Strimm.Data.Interfaces;
using Strimm.Model.WebModel;
using Strimm.Shared;
using Strimm.WebApi.HttpResponse;
using Strimm.WebApi.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Strimm.WebApi.Controllers
{
    public class IdentityController : BaseApiController
    {
        private IUserRepository userRepository;

        public IdentityController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        /// <summary>
        /// This method should be used to authenticate user with Strimm
        /// </summary>
        /// <param name="credentials">Instance of UserCredentials</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/login", Name = "Login")]
        public async Task<HttpResponseMessage> Login([FromBody] UserCredentials credentials)
        {
            return await Task.Run<HttpResponseMessage>(() =>
            {
                var response = new DataOperationResponse(this.Request);

                try
                {
                    if (credentials == null)
                    {
                        throw new Exception("Invalid user credentials specified");
                    }

                    var user = userRepository.GetUserPoByLoginIdentifier(credentials.LoginIdentifier);
                    string hashedPassword = CryptoUtils.HashPassword(CryptoUtils.GetPasswordWithSalt(user.Email, credentials.Password));
                    Guid authToken = userRepository.CreateAuthTokenForUserById(user.UserId);

                    var modelFactory = new ModelFactory(this.Request);

                    if (authToken != Guid.Empty)
                    {
                        response.Status = DataOperationStatus.Success;
                        response.Type = OperationType.Authenticate;
                        response.Data = modelFactory.CreateUserAuthModel(user, authToken);
                    }
                    else
                    {
                        response.Status = DataOperationStatus.Forbidden;
                        response.Type = OperationType.Authenticate;
                        response.Message = "Invalid user credentials specified.";
                    }
                }
                catch (Exception ex)
                {
                    response.Status = DataOperationStatus.Exception;
                    response.Exception = ex;
                    response.Type = OperationType.Authenticate;
                }

                return SetupHttpResponse(response);
            });
        }

        /// <summary>
        /// This method will validate/challenge user's authentication token
        /// </summary>
        /// <param name="token">Authentication Token</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/user/validate/{token}", Name="ValidateToken")]
        public async Task<HttpResponseMessage> ValidateAuthToken(Guid token)
        {
            return await Task.Run<HttpResponseMessage>(() =>
            {
                var response = new DataOperationResponse(this.Request);

                try
                {
                    if (token == null || token == Guid.Empty)
                    {
                        throw new Exception("Invalid authentication token specified");
                    }

                    bool isValid = userRepository.ValidateAuthToken(token);

                    if (isValid)
                    {
                        response.Status = DataOperationStatus.Success;
                        response.Type = OperationType.Authenticate;
                        response.Data = null;
                    }
                    else
                    {
                        response.Status = DataOperationStatus.Forbidden;
                        response.Type = OperationType.Authenticate;
                        response.Message = "Invalid authentication token specified.";
                    }
                }
                catch (Exception ex)
                {
                    response.Status = DataOperationStatus.Exception;
                    response.Exception = ex;
                    response.Type = OperationType.Authenticate;
                }

                return SetupHttpResponse(response);
            });
        }

        [HttpGet]
        [Route("api/user/signout/{token}", Name="SingOut")]
        public async Task<HttpResponseMessage> SignOut(Guid token)
        {
            return await Task.Run<HttpResponseMessage>(() =>
            {
                var response = new DataOperationResponse(this.Request);

                try
                {
                    if (token == null || token == Guid.Empty)
                    {
                        throw new Exception("Invalid authentication token specified");
                    }

                    userRepository.SignOut(token);
                    bool isValid = userRepository.ValidateAuthToken(token);

                    if (!isValid)
                    {
                        response.Status = DataOperationStatus.Success;
                        response.Type = OperationType.SignOut;
                        response.Data = null;
                    }
                    else
                    {
                        response.Status = DataOperationStatus.Forbidden;
                        response.Type = OperationType.SignOut;
                        response.Message = "System error. Unable to sign out the user";
                    }
                }
                catch (Exception ex)
                {
                    response.Status = DataOperationStatus.Exception;
                    response.Exception = ex;
                    response.Type = OperationType.SignOut;
                }

                return SetupHttpResponse(response);
            });
        }
    }
}
