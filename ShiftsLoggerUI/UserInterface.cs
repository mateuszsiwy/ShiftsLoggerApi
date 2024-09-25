using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;
using ShiftsLoggerUI.Services;
using System.Text.Json;
using ShiftsLoggerUI.Helpers;
namespace ShiftsLoggerUI
{
    internal class UserInterface
    {
        private readonly ApiService _apiService;
        public UserInterface() 
        {
            _apiService = new ApiService();
           
        }

        public async Task StartApp()
        {
            while (true)
            {
                var options = new Dictionary<string, Func<Task>>
                {
                    {"Add Shift", async () => await Add() },
                    {"Delete Shift", async () => await Delete() },
                    {"Update Shift", async() => await Update() },
                    {"View Shifts", async() => await Read() }
                };

                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                    .Title("\nWelcome to the Shifts Logger, please pick an option")
                    .PageSize(10)
                    .AddChoices(options.Keys));
                await options[choice]();
            }
        }
        public async Task Add()
        {
            try
            {
                ShiftItem shift = new ShiftItem();
                Console.WriteLine("Please enter the name");
                shift.Name = Console.ReadLine();
                shift.StartShift = DateHelper.getUserDate();
                shift.EndShift = DateHelper.getUserDate();
                shift.Duration = DateHelper.calculateDuration(shift.StartShift, shift.EndShift);
                var jsonContent = JsonSerializer.Serialize(shift);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");


                var response = await _apiService.PostAsync("", content);

                if (response.IsSuccessStatusCode)
                {
                    AnsiConsole.Markup("[green]Shift added successfully![/]\n");
                }
                else
                {
                    AnsiConsole.Markup($"[red]Failed to add shift. Status Code: {response.StatusCode}[/]\n");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


        }

        public async Task Delete()
        {
            while (true)
            {
                Console.WriteLine("Please enter the ID to be deleted:");
                string idString = Console.ReadLine();
                var isNumeric = int.TryParse(idString, out int id);

                if (isNumeric)
                {
                    try
                    {
                        // Call DeleteAsync with the given ID
                        var response = await _apiService.DeleteAsync($"/{id}");

                        // Handle response status
                        if (response.IsSuccessStatusCode)
                        {
                            AnsiConsole.Markup("[green]Shift deleted successfully![/]\n");
                        }
                        else
                        {
                            AnsiConsole.Markup($"[red]Failed to delete shift. Status Code: {response.StatusCode}[/]\n");
                        }

                        break; // Exit the loop after a successful or failed API call
                    }
                    catch (Exception ex)
                    {
                        AnsiConsole.Markup($"[red]Error during deletion: {ex.Message}[/]\n");
                    }
                }
                else
                {
                    AnsiConsole.Markup("[yellow]Invalid input. Please enter a valid numeric ID.[/]\n");
                }
            }
        }

        public async Task Update()
        {
            try
            {
                int id;
                while (true)
                {
                    Console.WriteLine("Please enter the ID to be updated:");
                    string idString = Console.ReadLine();

                    // Check if the input is numeric
                    if (int.TryParse(idString, out id))
                    {
                        break; // Exit the loop if the ID is valid
                    }
                    else
                    {
                        AnsiConsole.Markup("[red]Please enter a valid numeric ID.[/]\n");
                    }
                }

                ShiftItem shift = new ShiftItem();
                shift.Id = id; // Set the ID of the shift item
                Console.WriteLine("Please enter the name:");
                shift.Name = Console.ReadLine();
                shift.StartShift = DateHelper.getUserDate();
                shift.EndShift = DateHelper.getUserDate();
                shift.Duration = DateHelper.calculateDuration(shift.StartShift, shift.EndShift);

                var jsonContent = JsonSerializer.Serialize(shift);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await _apiService.PutAsync($"/{id}", content);

                if (response.IsSuccessStatusCode)
                {
                    AnsiConsole.Markup("[green]Shift updated successfully![/]\n");
                }
                else
                {
                    AnsiConsole.Markup($"[red]Failed to update shift. Status Code: {response.StatusCode}[/]\n");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task Read()
        {
            try
            {
                var response = await _apiService.GetAsync("");
                if (response.IsSuccessStatusCode)
                {
                    var jsonShifts = await response.Content.ReadAsStringAsync();
                    var shifts = JsonSerializer.Deserialize<List<ShiftItem>>(jsonShifts);
                    AnsiConsole.Markup("[green]Get completed succesfully[/]\n");
                    foreach (var shift in shifts)
                    {
                        Console.WriteLine($"ID: {shift.Id}, Employee: {shift.Name}, " +
                                          $"Start: {shift.StartShift}, End: {shift.EndShift}, Duration: {shift.Duration}\n\n");
                    }
                }
                else
                {
                    AnsiConsole.Markup("[red]Failed to complete Get request[/]\n");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
