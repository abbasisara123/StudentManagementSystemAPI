using AutoMapper;
using Azure.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.API.DTOs.Department;
using StudentManagement.API.Entities;
using StudentManagement.API.Interfaces;

namespace StudentManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public DepartmentController(IDepartmentRepository departmentRepository, IMapper mapper)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllDepartment()
        {
            var departments = await _departmentRepository.GetAllAsync();
            //Entity to DTO mapping
            var response = _mapper.Map<List<DepartmentDto>>(departments);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDepartmentById(int id)
        {
            var department = await _departmentRepository.GetByIdAsync(id);
            if (department == null)
            {
                return NotFound();
            }
            var response = _mapper.Map<DepartmentDto>(department);
            return Ok(response);
        }

        [HttpPost("")]
        public async Task<IActionResult> CreateDepartment(CreateDepartmentDto dto)
        {
            // DTO to Entity mapping
            var department = _mapper.Map<Department>(dto);
            var createDepartment = await _departmentRepository.AddAsync(department);
            //Entity to DTO mapping
            var response = _mapper.Map<DepartmentDto>(createDepartment);
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task <IActionResult> UpdateDepartment(int id,UpdateDepartmentDto updateDepartment)
        {
            var department = await _departmentRepository.GetByIdAsync(id);
            if (department == null)
            {
                return NotFound();
            }
            var updateDep = _mapper.Map(updateDepartment,department);
            await _departmentRepository.UpdateAsync(updateDep);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            await _departmentRepository.DeleteAsync(id);
            return NoContent();
        }


    }
}
