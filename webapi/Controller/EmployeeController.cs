
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webapi.infra;
using webapi.models;
using webapi.ViewModel;

namespace webapi.Controller
{
    [ApiController]
    [Authorize]
    [Route("api/v1/employee")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            // Dependency Injection
            _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
        }

        [HttpPost]
        public IActionResult Add([FromForm] EmployeeViewModel employeeViewModel)
        {

            string filePath = Path.Combine("storage", employeeViewModel.Photo.FileName);
            using Stream fileStream = new FileStream(filePath, FileMode.Create);
            employeeViewModel.Photo.CopyTo(fileStream);
            var employee = new Employee(employeeViewModel.Name, employeeViewModel.Age, filePath);
            _employeeRepository.Add(employee);
            return Ok();
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<Employee> employee = _employeeRepository.Get();
            return Ok(employee);
        }

        // Rota para baixar a imagem
        [HttpPost]
        [Route("{id}/download")]
        public IActionResult DownloadPhoto(int id)
        {
            var employee = _employeeRepository.GetEmployeeById(id);

            //Verifica se há um usuario no banco e se possui uma foto para download
            if (employee == null || string.IsNullOrEmpty(employee.photo))
            {
                return NotFound();
            }

            string contentType = GetContentType(Path.GetExtension(employee.photo));

            var dataBytes = System.IO.File.ReadAllBytes(employee.photo);

            return File(dataBytes, contentType);

        }

        private string GetContentType(string fileExtension)
        {
            switch (fileExtension.ToLower())
            {
                case ".jpg":
                case ".jpeg":
                    return "image/jpeg";
                case ".png":
                    return "image/png";
                case ".pdf":
                    return "application/pdf";
                case ".webp":
                    return "image/webp";
                default:
                    return "application/octet-stream"; // default to binary data if MIME type not recognized
            }
        }
    }
}