using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevNots.Application.Contracts;
using DevNots.Application.Contracts.Note;
using DevNots.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DevNots.RestApi.Controllers
{
    [Route("api/notes")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly NoteService _noteService;

        public NoteController(NoteService noteService)
        {
            _noteService = noteService;
        }
        /// <summary>
        /// Create a note 
        /// </summary>
        /// <param name="note">Note object</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateNote([FromBody] NoteDto note)
        {
            var response = await _noteService.CreateNoteAsync(note);

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
        public async Task<IActionResult> DeleteNote([FromBody] DeleteNoteDto note)
        {
            var response = await _noteService.DeleteNoteAsync(note);

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
        public async Task<IActionResult> GetNotes([FromQuery] int limit = 20)
        {
            var response = await _noteService.GetNotesAsync(new NoteListDto()
            {
                Limit = limit,
            });

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
        public async Task<IActionResult> UpdateNote([FromBody]NoteDto note)
        {
            var response = await _noteService.UpdateNoteAsync(note);

            if (response.Error != null)
                return StatusCode(response.Error.StatusCode, response.Error.Message);

            var isUpdated = response.Result;

            if (!isUpdated)
                return NotFound(new { message = "404 not found."});

            return Ok(new { message = "note updated." });
        }
    }
}
