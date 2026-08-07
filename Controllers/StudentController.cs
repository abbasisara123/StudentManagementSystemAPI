using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.API.DTOs.Student;
using StudentManagement.API.Entities;
using StudentManagement.API.Interfaces;

namespace StudentManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;
        public StudentController(IStudentRepository studentRepository, IMapper mapper) 
        { 
            _studentRepository = studentRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStudents()
        {
            var student = await _studentRepository.GetAllAsync();
            var response = _mapper.Map<List<StudentDto>>(student);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudentById(int id)
        {
            var student=await _studentRepository.GetByIdAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            var response = _mapper.Map<StudentDto>(student);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddStudent(CreateStudentDto newStudent)
        {
            var student = _mapper.Map<Student>(newStudent);
            var record= await _studentRepository.AddAsync(student);
            var response = _mapper.Map<StudentDto>(record);
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id,UpdateStudentDto updateStudent)
        {
            var student=await _studentRepository.GetByIdAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            var updateStd=_mapper.Map(updateStudent, student);
            await _studentRepository.UpdateAsync(updateStd);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            await _studentRepository.DeleteAsync(id);
            return NoContent();
        }

    }
}
