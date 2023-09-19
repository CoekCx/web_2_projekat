using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewAvalon.Storage.Boundary.Images.Commands.DeleteImage;
using NewAvalon.Storage.Boundary.Images.Commands.UploadImage;
using NewAvalon.Storage.Presentation.Abstractions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NewAvalon.Storage.Presentation.Controllers
{
    /// <summary>
    /// Represents the images controller.
    /// </summary>
    public sealed class ImagesController : StorageModuleController
    {
        /// <summary>
        /// Uploads the specified image to storage.
        /// </summary>
        /// <param name="imageFile">The image file to be uploaded.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The upload image response containing the image identifier and URL.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(UploadImageResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> UploadImage(IFormFile imageFile, CancellationToken cancellationToken)
        {
            var command = new UploadImageCommand(imageFile);

            UploadImageResponse uploadImageResponse = await Sender.Send(command, cancellationToken);

            return Ok(uploadImageResponse);
        }

        /// <summary>
        /// Deletes the image with the specified identifier from storage.
        /// </summary>
        /// <param name="imageId">The image identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>204 - No Content.</returns>
        [HttpDelete("{imageId:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteImage(Guid imageId, CancellationToken cancellationToken)
        {
            var command = new DeleteImageCommand(imageId);

            await Sender.Send(command, cancellationToken);

            return NoContent();
        }
    }
}
