using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewAvalon.Storage.Boundary.Files.Commands.DeleteFile;
using NewAvalon.Storage.Boundary.Files.Commands.UploadFile;
using NewAvalon.Storage.Boundary.Files.Queries.DownloadFile;
using NewAvalon.Storage.Presentation.Abstractions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NewAvalon.Storage.Presentation.Controllers
{
    /// <summary>
    /// Represents the files controller.
    /// </summary>
    public sealed class FilesController : StorageModuleController
    {/// <summary>
     /// Uploads the specified file to storage.
     /// </summary>
     /// <param name="file">The file to be uploaded.</param>
     /// <param name="cancellationToken">The cancellation token.</param>
     /// <returns>The upload file response containing the file identifier and details.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(UploadFileResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> UploadFile(IFormFile file, CancellationToken cancellationToken)
        {
            var command = new UploadFileCommand(file);

            UploadFileResponse uploadFileResponse = await Sender.Send(command, cancellationToken);

            return Ok(uploadFileResponse);
        }

        /// <summary>
        /// Downloads file.
        /// </summary>
        /// <param name="fileId">The file identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Download file response.</returns>
        [HttpGet("{fileId:guid}")]
        [ProducesResponseType(typeof(DownloadFileResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Download(Guid fileId, CancellationToken cancellationToken)
        {
            var command = new DownloadFileQuery(fileId);

            DownloadFileResponse response = await Sender.Send(command, cancellationToken);

            return Ok(response);
        }

        /// <summary>
        /// Deletes the file with the specified identifier from storage.
        /// </summary>
        /// <param name="fileId">The file identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>204 - No Content.</returns>
        [HttpDelete("{fileId:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteFile(Guid fileId, CancellationToken cancellationToken)
        {
            var command = new DeleteFileCommand(fileId);

            await Sender.Send(command, cancellationToken);

            return NoContent();
        }
    }
}
