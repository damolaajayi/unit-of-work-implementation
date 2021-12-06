using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnitOfWorkTutorial.Configuration;
using UnitOfWorkTutorial.Models;

namespace UnitOfWorkTutorial.Controllers
{
    public class EmployeeController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = await _unitOfWork.CompanyEmployee.All();
            return Ok(users);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee(Guid id)
        {
            var item = await _unitOfWork.CompanyEmployee.GetById(id);

            if (item == null)
                return NotFound();

            return Ok(item);
        }


        [HttpPost]
        public async Task<IActionResult> CreateEmployee(CompanyEmployee employee)
        {
            if (ModelState.IsValid)
            {
                employee.Id = Guid.NewGuid();

                await _unitOfWork.CompanyEmployee.Add(employee);
                await _unitOfWork.CompleteAsync();

                return CreatedAtAction("GetEmployee", new { employee.Id }, employee);
            }

            return new JsonResult("Something went wrong") { StatusCode = 500 };
        }

        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            var item = await _unitOfWork.CompanyEmployee.GetById(id);

            if (item == null)
                return BadRequest();

            await _unitOfWork.CompanyEmployee.Delete(id);
            await _unitOfWork.CompleteAsync();

            return Ok(item);
        }
    }
}
