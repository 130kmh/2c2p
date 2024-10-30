using Microsoft.AspNetCore.Mvc;
using TransactionProcess.Core.Interfaces;

namespace TransactionProcess.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITransactionRepository _repository;
        private readonly ILogger<HomeController> _logger;

        public HomeController(
                    ITransactionRepository repository,
                    ILogger<HomeController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var transactions = await _repository.GetAllAsync();
            return View(transactions);
        }
    }
}
