using Microsoft.AspNetCore.Mvc;
using TransactionsApp.Application.Models.Dto;
using TransactionsApp.Application.Services.Managers;

namespace TransactionsApp.API.Controllers
{
    /// <summary>
    /// Handles transactions related requests.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly ILogger<TransactionsController> _logger;
        private readonly ITransactionManager _manager;

        public TransactionsController(ILogger<TransactionsController> logger, ITransactionManager manager)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _manager = manager ?? throw new ArgumentNullException(nameof(manager));
        }

        /// <summary>
        /// Gets all transactions.
        /// </summary>
        /// <returns>A collection of all transactions.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var transactions = await _manager.GetAllTransactionsAsync();
            return Ok(transactions);
        }

        /// <summary>
        /// Gets a transaction by its unique identifier.
        /// </summary>
        /// <param name="id">The transaction unique identifier.</param>
        /// <returns>The fetched transaction.</returns>

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var transaction = await _manager.GetTransactionByIdAsync(id);
            return Ok(transaction);
        }

        /// <summary>
        /// Process a new transaction.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Process([FromBody]AddTransactionDto dto)
        {
            await _manager.ProcessTransactionAsync(dto);
            return Ok();
        }

        /// <summary>
        /// Updates a transaction by its unique identifier.
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody]UpdateTransactionDto dto)
        {
            await _manager.UpdateTransactionAsync(dto);
            return Ok();
        }

        /// <summary>
        /// Deletes a transaction by its unique identifier.
        /// </summary>
        /// <param name="id">The transaction unique identifier.</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _manager.DeleteTransactionAsync(id);
            return Ok();
        }
    }
}
