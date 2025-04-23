using System.Text;

using fullstack_backend.Context;
using fullstack_backend.Hubs;

// using fullstack_backend.Hubs;
using fullstack_backend.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<UserServices>();
builder.Services.AddScoped<FriendshipServices>();

//builder for chat (For SignalR Services)
builder.Services.AddSignalR();

var connectionString = builder.Configuration.GetConnectionString("DatabaseConnection");
builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(connectionString));

var secretKey = builder.Configuration["JWT:key"];
var signingCredentials = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!));

builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer( options => {
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        
        ValidIssuer = "https://fullstackwebapp-bxcja2evd2hef3b9.westus-01.azurewebsites.net/",
        ValidAudience = "https://fullstackwebapp-bxcja2evd2hef3b9.westus-01.azurewebsites.net/",
        IssuerSigningKey = signingCredentials
    };
});

//Custom added scopes
builder.Services.AddScoped<UserServices>();

builder.Services.AddCors(options =>{
    options.AddPolicy("AllowAll",
    policy=>{
        policy.AllowAnyHeader()
        .AllowAnyMethod()
        .AllowAnyOrigin();
    }
    );
});

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.MapHub<MessagingHub>("/hub");

app.Run();
