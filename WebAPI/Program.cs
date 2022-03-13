using Autofac;
using Autofac.Extensions.DependencyInjection;
using Business.DependencyResolvers.Autofac;
using Core.DependencyResolvers;
using Core.Extensions;
using Core.Utilities.IoC;
using Core.Utilities.Security.Encryption;
using Core.Utilities.Security.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;



var builder = WebApplication.CreateBuilder(args);
IConfiguration configuration = builder.Configuration;

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

// Add services to the container.

builder.Services.AddControllers();

builder.Host.ConfigureContainer<ContainerBuilder>(builder => 
    builder.RegisterModule(new AutofacBusinessModule()));

builder.Services.AddCors();

//builder.Services.Configure<StripeSettings>(configuration.GetSection("Stripe"));

var tokenOptions = configuration.GetSection("TokenOptions").Get<TokenOptions>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidAudience = tokenOptions.Audience,
            ValidIssuer = tokenOptions.Issuer,
            IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(builder.Configuration["TokenOptions:SecurityKey"])
        };

    });

builder.Services.AddDependencyResolvers(new ICoreModule[]
{
    new CoreModule(),
    new SecurityModule()
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//StripeConfiguration.ApiKey = (configuration.GetSection("Stripe")["SecretKey"]);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ConfigureCustomExceptionMiddleware();

app.UseCors(builder => builder.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()
    .WithMethods("GET","POST","PUT","DELETE").DisallowCredentials());

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseStaticFiles();

app.MapControllers();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
