
using DBRejectionTherapyAPI.Data;
using DBRejectionTherapyAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DBRejectionTherapyAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
           
            MongoCRUD db = new MongoCRUD("Dares");
            MongoCRUD userdb = new MongoCRUD("Users");

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseHttpsRedirection();

            app.UseAuthorization();

            //Add Dare method 
            app.MapPost("/dare", async (Dares dare) =>
            {
                try
                {
                    var addDare = await db.AddDare("Dares", dare);
                    return Results.Ok(addDare);
                } 
                catch (Exception ex)
                {
                    return Results.NotFound(ex);
                }
            });

            //Add User method 
            app.MapPost("/user", async (Users user) =>
            {
                try
                {
                    var addUser = await db.AddUser("Users", user);
                    return Results.Ok(addUser);
                }
                catch (Exception ex)
                {
                    return Results.NotFound(ex);
                }
            });

            //Show All dares Method
            app.MapGet("/dares", async () =>
            {
                try
                {
                    var dare = await db.ShowDares("Dares");
                    return Results.Ok(dare);
                }
                catch (Exception ex)
                {
                    return Results.NotFound(ex);
                }
            });

            //Show All users Method
            app.MapGet("/users", async () =>
            {
                try
                {
                    var user = await db.ShowUsers("Users");
                    return Results.Ok(user);
                }
                catch (Exception ex)
                {
                    return Results.NotFound(ex);
                }
            });

            //Get dare by Id method
            app.MapGet("/dare/{id}", async (string id) =>
            {
                try
                {
                    var dare = await db.ShowDare("Dares", id);
                    return Results.Ok(dare);
                }
                catch (Exception ex)
                {
                    return Results.NotFound(ex);
                }
            });

            //Get user by Id method
            app.MapGet("/user/{id}", async (string id) =>
            {
                try
                {
                    var user = await db.ShowUser("Users", id);
                    return Results.Ok(user);
                }
                catch (Exception ex)
                {
                    return Results.NotFound(ex);
                }
            });

            //Update dare method 
            app.MapPut("/dare", async (Dares updatedDare) =>
            {
                try
                {
                    var dare = await db.UpdateDare("Dares", updatedDare);
                    return Results.Ok(dare);
                }
                catch (Exception ex)
                {
                    return Results.NotFound(ex);
                }   
            });

            //update user method
            app.MapPut("/user", async (Users updatedUser) =>
            {
                try
                {
                    var user = await db.UpdateUser("Users", updatedUser);
                    return Results.Ok(user);
                }
                catch (Exception ex)
                {
                    return Results.NotFound(ex);
                }
            });

            //Delete dare method
            app.MapDelete("/dare/{id}", async (string id) =>
            {
                try
                {
                    var dare = await db.DeleteDare("Dares", id);
                    return Results.Ok(dare);
                }
                catch (Exception ex)
                {
                    return Results.NotFound(ex);
                }
            });

            //Delete user method
            app.MapDelete("/user/{id}", async (string id) =>
            {
                try
                {
                    var user = await db.DeleteUser("Users", id);
                    return Results.Ok(user);
                }
                catch (Exception ex)
                {
                    return Results.NotFound(ex);
                }
            });

            app.Run();
        }
    }
}
