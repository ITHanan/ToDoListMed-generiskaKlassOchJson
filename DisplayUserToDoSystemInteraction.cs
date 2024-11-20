

using Spectre.Console;
using System.Text.Json;

namespace ToDoListMed_generiskaKlassOchJson
{
    public class DisplayUserToDoSystemInteraction
    {


        public void Run()
        {

            ToDoSystem toDoSystem = new ToDoSystem();

            string dataJsonFilePath = "ToDoData.json";
            string allDataAsJson = File.ReadAllText(dataJsonFilePath);
            ToDoDB toDoDB = JsonSerializer.Deserialize<ToDoDB>(allDataAsJson)!;

            bool running = true;

            while (running)
            {
               

                // Display the menu and get the selected option
                string option = DisplayMenu();

                switch (option)
                {
                    case "1. View All Tasks":
                        toDoSystem.ViewTasks(toDoDB); 
                      
                        break;
                    case "2. Add New Task":
                        toDoSystem.AddTask(toDoDB);
                        break;
                    case "3. Update Task":
                        toDoSystem.UpdateTask(toDoDB);
                        break;
                    case "4. Delete Task":
                        toDoSystem.DeleteTask(toDoDB);
                        break;
                    case "5. Mark Tasks as Done":
                            toDoSystem.MarkTasksAsDone(toDoDB);
                        break;
                    case "0. Save and Exit":
                        //   toDoSystem.SaveAllData(toDoDB);
                        running = false;
                        break;
                    default:
                        AnsiConsole.MarkupLine("[red]Invalid choice! Please try again.[/]");
                        break;
                }
            } Console.Clear();
        }


        public static string DisplayMenu()
        {
            return AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[green]Select an option from the Task Manager Menu:[/]")
                    .PageSize(6)
                    .MoreChoicesText("[grey](Move up and down to reveal more options)[/]")
                    .AddChoices(new[]
                    {
                "1. View All Tasks",
                "2. Add New Task",
                "3. Update Task",
                "4. Delete Task",
                "5. Mark Tasks as Done",
                "0. Save and Exit"
                    }));
        }

    }
}
