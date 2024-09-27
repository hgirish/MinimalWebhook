using MinimalWebhook;
using MinimalWebhook.Models;
using System.Text.RegularExpressions;

public static class PersonEndpoints
{
	public static void MapPersonEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Person").WithTags(nameof(Person));

        group.MapGet("/", () =>
        {
            return new [] { new Person() };
        })
        .WithName("GetAllPeople")
        .WithOpenApi();

        group.MapGet("/byphone", (string phone, IDataService dataService) =>
        {
            return dataService.GetPersonByPhone(phone);

        })
        .WithName("GetPersonById")
        .WithOpenApi();

        group.MapPut("/{id}", (int id, Person input) =>
        {
            return TypedResults.NoContent();
        })
        .WithName("UpdatePerson")
        .WithOpenApi();

        group.MapPost("/", (Person model) =>
        {
            //return TypedResults.Created($"/api/People/{model.ID}", model);
        })
        .WithName("CreatePerson")
        .WithOpenApi();

        group.MapDelete("/{id}", (int id) =>
        {
            //return TypedResults.Ok(new Person { ID = id });
        })
        .WithName("DeletePerson")
        .WithOpenApi();
        group.MapPatch("/{id}", (int id, Person model, IDataService dataService) =>
        {
            dataService.PatchPerson(id, model);
            return TypedResults.NoContent();
        })
        .WithName("PatchPerson")
        .WithOpenApi();
    }
}