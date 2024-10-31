using Microsoft.AspNetCore.Mvc;
using TransactionProcess.Core.Interfaces;
using TransactionProcess.Infrastructure.Services;
using TransactionProcess.Web.Models;

namespace TransactionProcess.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITransactionRepository _repository;
        private readonly IFileProcessingService _fileProcessingService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(
                    ILogger<HomeController> logger,
                    ITransactionRepository repository,
                    IFileProcessingService fileProcessingService)
        {
            _repository = repository;
            _logger = logger;
            _fileProcessingService = fileProcessingService;
        }

        public async Task<IActionResult> Index()
        {
            var transactions = await _repository.GetAllAsync();
            return View(transactions);
        }

        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Upload(FileUploadViewModel model)
        {
            if (ModelState.IsValid && model.IsValidFileType)
            {
                using var stream = model.File.OpenReadStream();
                var result = await _fileProcessingService.ProcessFileAsync(stream, model.File.FileName, User.Identity.Name ?? "n/a");

                if (result)
                {
                    TempData["SuccessMessage"] = "File processed successfully.";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "Failed to process the file. Please check the file format and try again.");
                }
            }
            else if (!model.IsValidFileType)
            {
                ModelState.AddModelError("File", "The file must be either CSV or XML format.");
            }

            return View(model);
        }

        [HttpGet("currency/{currencyCode}")]
        public async Task<IActionResult> GetByCurrency(string currencyCode)
        {
            var transactions = await _repository.GetByCurrencyAsync(currencyCode);
            return Json(transactions);
        }

        [HttpGet, Route("date-range/{start:datetime}/{end:datetime}")]
        public async Task<IActionResult> GetByDateRange(DateTime start,[FromRoute] DateTime end)
        {
            var transactions = await _repository.GetByDateRangeAsync(start, end);
            return Json(transactions);
        }

        [HttpGet("status/{status}")]
        public async Task<IActionResult> GetByStatus(string status)
        {
            var transactions = await _repository.GetByStatusAsync(status);
            return Json(transactions);
        }
    }
}
