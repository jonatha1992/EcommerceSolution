using Ecommerce.Domain;
using Ecommerce.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddInfrastructureServices(builder.Configuration);


builder.Services.AddDbContext<EcommerceDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("ConecctionStringPC"),
b => b.MigrationsAssembly(typeof(EcommerceDbContext).Assembly.FullName)
));


builder.Services.AddControllers(options =>
{
    var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
    options.Filters.Add(new AuthorizeFilter(policy));
});

IdentityBuilder identityBuilder = builder.Services.AddIdentityCore<User>();
identityBuilder = new IdentityBuilder(identityBuilder.UserType, identityBuilder.Services);
identityBuilder.AddRoles<IdentityRole>().AddDefaultTokenProviders();
identityBuilder.AddClaimsPrincipalFactory<UserClaimsPrincipalFactory<User, IdentityRole>>();
identityBuilder.AddEntityFrameworkStores<EcommerceDbContext>();
identityBuilder.AddSignInManager<SignInManager<User>>();
builder.Services.TryAddSingleton<ISystemClock, SystemClock>();



var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(opt =>
{
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = key,
        ValidateAudience = false,
        ValidateIssuer = false
    };
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder => builder.AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()
    );

});



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();



app.UseAuthentication(); // para trabajar con identityrole
app.UseAuthorization(); // para trabajar con identityrole
app.UseCors("CorsPolicy");

app.MapControllers();

//using (var scope = app.Services.CreateScope())
//{
//    var service = scope.ServiceProvider;
//    var loggerFactory = service.GetRequiredService<ILoggerFactory>();

//    try
//    {
//        var context = service.GetRequiredService<EcommerceDbContext>();
//        var usuarioManager = service.GetRequiredService<UserManager<User>>();
//        var roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();
//        await context.Database.MigrateAsync();
//        await EcommerceDbContextData.LoadDataAsync(context, usuarioManager, roleManager, loggerFactory);
//    }
//    catch (Exception ex)
//    {
//        var logger = loggerFactory.CreateLogger<Program>();
//        logger.LogError(ex, "Error en la migration");
//    }
//}


app.Run();
