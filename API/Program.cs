using API.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<StoreContext>(opt => 
{
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
})  ;  //Nuestro Storecontext al cual le vamos a pasar unas opciones, que queremos usar Sqlite, dentro de useSqlite le tenemos que pasar una
//Configuracion --> para obtener el string de conexion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//Creamos una variable scope para  crear el scope
var scope= app.Services.CreateScope();
//creamos otra variable para guardar el context
var context=scope.ServiceProvider.GetRequiredService<StoreContext>();
//logger para tener un log de cada error que tengamos
var logger=scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
//Ahora necesitamos ejecutar código contra nuestro context
try
{
    context.Database.Migrate();  //crea la BD si todavía no existe, si existe no hace nada
    //Usamos la clase initializer que creamos antes, como era estática, la podemos llamar directamente
    DbInitializer.Initialize(context);
}
catch (Exception ex)
{
    
    logger.LogError(ex, "A problem ocurred during migration");
}


app.Run();
