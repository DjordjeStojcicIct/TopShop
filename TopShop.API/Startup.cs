using System.Runtime.CompilerServices;
using TopShop.API.DTO;
using TopShop.Implementation.Validators;
using TopShop.Application.UseCases.Commands;
using TopShop.DataAccess;
using TopShop.Implementation.Extensions;
using TopShop.Implementation.UseCases.Commands;
using TopShop.Application.Uploads;
using TopShop.Application.UseCaseHandling;
using TopShop.Implementation.Uploads;
using TopShop.Application.Logging;
using TopShop.Implementation.Logging;
using Microsoft.OpenApi.Models;
using TopShop.API.Jwt;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using TopShop.Application;
using TopShop.API.Extensions;
using TopShop.Application.UseCases.Queries;
using TopShop.Implementation.UseCases.Queries;
using TopShop.API.Middleware;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using Bugsnag.AspNet.Core;

namespace TopShop.API;

public class Startup
{
    public Startup(IConfiguration configuration) 
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        AppSettings appSettings = new AppSettings();
        Configuration.Bind(appSettings);

        services.AddTransient<ITokenStorage, InMemoryTokenStorage>();
        services.AddTransient<JwtManager>(x => 
        {
            TopShopContext context = x.GetService<TopShopContext>();
            ITokenStorage tokenStorage = x.GetService<ITokenStorage>();
            return new JwtManager(context, appSettings.Jwt.Issuer, appSettings.Jwt.SecretKey, appSettings.Jwt.DurationSeconds, tokenStorage);
        });

        services.AddBugsnag(configuration =>
        {
            configuration.ApiKey = appSettings.BugSnagKey;
        });

        services.AddLogger();

        services.AddValidators();

        services.AddHttpContextAccessor();

        services.AddScoped<IApplicationActor>(x =>
        {
            var accessor = x.GetService<IHttpContextAccessor>();
            var header = accessor?.HttpContext?.Request.Headers["Authorization"];

            if (!header.HasValue)
            {
                return new UnauthorizedActor();

            }
            var data = header?.ToString().Split("Bearer ");

            if (data?.Length < 2)
            {
                return new UnauthorizedActor();
            }

            var handler = new JwtSecurityTokenHandler();

            var tokenObj = handler.ReadJwtToken(data?[1].ToString());

            var claims = tokenObj.Claims;

            var email = claims.First(x => x.Type == "Email").Value;
            var id = claims.First(x => x.Type == "Id").Value;
            var username = claims.First(x => x.Type == "Username").Value;
            var useCases = claims.First(x => x.Type == "UseCases").Value;

            List<int> useCaseIds = JsonConvert.DeserializeObject<List<int>>(useCases);

            return new JwtActor
            {
                Email = email,
                AllowedUseCases = useCaseIds,
                Id = int.Parse(id),
                Username = username,
            };
        });

        services.AddTransient<TopShopContext>();
        services.AddTransient<IUseCaseLogger, EfUseCaseLogger>();
        services.AddTransient<IQueryHandler, QueryHandler>();
        services.AddTransient<ICommandHandler, CommandHandler>();
        services.AddTransient<IBase64FileUploader, EfBase64FileUploader>();

        services.AddTransient<IRegisterUserCommand, EfRegisterUserCommand>();
        services.AddTransient<ICreateCategoryCommand, EfCreateCategoryCommand>();
        services.AddTransient<ICreateOrderCommand, EfCreateOrderCommand>();
        services.AddTransient<ICreateProductCommand, EfCreateProductCommand>();
        services.AddTransient<ICreateReviewCommand, EfCreateReviewCommand>();
        services.AddTransient<ICreateWishlistCommand, EfCreateWishlistCommand>();
        services.AddTransient<IDeleteOrderCommand, EfDeleteOrderCommand>();
        services.AddTransient<IDeleteWishlistCommand, EfDeleteWishlistCommand>();
        services.AddTransient<IDeleteReviewCommand, EfDeleteReviewCommand>();
        services.AddTransient<IEditCategoryCommand, EfEditCategoryCommand>();

        services.AddTransient<IGetAddressesQuery, EfGetAddressesQuery>();
        services.AddTransient<IGetAuditLogsQuery, EfGetAuditLogsQuery>();
        services.AddTransient<IGetCategoriesQuery, EfGetCategories>();
        services.AddTransient<IGetOrdersQuery, EfGetOrdersQuery>();
        services.AddTransient<IGetProductsQuery, EfGetProductsQuery>();
        services.AddTransient<IGetReviewsQuery, EfGetReviewsQuery>();
        services.AddTransient<IGetRolesQuery, EfGetRoleQuery>();
        services.AddTransient<IGetUsersQuery, EfGetUsersQuery>();
        services.AddTransient<IGetWishlistsQuery, EfGetWishlistsQuery>();

        services.AddTransient<IFindAddressQuery, EfFindAddressQuery>();
        services.AddTransient<IFindCategoryQuery, EfFindCategoryQuery>();
        services.AddTransient<IFindOrderQuery, EfFindOrderQuery>();
        services.AddTransient<IFindProductQuery, EfFindProductQuery>();
        services.AddTransient<IFindReviewQuery, EfFindReviewQuery>();
        services.AddTransient<IFindRoleQuery, EfFindRoleQuery>();
        services.AddTransient<IFindUserQuery, EfFindUserQuery>();
        services.AddTransient<IFindWishListQuery, EfFindWishlistQuery>();

        services.AddControllers();
        services.AddSwaggerGen(c => 
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "TopShop.API", Version = "v1" });
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme 
            {
                In = ParameterLocation.Header,
                Description = "Please enter valid token",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header,
                    },
                    new List<string>()
                }
            });
        });

        services.AddJwt(appSettings);
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseDeveloperExceptionPage();
        app.UseStaticFiles();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TopShop.API v1"));


        app.UseHttpsRedirection();

        app.UseRouting();


        app.UseAuthentication();
        app.UseAuthorization();


        app.UseMiddleware<ExceptionHandlingMiddleware>();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }


}
