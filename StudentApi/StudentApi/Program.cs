using Microsoft.EntityFrameworkCore;
using StudentApi.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// add DB service
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("SchoolManagementSystem")));


var app = builder.Build();

var students = new List<Studnet>
{
    new Studnet{Id=1, Name="Albin"},
    new Studnet{Id=2, Name="Aline"},
    new Studnet{Id=3, Name="Anju"},
};


app.MapGet("/student", () => students);

app.MapGet("/student/{id}", (int id) =>
    {
        var student = students.Find(student => student.Id == id);
        return student;
    }
);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

class Studnet
{
    public int Id { get; set; }
    public string? Name { get; set; }
}