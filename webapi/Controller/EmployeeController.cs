
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webapi.infra;
using webapi.models;
using webapi.ViewModel;

namespace webapi.Controller
{
    [ApiController]
    // [Authorize]
    [Route("api/v1/employee")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(IEmployeeRepository employeeRepository, ILogger<EmployeeController> logger)
        {
            _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
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
        public IActionResult Get(int pageNumber, int pageQuantity)
        {
            throw new Exception("Error de teste");
            List<Employee> employee = _employeeRepository.Get(pageNumber, pageQuantity);
            _logger.LogInformation("Employee with page number " + employee.Count);
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