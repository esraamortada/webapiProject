using Anadolu.DTO;
using Anadolu.Models;
using Anadolu.Repository.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Anadolu.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactUsController : ControllerBase
    {
        private readonly IUnitOfWork unit;

        public ContactUsController(IUnitOfWork _unit)
        {
            unit = _unit;
        }

        [HttpGet]
        public IActionResult DisplayContactUS()
        {
            ResultDTO result = new ResultDTO();
            List<ContactUs> ContactUsList = unit.ContactUsRepository.GetAll(a => a.IsDeleted == false);
            if (ContactUsList != null)
            {
                List<ContactUsDTO> ContactUsDTOList = new List<ContactUsDTO>();
                foreach (ContactUs contactUs in ContactUsList)
                {
                    ContactUsDTO contactUsDTO = new ContactUsDTO();
                    contactUsDTO.Message = contactUs.Message;
                    contactUsDTO.Email = contactUs.Email;
                    contactUsDTO.Name = contactUs.Name;

                    ContactUsDTOList.Add(contactUsDTO);
                }
                result.Data = ContactUsList;
                result.IsPassed = true;
                return Ok(result);
            }

            result.IsPassed = false;
            result.Data = "No ContactUs Exist";
            return BadRequest(result);
        }

        [HttpDelete("deletecontactus/{Id}")]
        public IActionResult ProblemSolved(int Id)
        {
            ResultDTO result = new ResultDTO();
            if (ModelState.IsValid)
            {
                ContactUs contact = unit.ContactUsRepository.GetById(Id, c => c.IsDeleted == false);
                if (contact != null)
                {
                    contact.IsDeleted = true;
                    unit.ContactUsRepository.Update(contact.Id, contact);

                    ContactUsDTO contactUsDTO = new ContactUsDTO();
                    contactUsDTO.Name = contact.Name;
                    contactUsDTO.Email = contact.Email;
                    contactUsDTO.Message = contactUsDTO.Message;
                    result.Data = contactUsDTO;
                    result.IsPassed = true;
                    return Ok(result);
                }
                result.Data = "No ContactUs With This Id";
                result.IsPassed = false;
                return BadRequest(result);
            }
            result.IsPassed = false;
            result.Data = "ModelState Is Invalid";
            return BadRequest(result);
        }
        [HttpPost]
        public IActionResult AddMessage(ContactUsDTO contactUsDTO)
        {
            ResultDTO result = new ResultDTO();
            if (ModelState.IsValid)
            {
                ContactUs contactUs = new ContactUs();
                contactUs.Name = contactUsDTO.Name;
                contactUs.Email = contactUsDTO.Email;
                contactUs.Message = contactUsDTO.Message;

                unit.ContactUsRepository.Add(contactUs);

                result.IsPassed = true;
                result.Data = contactUsDTO;
                return Ok(result);
            }
            result.IsPassed= false;
            result.Data = "ModelState Is Invalid";
            return BadRequest(result);
        }
    }
}
