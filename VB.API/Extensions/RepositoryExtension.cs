using VB.Data.Repositories;

namespace VB.API.Extensions
{
    public static class RepositoryExtension
    {
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            services.AddScoped<IFilmRequestRepository, FilmRequestRepository>();
            return services;
        }
    }
}
