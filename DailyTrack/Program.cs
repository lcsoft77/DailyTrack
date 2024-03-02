using DailyTrack.ApplicationCore;
using DailyTrack.Client.Pages;
using DailyTrack.Common.Interface;
using DailyTrack.Common.Service;
using DailyTrack.Common.ViewModel;
using DailyTrack.Components;
using DailyTrack.Infrastructure;
using DailyTrack.ViewModel;
using Microsoft.FluentUI.AspNetCore.Components;

namespace DailyTrack
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveWebAssemblyComponents();
            builder.Services.AddFluentUIComponents();

            builder.Services.AddSingleton<IActivityService>(new ActivityService("activities.db"));

            builder.Services.ConfigureHttpJsonOptions(options =>
            {
                options.SerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
            });

            builder.Services.AddHttpClient<IActivityWebService, ActivityWebService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:7057"); // Sostituisci con l'URL del tuo server Web
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveWebAssemblyRenderMode()
                .AddAdditionalAssemblies(typeof(Counter).Assembly);

            app.MapPut("/api/activity/{id}", async (IActivityService activityService, Guid id, ActivityViewModel activity) =>
            {
                var existing = await activityService.GetActivityById(id);
                if (existing == null)
                {
                    return Results.NotFound();
                }

                existing.Name = activity.Name;
                existing.Description = activity.Description;
                existing.Type = activity.Type.ToDomainTaskType();

                await activityService.UpdateActivity(existing);

                return Results.Ok();
            });

            app.MapPost("/api/activity", async (IActivityService activityService, ActivitiyNewViewModel task) =>
            {
                var result = DailyTrack.Domain.Activity.Create(task.Name,
                    task.Description, task.CreatedAt, task.Type.ToDomainTaskType());
                if (result.IsFailure)
                {
                    return Results.BadRequest(result.Error);
                }
                await activityService.AddActivity(result.Value);

                return Results.Ok();
            });

            app.MapGet("/api/activities/", async (IActivityService activityService, DateTime startDate, DateTime endDate) =>
            {
                var activities = await activityService.GetActivitiesForDate(startDate, endDate);
                return activities.Select(t => new ActivityViewModel
                {
                    id = t.Id.ToString(),
                    Description = t.Description,
                    Name = t.Name,
                    CreatedAt = t.CreatedAt,
                    StartedAt = t.StartedAt,
                    Status = t.Status.ToTaskStatusViewModel(),
                    Type = t.Type.ToTaskTypeViewModel(),
                    Logs = t.Logs
                });
            });

            app.MapGet("/api/activity/{id:Guid}", async (IActivityService activityService, Guid id) =>
            {
                //var guid = Guid.Parse(id);
                var activity = await activityService.GetActivityById(id);
                if (activity == null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(new ActivityViewModel
                {
                    id = activity.Id.ToString(),
                    Description = activity.Description,
                    Name = activity.Name,
                    CreatedAt = activity.CreatedAt,
                    StartedAt = activity.StartedAt,
                    Status = activity.Status.ToTaskStatusViewModel(),
                    Type = activity.Type.ToTaskTypeViewModel(),
                    Logs = activity.Logs
                });
            });

            app.MapDelete("/api/activity/{id:Guid}", async (IActivityService activityService, Guid id) =>
            {
                await activityService.DeleteActivity(id);
                return Results.Ok();
            });

            app.MapGet("/api/start/{id:Guid}", async (IActivityService activityService, Guid id) =>
            {
                var activity = await activityService.GetActivityById(id);
                if (activity == null)
                {
                    return Results.NotFound();
                }
                activity.Start();
                await activityService.UpdateActivity(activity);
                return Results.Ok();
            });

            app.MapGet("/api/stop/{id:Guid}", async (IActivityService activityService, Guid id) =>
            {
                var activity = await activityService.GetActivityById(id);
                if (activity == null)
                {
                    return Results.NotFound();
                }
                activity.Stop();
                await activityService.UpdateActivity(activity);
                return Results.Ok();
            });

            app.Run();
        }
    }
}
