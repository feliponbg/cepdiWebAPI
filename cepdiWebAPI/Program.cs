using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

#region Token
byte[] data = Convert.FromBase64String(builder.Configuration.GetValue<string>("JWT:ClaveSecreta").ToString());
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            //ValidateIssuer = true,
            //ValidateAudience = true,
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            //ValidateIssuer = Configuration["JWT:Issuer"],
            //ValidateAudience = Configuration["JWT:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(data),
            ClockSkew = TimeSpan.Zero
        };
    });
#endregion Token

#region CORS
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                          policy.WithExposedHeaders();
                      });
});
#endregion CORS

// Add services to the container.
#region Servicios Locales
//Utilerias

//Servicios
builder.Services.AddTransient<cepdiWebAPI.Services.Medicamento>();        //Siempre es diferente, No mantiene ningun tipo de estado
builder.Services.AddTransient<cepdiWebAPI.Services.Sesion>();

//Servicios Utilerias
builder.Services.AddTransient<cepdiWebAPI.Services.Utilerias.Excel>();
#endregion Servicios Locales

builder.Services.AddControllers();
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

app.UseHttpsRedirection();

#region CORS
app.UseCors(MyAllowSpecificOrigins);
#endregion CORS

#region Token
app.UseAuthentication();
#endregion Token

app.UseAuthorization();

app.MapControllers();

app.Run();
