using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DevNots.Application.Contracts;
using DevNots.Application.Validations;
using DevNots.Domain;

namespace DevNots.Application.Services
{
    public class NoteService:AppService
    {
        private readonly IMapper mapper;
        private readonly INoteRepository noteRepository;
        private readonly UpdateNoteValidator updateNoteValidator;
        private readonly AddNoteValidator addNoteValidator;

        public NoteService(
            IMapper mapper,
            INoteRepository noteRepository,
            AddNoteValidator addNoteValidator,
            UpdateNoteValidator updateNoteValidator
        )
        {
            this.mapper = mapper;
            this.noteRepository = noteRepository;
            this.addNoteValidator = addNoteValidator;
            this.updateNoteValidator = updateNoteValidator;
        }

        public async Task<AppResponse<string>> CreateNoteAsync(AddNoteRequest request)
        {
            var validationResult = addNoteValidator.Validate(request);
            var response = new AppResponse<string>();

            if (validationResult.Errors.Count > 0)
            {
                var errorMessage = validationResult.Errors.FirstOrDefault().ErrorMessage;
                return ErrorResponse(errorMessage, 400, response);
            }

            var note = mapper.Map<Note>(request);
            var id = await noteRepository.CreateAsync(note);

            response.Result = id;
            return response;
        }

        public async Task<AppResponse> DeleteNoteAsync(DeleteNoteRequest request)
        {
            var response = new AppResponse();

            if (string.IsNullOrEmpty(request.Id))
                return ErrorResponse("Id can not be empty.", 400, response);

            var isSuccess = await noteRepository.RemoveAsync(request.Id);

            if (!isSuccess)
            {
                var errorMessage = "Note not found.";
                return ErrorResponse(errorMessage, 404, response);
            }

            return response;
        }

        public async Task<AppResponse<IEnumerable<NoteResponse>>> GetNotesAsync(GetNoteListRequest request)
        {
            var response = new AppResponse<IEnumerable<NoteResponse>>();

            if (string.IsNullOrEmpty(request.UserId))
                return ErrorResponse("UserId can not be empty.", 400, response);

            if (request.Limit > 49 || request.Limit < 1)
            {
                var errorMessage = "limit must between 1-50";
                return ErrorResponse(errorMessage, 400, response);
            }

            return await PaginateAsync(1, request.Limit);
        }

        public async Task<AppResponse<IEnumerable<NoteResponse>>> PaginateAsync(int page, int pageSize)
        {
            var response = new AppResponse<IEnumerable<NoteResponse>>();

            if (pageSize > 49)
            {
                var errorMessage = "pageSize can not be greater than 50";
                return ErrorResponse(errorMessage, 400, response);
            }

            var notes = await noteRepository.PaginateAsync(page, pageSize);

            response.Result = mapper.Map<IEnumerable<NoteResponse>>(notes);
            return response;
        }

        public async Task<AppResponse<bool>> UpdateNoteAsync(UpdateNoteRequest request)
        {
            var validationResult = updateNoteValidator.Validate(request);
            var response = new AppResponse<bool>();

            if (validationResult.Errors.Any())
            {
                var errorMessage = validationResult.Errors.FirstOrDefault().ErrorMessage;
                return ErrorResponse(errorMessage, 400, response);
            }

            var note = mapper.Map<Note>(request);
            var isUpdated = await noteRepository.UpdateAsync(request.Id, note);

            response.Result = isUpdated;
            return response;
        }
    }
}
