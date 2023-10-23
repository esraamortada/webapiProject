using Anadolu.DTO;

using Anadolu.Models;
using Anadolu.Repository;
using Anadolu.Repository.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Anadolu.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CategoryController : ControllerBase
    {
        private readonly IUnitOfWork unit;
        private readonly IHostingEnvironment host;
        public CategoryController(IUnitOfWork _unit, IHostingEnvironment _host)
        {
            unit = _unit;
            host = _host;
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory([FromForm] CategoryDTO catDTO)
        {
            ResultDTO result = new ResultDTO();
            if (ModelState.IsValid)
            {
                Category category = new Category();
                category.Name = catDTO.Name;

                UploaderImage up = new UploaderImage(host);
                string fileName = await up.Uploade(catDTO.File);

                category.ImagePath = "http://localhost:5194/images/" + fileName;
                unit.CategoryRepository.Add(category);

                result.Data = catDTO;
                result.IsPassed = true;
                return Ok(result);
            }

            result.IsPassed = false;
            result.Data = "ModelState Is Invalid";
            return BadRequest(result);
        }

        [HttpPut("editcategory/{Id}")]
        public async Task<IActionResult> EditCategory(int Id, CategoryDTO categoryDTO)
        {
            ResultDTO resultDTO = new ResultDTO();
            if (ModelState.IsValid)
            {
                Category category = unit.CategoryRepository.GetById(Id, a => a.IsDeleted == false);
                if (category != null)
                {
                    category.Name = categoryDTO.Name;


                    UploaderImage up = new UploaderImage(host);
                    string fileName = await up.Uploade(categoryDTO.File);

                    category.ImagePath = "http://localhost:5194/images/" + fileName;

                    unit.CategoryRepository.Update(Id, category);

                    resultDTO.IsPassed = true;
                    resultDTO.Data = categoryDTO;
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


        [HttpDelete("deletecategory/{Id}")]
        public IActionResult DeleteCategory(int Id)
        {
            ResultDTO result = new ResultDTO();

            Category category = unit.CategoryRepository.GetById(Id, a => a.IsDeleted == false);
            if (category != null)
            {
                category.IsDeleted = true;

                unit.CategoryRepository.Update(Id, category);

                ReturnCategoryDTO returnCategoryDTO = new ReturnCategoryDTO();

                returnCategoryDTO.Name = category.Name;
                returnCategoryDTO.ImagePath = category.ImagePath;

                result.IsPassed = true;
                result.Data = returnCategoryDTO;
                return Ok(result);
            }
            result.IsPassed = false;
            result.Data = "No Category Exist With This Id";
            return BadRequest(result);
        }

        [HttpGet("categorydetails/{Id}")]
        public IActionResult GetDiscount(int Id)
        {
            ResultDTO result = new ResultDTO();
            Category category = unit.CategoryRepository.GetById(Id, a => a.IsDeleted == false);
            if (category != null)
            {
                ReturnCategoryDTO returnCategoryDTO = new ReturnCategoryDTO();

                returnCategoryDTO.Name = category.Name;
                returnCategoryDTO.ImagePath = category.ImagePath;

                result.IsPassed = true;
                result.Data = returnCategoryDTO;
                return Ok(result);
            }
            result.IsPassed = false;
            result.Data = "No Category Exist With This Id";
            return BadRequest(result);
        }

        [HttpGet("categories")]
        public IActionResult GetCategories()
        {
            ResultDTO result = new ResultDTO();
            List<Category> Categories = unit.CategoryRepository.GetAll(d => d.IsDeleted == false);
            if (Categories != null)
            {
                List<ReturnCategoryDTO> CategoryDTOs = new List<ReturnCategoryDTO>();
                for (int i = 0; i < Categories.Count; i++)
                {
                    ReturnCategoryDTO dto = new ReturnCategoryDTO();

                    dto.Name = Categories[i].Name;
                    dto.ImagePath = Categories[i].ImagePath;
                    dto.Id = Categories[i].Id;
                    CategoryDTOs.Add(dto);
                }
                result.IsPassed = true;
                result.Data = CategoryDTOs;
                return Ok(result);
            }
            result.IsPassed = false;
            result.Data = "No Category Exist";
            return BadRequest(result);
        }


        //public class CategoryController : ControllerBase
        //{
        //    private readonly IUnitOfWork unit;
        //    private readonly IHostingEnvironment host;
        //    public CategoryController(IUnitOfWork _unit, IHostingEnvironment _host)
        //    {
        //        unit = _unit;
        //        host = _host;
        //    }

        //    [HttpPost]
        //    public async Task<IActionResult> AddCategory([FromForm] CategoryDTO catDTO)
        //    {
        //        ResultDTO result = new ResultDTO();
        //        if (ModelState.IsValid)
        //        {
        //            Category category = new Category();
        //            category.Name = catDTO.Name;

        //            UploaderImage up = new UploaderImage(host);
        //            string fileName = await up.Uploade(catDTO.File);

        //            category.ImagePath = "http://localhost:5194/images/" + fileName;
        //            unit.CategoryRepository.Add(category);

        //            result.Data = catDTO;
        //            result.IsPassed = true;
        //            return Ok(result);
        //        }

        //        result.IsPassed = false;
        //        result.Data = "ModelState Is Invalid";
        //        return BadRequest(result);
        //    }

        //    [HttpPut("editcategory/{Id}")]
        //    public async Task<IActionResult> EditCategory(int Id, CategoryDTO categoryDTO)
        //    {
        //        ResultDTO resultDTO = new ResultDTO();
        //        if (ModelState.IsValid)
        //        {
        //            Category category = unit.CategoryRepository.GetById(Id, a => a.IsDeleted == false);
        //            if (category != null)
        //            {
        //                category.Name = categoryDTO.Name;


        //                UploaderImage up = new UploaderImage(host);
        //                string fileName = await up.Uploade(categoryDTO.File);

        //                category.ImagePath = "http://localhost:5194/images/" + fileName;

        //                unit.CategoryRepository.Update(Id, category);

        //                resultDTO.IsPassed = true;
        //                resultDTO.Data = categoryDTO;
        //                return Ok(resultDTO);
        //            }
        //            resultDTO.IsPassed = false;
        //            resultDTO.Data = "No Category Exist With This Id";
        //            return BadRequest(resultDTO);
        //        }
        //        resultDTO.IsPassed = false;
        //        resultDTO.Data = "ModelState Is Invalid";
        //        return BadRequest(resultDTO);
        //    }


        //    [HttpDelete("deletecategory/{Id}")]
        //    public IActionResult DeleteCategory(int Id)
        //    {
        //        ResultDTO result = new ResultDTO();

        //        Category category = unit.CategoryRepository.GetById(Id, a => a.IsDeleted == false);
        //        if (category != null)
        //        {
        //            category.IsDeleted = true;

        //            unit.CategoryRepository.Update(Id, category);

        //            ReturnCategoryDTO returnCategoryDTO = new ReturnCategoryDTO();

        //            returnCategoryDTO.Name = category.Name;
        //            returnCategoryDTO.ImagePath = category.ImagePath;

        //            result.IsPassed = true;
        //            result.Data = returnCategoryDTO;
        //            return Ok(result);
        //        }
        //        result.IsPassed = false;
        //        result.Data = "No Category Exist With This Id";
        //        return BadRequest(result);
        //    }

        //    [HttpGet("categorydetails/{Id}")]
        //    public IActionResult GetDiscount(int Id)
        //    {
        //        ResultDTO result = new ResultDTO();
        //        Category category = unit.CategoryRepository.GetById(Id, a => a.IsDeleted == false);
        //        if (category != null)
        //        {
        //            ReturnCategoryDTO returnCategoryDTO = new ReturnCategoryDTO();

        //            returnCategoryDTO.Name = category.Name;
        //            returnCategoryDTO.ImagePath = category.ImagePath;

        //            result.IsPassed = true;
        //            result.Data = returnCategoryDTO;
        //            return Ok(result);
        //        }
        //        result.IsPassed = false;
        //        result.Data = "No Category Exist With This Id";
        //        return BadRequest(result);
        //    }

        //    [HttpGet("categories")]
        //    public IActionResult GetCategories()
        //    {
        //        ResultDTO result = new ResultDTO();
        //        List<Category> Categories = unit.CategoryRepository.GetAll(d => d.IsDeleted == false);
        //        if (Categories != null)
        //        {
        //            List<ReturnCategoryDTO> CategoryDTOs = new List<ReturnCategoryDTO>();
        //            for (int i = 0; i < Categories.Count; i++)
        //            {
        //                ReturnCategoryDTO dto = new ReturnCategoryDTO();

        //                dto.Name = Categories[i].Name;
        //                dto.ImagePath = Categories[i].ImagePath;
        //                dto.Id = Categories[i].Id;
        //                CategoryDTOs.Add(dto);
        //            }
        //            result.IsPassed = true;
        //            result.Data = CategoryDTOs;
        //            return Ok(result);
        //        }
        //        result.IsPassed = false;
        //        result.Data = "No Category Exist";
        //        return BadRequest(result);
        //    }
    }
}
