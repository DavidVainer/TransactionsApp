namespace TransactionsApp.API.IoC
{
    /// <summary>
    /// Extension methods for configuring services and dependencies in the IoC container.
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Configures services and dependencies for the application in the IoC container.
        /// </summary>
        /// <param name="services">The collection of services.</param>
        /// <param name="configuration">The configuration instance.</param>
        public static void ConfigureDI(this IServiceCollection services, IConfiguration configuration)
        {
        }
    }
}
