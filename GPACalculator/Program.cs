using GPACalculator.Data;
using GPACalculator.Repository;
using Microsoft.EntityFrameworkCore;

namespace GPACalculator {
    public class Program {
        public static void Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);
             //THIS...
            builder.Services.AddTransient<IGPACalculatorRepository, GPACalculatorRepository>();

            builder.Services.AddDbContext<AppDbContext>(c =>
            c.UseSqlServer(builder.Configuration["AppDbContextConnection"]));
            
            //...

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment()) {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}