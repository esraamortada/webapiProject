using Anadolu.DTO;
using Anadolu.Models;
using Anadolu.Repository.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;


namespace Anadolu.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoryController : ControllerBase
    {
        private readonly IUnitOfWork unit;
        private readonly IHostingEnvironment host;
        public SubCategoryController(IUnitOfWork _unit, IHostingEnvironment _host)
        {
            unit = _unit;
            host = _host;
        }
        [HttpPost]
        public async Task<IActionResult> AddSubCategory([FromForm] SubCategoryDTO subCatDTO)
        {
            ResultDTO result = new ResultDTO();
            if (ModelState.IsValid)
            {
                SubCategory subCategory = new SubCategory();
                subCategory.Name = subCatDTO.Name;

                // اعملي فانكشن تجيب الكاتيجورس من الريبوزيتوري وتحطي ال Id
                // في السطر اللي تحت ده

                subCategory.CategoryId = subCatDTO.CategoryId;

                UploaderImage up = new UploaderImage(host);
                string fileName = await up.Uploade(subCatDTO.File);

                subCategory.ImagePath = "http://localhost:5194/images/" + fileName;
                unit.SubCategoryRepository.Add(subCategory);

                result.Data = subCatDTO;
                result.IsPassed = true;
                return Ok(result);
            }

            result.IsPassed = false;
            result.Data = "ModelState Is Invalid";
            return BadRequest(result);
        }

        [HttpPut("editsubcategory/{Id}")]
        public async Task<IActionResult> EditSubCategory(int Id, SubCategoryDTO subCategoryDTO)
        {
            ResultDTO resultDTO = new ResultDTO();
            if (ModelState.IsValid)
            {
                SubCategory subCategory = unit.SubCategoryRepository.GetById(Id, a => a.IsDeleted == false);
                if (subCategory != null)
                {
                    subCategory.Name = subCategoryDTO.Name;
                    subCategory.CategoryId = subCategoryDTO.CategoryId;

                    UploaderImage up = new UploaderImage(host);
                    string fileName = await up.Uploade(subCategoryDTO.File);

                    subCategory.ImagePath = "http://localhost:5194/images/" + fileName;

                    unit.SubCategoryRepository.Update(Id, subCategory);

                    resultDTO.IsPassed = true;
                    resultDTO.Data = subCategoryDTO;
                    return Ok(resultDTO);
                }
                resultDTO.IsPassed = false;
                resultDTO.Data = "No Category Exist With This Id";
                return BadRequest(resultDTO);
            }
            resultDTO.IsPassed = false;
            resultDTO.Data = "ModelState Is Invalid";
            return BadRequest(resultDTO);
        }

        [HttpDelete("deletesubcategory/{Id}")]
        public IActionResult DeleteSubCategory(int Id)
        {
            ResultDTO result = new ResultDTO();

            SubCategory subCategory = unit.SubCategoryRepository.GetById(Id, a => a.IsDeleted == false);
            if (subCategory != null)
            {
                subCategory.IsDeleted = true;

                unit.SubCategoryRepository.Update(Id, subCategory);

                ReturnSubCategoryDTO returnSubCategoryDTO = new ReturnSubCategoryDTO();

                returnSubCategoryDTO.Name = subCategory.Name;
                returnSubCategoryDTO.ImagePath = subCategory.ImagePath;
                returnSubCategoryDTO.CategoryId = subCategory.CategoryId;

                result.IsPassed = true;
                result.Data = returnSubCategoryDTO;
                return Ok(result);
            }
            result.IsPassed = false;
            result.Data = "No SubCategory Exist With This Id";
            return BadRequest(result);
        }

        [HttpGet("subcategorydetails/{Id}")]
        public IActionResult GetSubCategory(int Id)
        {
            ResultDTO result = new ResultDTO();
            SubCategory subCategory = unit.SubCategoryRepository.GetById(Id, a => a.IsDeleted == false);
            if (subCategory != null)
            {
                ReturnSubCategoryDTO returnSubCategoryDTO = new ReturnSubCategoryDTO();

                returnSubCategoryDTO.Name = subCategory.Name;
                returnSubCategoryDTO.ImagePath = subCategory.ImagePath;
                returnSubCategoryDTO.CategoryId = subCategory.CategoryId;
                result.IsPassed = true;
                result.Data = returnSubCategoryDTO;
                return Ok(result);
            }
            result.IsPassed = false;
            result.Data = "No SubCategory Exist With This Id";
            return BadRequest(result);
        }

        [HttpGet("subcategories")]
        public IActionResult GetSubCategories()
        {
            ResultDTO result = new ResultDTO();
            List<SubCategory> subCategories = unit.SubCategoryRepository.GetAll(d => d.IsDeleted == false);
            if (subCategories != null)
            {
                List<ReturnSubCategoryDTO> subCategoryDTOs = new List<ReturnSubCategoryDTO>();
                for (int i = 0; i < subCategories.Count; i++)
                {
                    ReturnSubCategoryDTO dto = new ReturnSubCategoryDTO();

                    dto.Id = subCategories[i].Id;
                    dto.Name = subCategories[i].Name;
                    dto.ImagePath = subCategories[i].ImagePath;
                    dto.CategoryId = subCategories[i].CategoryId;

                    subCategoryDTOs.Add(dto);
                }
                result.IsPassed = true;
                result.Data = subCategoryDTOs;
                return Ok(result);
            }
            result.IsPassed = false;
            result.Data = "No SubCategory Exist";
            return BadRequest(result);
        }
    }
}
