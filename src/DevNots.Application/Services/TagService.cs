using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DevNots.Application.Contracts;
using DevNots.Application.Validations;
using DevNots.Domain;

namespace DevNots.Application.Services
{
    public class TagService: AppService
    {
        private readonly IMapper mapper;
        private readonly ITagRepository tagRepository;
        private readonly CreateTagValidator createTagValidator;

        public TagService(IMapper mapper, ITagRepository tagRepository, CreateTagValidator createTagValidator)
        {
            this.mapper             = mapper;
            this.tagRepository      = tagRepository;
            this.createTagValidator = createTagValidator;
        }

        public async Task<AppResponse<string>> CreateTagAsync(CreateTagRequest request)
        {
            var validationResult = createTagValidator.Validate(request);
            var response = new AppResponse<string>();

            if (validationResult.Errors.Count > 0)
            {
                var errorMessage = validationResult.Errors.FirstOrDefault().ErrorMessage;
                return ErrorResponse(errorMessage, 400, response);
            }

            var tag = mapper.Map<Tag>(request);
            tag.CreatedAt = DateTime.UtcNow;

            var id = await tagRepository.CreateAsync(tag);

            response.Result = id;
            return response;
        }

        public async Task<AppResponse> DeleteTagAsync(DeleteTagRequest request)
        {
            var response = new AppResponse();

            if (string.IsNullOrEmpty(request.Id))
                return ErrorResponse("Id can not be empty.", 400, response);

            var isSuccess = await tagRepository.RemoveAsync(request.Id);

            if (!isSuccess)
            {
                var errorMessage = "Tag not found.";
                return ErrorResponse(errorMessage, 404, response);
            }

            return response;
        }

        public async Task<AppResponse<IEnumerable<TagResponse>>> GetTagListAsync(GetTagListRequest request)
        {
            var response = new AppResponse<IEnumerable<TagResponse>>();

            if (string.IsNullOrEmpty(request.UserId))
                return ErrorResponse("UserId can not be empty.", 400, response);

            var tags = await tagRepository.PaginateAsync(1, request.Limit);

            response.Result = mapper.Map<IEnumerable<TagResponse>>(tags);
            return response;
        }
    }
}
