using CE_TaskTest.BackEnd.Services.Dtos.Request;
using CE_TaskTest.BackEnd.Services.Validations;
using FluentValidation;
using System.Reflection;

namespace CE_TaskTest.BackEnd.Services.Extensions
{
    public static class Services
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(configuration);

            // Mapper
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            // Validations
            services.AddScoped<IValidator<TareaRequestDto>, TareaValidation>();
            services.AddScoped<IValidator<EstimacionRequestDto>, EstimacionValidation>();

            // Services
            services.AddScoped<TareaService>();

            return services;
        }
    }
}
