
using FluentValidation;
using AutoMapper;
using Labb1_MinimalAPI.Models;
using Labb1_MinimalAPI.Services;
using Labb1_MinimalAPI.Models.Validations;
using Microsoft.EntityFrameworkCore;
using Labb1_MinimalAPI.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Labb1_MinimalAPI.Data;
using System.ComponentModel.DataAnnotations;

namespace Labb1_MinimalAPI {
    public class Program {
        public static void Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);

            //Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<IRepository<Book>, BookRepository>();
            builder.Services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnectionString")));

            builder.Services.AddAutoMapper(typeof(MappingConfig));
            builder.Services.AddValidatorsFromAssemblyContaining<Program>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment()) {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();

            //Get all books
            app.MapGet("api/books", async ([FromServices] IRepository<Book> bookRepository) => {

                APIResponse response = new APIResponse {
                    Result = await bookRepository.GetAll(),
                    IsSuccess = true,
                    StatusCode = System.Net.HttpStatusCode.OK
                };
                return Results.Ok(response);

            }).WithName("GetAllBooks")
            .WithTags("Get")
            .Produces(200);

            //Get book by Title
            app.MapGet("api/books/{title}", async (IRepository<Book> bookRepository, string title) => {

                APIResponse response = new APIResponse() {
                    IsSuccess = false,
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };

                var bookRes = await bookRepository.GetByTitle(title);

                if (bookRes == null) {
                    response.ErrorMessages.Add($"Book With Title : {title} Not Found");
                    response.StatusCode = System.Net.HttpStatusCode.NotFound;

                    return Results.NotFound(response);
                }

                return Results.Ok(new APIResponse {
                    IsSuccess = true,
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Result = bookRes
                });

            }).WithName("GetBookByTitle")
            .WithTags("Get")
            .Produces(200)
            .Produces(404);

            //Get book by Id
            app.MapGet("api/books/{id:guid}", async (IRepository<Book> book, Guid id) => {
                
                APIResponse response = new APIResponse() { 
                    IsSuccess = false, 
                    StatusCode = System.Net.HttpStatusCode.BadRequest 
                };

                var bookRes = await book.GetById(id);

                if (bookRes == null) {
                    response.ErrorMessages.Add($"Book With ID : {id} Not Found");
                    response.StatusCode = System.Net.HttpStatusCode.NotFound;
                    return Results.NotFound(response);
                }

                return Results.Ok(new APIResponse {
                    IsSuccess = true,
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Result = bookRes
                });

            }).WithName("GetBookById")
            .WithTags("Get")
            .Produces(200)
            .Produces(404);

            //Create a new book 
            app.MapPost("api/books", async (IValidator<BookCreateDTO> _validator, IMapper _mapper, IRepository<Book> bookRepository, BookCreateDTO book_c_dto) => {

                APIResponse response = new() { 
                    IsSuccess = false, 
                    StatusCode = System.Net.HttpStatusCode.BadRequest 
                };
              
                var validationResult = await _validator.ValidateAsync(book_c_dto);
                if (!validationResult.IsValid) {
                    return Results.BadRequest(response);
                }

                var existingBook = (await bookRepository.GetAll()).FirstOrDefault(x => x.Title.ToLower() == book_c_dto.Title.ToLower());
                if (existingBook != null) {
                    response.ErrorMessages.Add($"Book With Title : {book_c_dto} Already Exists.");
                    return Results.Ok(response);
                }

                Book bookToCreate = _mapper.Map<Book>(book_c_dto);
                await bookRepository.Create(bookToCreate);

                return Results.Ok(new APIResponse {
                    IsSuccess = true,
                    StatusCode = System.Net.HttpStatusCode.Created,
                    Result = bookToCreate
                });

            }).WithName("CreateBook")
            .WithTags("Create")
            .Produces(201)
            .Produces(400);

            //Update book 
            app.MapPut("api/books", async (IValidator<BookUpdateDTO> _validator, IMapper _mapper, IRepository<Book> bookRepository, BookUpdateDTO book_u_dto) => {

                APIResponse response = new() {
                    IsSuccess = false,
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };

                var validationResult = await _validator.ValidateAsync(book_u_dto);
                if (!validationResult.IsValid) {
                    //Check if there's a book with given Id
                    if (await bookRepository.GetById(book_u_dto.Id) == null) {
                        response.ErrorMessages.Add($"Book With ID : {book_u_dto.Id} Not Found.");
                        response.StatusCode = System.Net.HttpStatusCode.NotFound;

                        return Results.NotFound(response);
                    }
                    return Results.BadRequest(response);
                }

                Book bookToUpdate = _mapper.Map<Book>(book_u_dto);
                await bookRepository.Update(bookToUpdate);

                return Results.Ok(new APIResponse {
                    IsSuccess = true,
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Result = bookToUpdate
                });

            }).WithName("UpdateBook")
            .WithTags("Update")
            .Produces(200)
            .Produces(404);

            //Delete book by guid
            app.MapDelete("api/books/{id:guid}", async (IRepository<Book> bookRepository, Guid id) => {
                
                APIResponse response = new() { 
                    IsSuccess = false, 
                    StatusCode = System.Net.HttpStatusCode.BadRequest 
                };

                if (await bookRepository.GetById(id) == null) {
                    response.ErrorMessages.Add($"Book With ID : {id} Not Found.");
                    response.StatusCode = System.Net.HttpStatusCode.NotFound;
                    return Results.NotFound(response);
                }

                await bookRepository.Delete(id);
                return Results.Ok(new APIResponse {
                    IsSuccess = true,
                    StatusCode = System.Net.HttpStatusCode.NoContent,
                });

            }).WithName("DeleteBook")
            .WithTags("Delete")
            .Produces(204)
            .Produces(404);

            app.Run();
        }
    }
}