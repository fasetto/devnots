using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DevNots.Application.Contracts;
using DevNots.Application.Validations;
using DevNots.Domain;
using FluentValidation;

namespace DevNots.Application.Services
{
    public class UserService: AppService
    {
        private readonly IMapper mapper;
        private readonly IUserRepository userRepository;
        private readonly RegisterUserValidator registerUserValidator;
        private readonly UpdateUserValidator updateUserValidator;
        public UserService(
            IMapper mapper,
            IUserRepository userRepository,
            RegisterUserValidator registerUserValidator,
            UpdateUserValidator updateUserValidator
        )
        {
            this.mapper = mapper;
            this.userRepository = userRepository;
            this.registerUserValidator = registerUserValidator;
            this.updateUserValidator = updateUserValidator;
        }

        /// <summary>
        /// Registers given user to the database.
        /// </summary>
        /// <param name="request">User to register</param>
        /// <returns>Id of the registered user</returns>
        public async Task<AppResponse<string>> RegisterAsync(RegisterUserRequest request)
        {
            var validationResult = registerUserValidator.Validate(request);
            var response = new AppResponse<string>();

            if (validationResult.Errors.Any())
            {
                var errorMessage = validationResult.Errors.FirstOrDefault().ErrorMessage;
                return ErrorResponse(errorMessage, 400, response);
            }

            var user = mapper.Map<User>(request);
            user.CreatedAt = DateTime.UtcNow;

            var id = await userRepository.CreateAsync(user);

            response.Result = id;
            return response;
        }

        public async Task<AppResponse> DeleteUserAsync(DeleteUserRequest request)
        {
            var response = new AppResponse();

            if (string.IsNullOrEmpty(request.Id))
                return ErrorResponse("Id can not be empty.", 400, response);

            var isSuccess = await userRepository.RemoveAsync(request.Id);

            if (!isSuccess)
            {
                var errorMessage = "User not found.";
                return ErrorResponse(errorMessage, 404, response);
            }

            return response;
        }

        public async Task<AppResponse<bool>> UpdateUserAsync(UpdateUserRequest request)
        {
            var validationResult = updateUserValidator.Validate(request);
            var response = new AppResponse<bool>();

            if (validationResult.Errors.Any())
            {
                var errorMessage = validationResult.Errors.FirstOrDefault().ErrorMessage;
                return ErrorResponse(errorMessage, 400, response);
            }

            var user = mapper.Map<User>(request);
            var isUpdated = await userRepository.UpdateAsync(request.Id, user);

            response.Result = isUpdated;
            return response;
        }

    }
}
