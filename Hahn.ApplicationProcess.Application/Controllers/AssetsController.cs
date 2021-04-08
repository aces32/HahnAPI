using Hahn.ApplicationProcess.February2021.Data.Interfaces;
using Hahn.ApplicationProcess.February2021.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.Web.Controllers
{
    /// <summary>
    /// Api to get asset records
    /// </summary>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Route("api/[controller]")]
    [ApiController]
    public class AssetsController : ControllerBase
    {
        private readonly ILogger<AssetsController> logger;
        private readonly IUnitOfWork unitOfWork;

        public AssetsController(ILogger<AssetsController> logger,
            IUnitOfWork unitOfWork)
        {
            this.logger = logger;
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Get Assets based on specified ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<DefaultResponse<IEnumerable<Asset>>> Get(int id)
        {
            try
            {
                var asset = unitOfWork.AssetRepository.FInd(asset => asset.ID == id);
                if (asset == null)
                {
                    return NotFound(new DefaultResponse<IEnumerable<Asset>>
                    { 
                        Message = "No Asset found based on the ID supplied"
                    });
                }
                else if (asset.Count() == 0)
                {
                    return NotFound(new DefaultResponse<IEnumerable<Asset>>
                    {
                        Message = "No Asset found based on the ID supplied"
                    });
                }

                return Ok(new DefaultResponse<IEnumerable<Asset>>
                {
                    Message = "Success",
                    Data = asset
                });
            }
            catch (Exception ex)
            {
                logger.LogError($"{ex}", $"Error calling Get Api {nameof(Get)}");
                return StatusCode(500, new DefaultResponse<IEnumerable<Asset>>
                {
                    Message = "An error occured getting asset"
                });
            }
        }

        /// <summary>
        /// Add new assets
        /// </summary>
        /// <param name="asset"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<DefaultResponse<Asset>> Post([FromBody] Asset asset)
        {
            try
            {
                unitOfWork.AssetRepository.Add(asset);
                unitOfWork.AssetRepository.SaveChanges();

                return CreatedAtAction(
                    nameof(Get),
                    new { id = asset.ID },
                    new DefaultResponse<Asset>
                    {
                        Message = "Success",
                        Data = asset
                    });

            }
            catch (Exception ex)
            {
                logger.LogError($"{ex}", $"Error calling Post Api {nameof(Post)}");
                return StatusCode(500, new DefaultResponse<string>
                {
                    Message = "An error occured Adding asset"
                });
            }
        }

        /// <summary>
        /// Update new asset data
        /// </summary>
        /// <param name="id"></param>
        /// <param name="asset"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public ActionResult<DefaultResponse<string>> Put(int id, [FromBody] Asset asset)
        {
            try
            {
                if (id != asset.ID)
                {
                    return BadRequest(new DefaultResponse<Asset>
                    {
                        Message = "Invalid asset ID Passed"
                    });
                }

                var exisitingAsset = unitOfWork.AssetRepository.FInd(asset => asset.ID == id);
                ;
                if (exisitingAsset == null)
                {
                    return NotFound(new DefaultResponse<Asset>
                    {
                        Message = "No Asset found based on the ID supplied"
                    });
                }
                else if (exisitingAsset.Count() == 0)
                {
                    return NotFound(new DefaultResponse<Asset>
                    {
                        Message = "No Asset found based on the ID supplied"
                    });
                }

                unitOfWork.AssetRepository.Update(asset);
                unitOfWork.SaveChanges();

                return StatusCode(204, new DefaultResponse<Asset>
                {
                    Message = "Assets updated successfully"
                });


            }
            catch (Exception ex)
            {
                logger.LogError($"{ex}", $"Error calling updating Api {nameof(Put)}");
                return StatusCode(500, new DefaultResponse<Asset>
                {
                    Message = "An error occured updating asset"
                });
            }
        }


        /// <summary>
        /// Delete assets based on the specified ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult<DefaultResponse<string>> Delete(int id)
        {
            try
            {
                var asset = unitOfWork.AssetRepository.FInd(asset => asset.ID == id);
                if (asset == null)
                {
                    return NotFound(new DefaultResponse<string>
                    {
                        Message = "No Asset found based on the ID supplied"
                    });
                }
                else if (asset.Count() == 0)
                {
                    return NotFound(new DefaultResponse<IEnumerable<Asset>>
                    {
                        Message = "No Asset found based on the ID supplied"
                    });
                }

                unitOfWork.AssetRepository.Delete(asset.FirstOrDefault());
                unitOfWork.SaveChanges();
                return NoContent();
            }
            catch (Exception ex)
            {
                logger.LogError($"{ex}", $"Error calling Delete Api {nameof(Delete)}");
                return StatusCode(500, new DefaultResponse<Asset>
                {
                    Message = "An error occured deleting asset"
                });
            }
        }
    }
}
