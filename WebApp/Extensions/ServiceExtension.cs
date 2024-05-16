using Application.DataContext;
using Application.MappingServices.MapInitializers;
using Application.Repository.CandidateApplicationServices;
using Application.Repository.GenericRepo.IRepositoryBase;
using Application.Repository.GenericRepo.RepositoryBase;
using Application.Repository.ProgramServices;
using Infrastructure.ICandidateApplicationServices;
using Infrastructure.IProgramServices;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace WebApp.Extensions;

public static class ServiceExtension
{
    public static void ResolveDependencyInjectionContainer(this IServiceCollection services)
    {
        services.AddScoped<IProgramQueryService, ProgramQueryService>();
        services.AddScoped<IProgramCommandService, ProgramCommandService>();
        services.AddScoped<ICandidateAppCommandServices, CandidateAppCommandServices>();
        services.AddScoped<ICandidateAppQueryServices, CandidateAppQueryServices>();
        services.AddScoped(typeof(ICommandRepoBase<>), typeof(CommandRepoBase<>));
        services.AddScoped(typeof(IQueryRepoBase<>), typeof(QueryRepoBase<>));
    }
    public static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ProgramAppDbContext>(options =>
            options.UseCosmos(
                configuration["CosmosConfig:accountEndpoint"], // Cosmos DB account endpoint
                configuration["CosmosConfig:accountKey"],      // Cosmos DB account key
                configuration["CosmosConfig:databaseName"]));  // Cosmos DB database name
    }
    public static void ConfigureController(this IServiceCollection services)
    {
        services.AddControllers()
            .AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly)
                    .AddJsonOptions(options =>
                    {
                        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                    }).AddXmlDataContractSerializerFormatters();
    }
    public static void ConfigureAutoMapper(this IServiceCollection services)
    {

        services.AddAutoMapper(typeof(MappingProfile).Assembly);
    }
}
