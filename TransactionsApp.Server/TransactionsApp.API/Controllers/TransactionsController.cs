using Microsoft.AspNetCore.Mvc;

namespace TransactionsApp.API.Controllers
{
    /// <summary>
    /// Handles transactions related requests.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly ILogger<TransactionsController> _logger;

        public TransactionsController(ILogger<TransactionsController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Gets all transactions.
        /// </summary>
        /// <returns>A collection of all transactions.</returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets a transaction by its unique identifier.
        /// </summary>
        /// <param name="id">The transaction unique identifier.</param>
        /// <returns>The fetched transaction.</returns>

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates a new transaction.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Updates a transaction by its unique identifier.
        /// </summary>
        /// <param name="id">The transaction unique identifier.</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Update(Guid id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes a transaction by its unique identifier.
        /// </summary>
        /// <param name="id">The transaction unique identifier.</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
