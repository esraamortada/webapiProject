using Anadolu.DTO;
using Anadolu.Models;
using Anadolu.Repository.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Anadolu.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration config;
        private readonly IUnitOfWork unit;
        private readonly IHostingEnvironment host;

        public AdminController(UserManager<ApplicationUser> userManager, IConfiguration config,
            IUnitOfWork _unit, IHostingEnvironment _host)
        {
            this.userManager = userManager;
            this.config = config;
            unit = _unit;
            host = _host;
        }
        [HttpPost("adminregister")]
        public async Task<IActionResult> RegisterAsAdmin(RegisterDTO userDTO)
        {
            ResultDTO resultDTO = new ResultDTO();
            if (ModelState.IsValid)
            {
                ApplicationUser userModel = new ApplicationUser();
                userModel.FirstName = userDTO.FirstName;
                userModel.LastName = userDTO.LastName;
                userModel.Email = userDTO.Email;
                userModel.UserName = userDTO.UserName;
                userModel.PhoneNumber = userDTO.PhoneNumber;
                userModel.BirthDate = userDTO.BirthDate;
                userModel.Gender = userDTO.Gender;
                
                IdentityResult result = await userManager.CreateAsync(userModel, userDTO.Password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(userModel, "Admin");
                    Admin admin = new Admin();

                    //Another Action
                    ///////////////////////////////////////////////
                    admin.ApplicationUserId = userModel.Id;
                    //admin.ImagePath = userModel.ImagePath;
                    admin.Phone = userModel.PhoneNumber;
                    admin.LastName = userModel.LastName;
                    admin.FirstName = userModel.FirstName;
                    admin.BirthDate = userModel.BirthDate;

                    unit.AdminRepository.Add(admin);
                    resultDTO.Data = userModel;
                    resultDTO.IsPassed = true;
                    return Ok(resultDTO);
                }
                else
                {
                    resultDTO.Data = userModel;
                    resultDTO.IsPassed = false;
                    return Ok(resultDTO);
                }
            }
            resultDTO.IsPassed = false;
            resultDTO.Data = ModelState;
            return BadRequest(resultDTO);
        }

        [HttpGet("getalladmins")]
        public IActionResult GetAllAdmins()
        {
            ResultDTO result = new ResultDTO();
            List<Admin> admins = unit.AdminRepository.GetAll(a => a.IsDeleted == false);
            if (admins != null)
            {
                List<ApplicationUser> users = new List<ApplicationUser>();
                for(int i=0; i<admins.Count; i++)
                {
                    ApplicationUser user = unit.ApplicationUserRepository
                        .GetByIdString(admins[i].ApplicationUserId,a=>a.IsDeleted==false);
                    users.Add(user);
                }
                List<ReturnAdminDTO> adminsDTO = new List<ReturnAdminDTO>();
                for (int i = 0; i < admins.Count; i++)
                {
                    ReturnAdminDTO dto = new ReturnAdminDTO();
                    dto.Phone = admins[i].Phone;
                    dto.BirthDate = admins[i].BirthDate;
                    dto.Id = admins[i].ApplicationUserId;
                    dto.Email = users[i].Email;
                    dto.Name = admins[i].FirstName + " " + admins[i].LastName;

                    adminsDTO.Add(dto);
                }
                result.IsPassed = true;
                result.Data = adminsDTO;
                return Ok(result);
            }
            result.IsPassed = false;
            result.Data = "No Admins Exist";
            return BadRequest(result);
        }
    }
}
//string fileName = string.Empty;
//if (userDTO.File == null || userDTO.File.Length == 0)
//{
//    return BadRequest(new { IsPassed = false, Data = "No File Selected" });
//}
//string myUpload = Path.Combine(host.WebRootPath, "images");
//fileName = userDTO.File.FileName;
//string fullPath = Path.Combine(myUpload, fileName);
//using (var stream = new FileStream(fullPath, FileMode.Create))
//{
//    await userDTO.File.CopyToAsync(stream);
//}

//userModel.ImagePath = "http://localhost:5194/images/" + fileName;