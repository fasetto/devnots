using System.Threading.Tasks;
using DevNots.Application.Contracts;
using DevNots.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace DevNots.RestApi.Controllers
{
    [Route("api/notes")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly NoteService noteService;
        public NoteController(NoteService noteService)
        {
            this.noteService = noteService;
        }

        /// <summary>
        /// Create a note
        /// </summary>
        /// <param name="note">Note object</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(201)]
        public async Task<IActionResult> CreateNote([FromBody] AddNoteRequest request)
        {
            var response = await noteService.CreateNoteAsync(request);

            if (response.Error != null)
                return StatusCode(response.Error.StatusCode, response.Error);

            return StatusCode(201, new { id = response.Result });
        }

        /// <summary>
        /// Delete the note
        /// </summary>
        /// <param name="note">DeleteNoteDto object</param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(200)]
        public async Task<IActionResult> DeleteNote([FromBody] DeleteNoteRequest note)
        {
            var response = await noteService.DeleteNoteAsync(note);

            if (response.Error != null)
                return StatusCode(response.Error.StatusCode, response.Error);

            return Ok(new { message = "Note deleted." });
        }

        /// <summary>
        /// Get list of Notes
        /// </summary>
        /// <param name="limit">Set the note limit  ( Default limit = 20 )</param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetNotes([FromBody] GetNoteListRequest request)
        {
            var response = await noteService.GetNotesAsync(request);

            if (response.Error != null)
                return StatusCode(response.Error.StatusCode, response.Error.Message);

            return Ok(response.Result);
        }

        /// <summary>
        /// Update the note
        /// </summary>
        /// <param name="note">Note Object</param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> UpdateNote([FromBody] UpdateNoteRequest request)
        {
            var response = await noteService.UpdateNoteAsync(request);

            if (response.Error != null)
                return StatusCode(response.Error.StatusCode, response.Error.Message);

            var isUpdated = response.Result;

            if (!isUpdated)
                return NotFound(new { message = "404 not found."});

            return Ok(new { message = "note updated." });
        }
    }
}
