

using Figgle;
using Spectre.Console;
using System.Text.Json;

namespace ToDoListMed_generiskaKlassOchJson
{
    public class ToDoSystem
    {

        public void ViewTasks(ToDoDB toDoDB)
        {
            var table = new Table()
                .Border(TableBorder.Rounded)
                .AddColumn("[yellow]Task ID[/]")
                .AddColumn("[yellow]Title[/]")
                .AddColumn("[yellow]Description[/]")
                .AddColumn("[yellow]Due Date[/]")
                .AddColumn("[yellow]Priority[/]")
                .AddColumn("[yellow]Is Completed?[/]")
                .AddColumn("[yellow]Created At[/]");

            foreach (var task in toDoDB.AllTaskDatafromToDoDB)
            {
                table.AddRow(
                    task.Id.ToString(),
                    task.Title,
                    task.Description,
                    task.DueDate.ToString("yyyy-MM-dd"),
                    task.Priority,
                    task.IsCompleted ? "[green]Yes[/]" : "[red]No[/]",
                    task.CreatedAt.ToString("yyyy-MM-dd HH:mm")
                );
            }

            AnsiConsole.Write(table);

            //save all changes 
            //   SaveAllData(toDoDB);
        }


        public void AddTask(ToDoDB toDoDB)
        {
            Console.Clear();
            AnsiConsole.MarkupLine("[yellow]------------- Hi and welcome agen you can add a New Task ------------[/]");

            // Create a generic class instance
            var TaskAdmin = new GeneriskaClass<Task>();

            // Load existing tasks into the generic class
            foreach (var t in toDoDB.AllTaskDatafromToDoDB)
            {
                TaskAdmin.AddTo(t);
            }

            // Prompt for task details
            int id = TaskAdmin.GetAll().Count() + 1;
            string title = AnsiConsole.Ask<string>("[green]Enter Task Title:[/]");
            string description = AnsiConsole.Ask<string>("[green]Enter Task Description:[/]");
            DateTime dueDate = AnsiConsole.Ask<DateTime>("[green]Enter Due Date (yyyy-MM-dd):[/]");

            // Prompt for priority
            var priority = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                     .Title("[green]Select the task priority level:[/]")
            .AddChoices("High", "Medium", "Low")
            );


            // Style priority based on user selection
            string levelOfTheStyledPriority = priority switch
            {
                "High" => "[red]High[/]",
                "Medium" => "[yellow]Medium[/]",
                "Low" => "[green]Low[/]",
                _ => "[grey]Unknown[/]" // Fallback, though it should not happen
            };

            // Task starts as not completed
            bool isCompleted = false;

            // Prompt for creation date (optional, defaults to today)
            DateTime createdAt = AnsiConsole.Prompt(
                new TextPrompt<DateTime>("[green]Enter task creation date (yyyy-MM-dd) or press Enter to use today's date:[/]")
                    .AllowEmpty()
                    .DefaultValue(DateTime.Now)
            );

            // Create a new task instance
            Task newTask = new(toDoDB.AllTaskDatafromToDoDB.Count + 1, title, description, dueDate, priority, isCompleted, createdAt)
            {
                Id = TaskAdmin.GetAll().Count + 1,
                Title = title,
                Description = description,
                DueDate = dueDate,
                Priority = priority,
                IsCompleted = isCompleted,
                CreatedAt = createdAt,
                CompletedAt = isCompleted ? DateTime.Now.ToLocalTime() : DateTime.Now,
                //Set completed date if applicable
            };

            // Add the task to the generic class and database
            TaskAdmin.AddTo(newTask);
            toDoDB.AllTaskDatafromToDoDB.Add(newTask);

            // Display a success message with task details
            AnsiConsole.MarkupLine("[green]Task added successfully![/]");
            AnsiConsole.MarkupLine($"[yellow]Task Details:[/]");
            AnsiConsole.MarkupLine($"[blue]ID:[/] {newTask.Id}");
            AnsiConsole.MarkupLine($"[blue]Title:[/] {newTask.Title}");
            AnsiConsole.MarkupLine($"[blue]Description:[/] {newTask.Description}");
            AnsiConsole.MarkupLine($"[blue]Due Date:[/] {newTask.DueDate.ToShortDateString()}");
            AnsiConsole.MarkupLine($"[blue]Priority:[/] {levelOfTheStyledPriority}");
            AnsiConsole.MarkupLine($"[blue]Status:[/] {(newTask.IsCompleted ? "[green]Completed[/]" : "[red]Pending[/]")}");


            //To save Data
            SaveAllData(toDoDB);
            // Pause briefly before returning

        }



        public void UpdateTask(ToDoDB toDoDB)
        {
            Console.Clear();

            var TaskAdmin = new GeneriskaClass<Task>();

            ViewTasks(toDoDB);
            int id = AnsiConsole.Ask<int>("[green]Enter Task ID to Update:[/]");

            // Retrieve the task from the database using the id
            var task = toDoDB.AllTaskDatafromToDoDB.FirstOrDefault(t => t.Id == id);

            if (task == null)
            {
                AnsiConsole.MarkupLine("[red]Task not found![/]");

                return;
            }

            // Prompt for new task details
            string title = AnsiConsole.Ask<string>($"[green]Enter New Title ({task.Title}):[/]");
            string description = AnsiConsole.Ask<string>($"[green]Enter New Description ({task.Description}):[/]");
            DateTime dueDate = AnsiConsole.Ask<DateTime>($"[green]Enter New Due Date ({task.DueDate:yyyy-MM-dd}):[/]");
            bool isCompleted = AnsiConsole.Confirm("[green]Mark as Completed?[/]", task.IsCompleted);




            // Add styled prompt using Spectre.Console
            AnsiConsole.MarkupLine("[green]Select the task priority level:[/]");


            AnsiConsole.MarkupLine("[blue]1:[/] [bold][red]High[/][/]");    // Red for High, with bold text
            AnsiConsole.MarkupLine("[blue]2:[/] [yellow]Medium[/]");  // Yellow for Medium
            AnsiConsole.MarkupLine("[blue]3:[/] [green]Low[/]");     // Green for Low

            // Ask the user for input
            string priorityInput = AnsiConsole.Ask<string>("[green]Enter the number of your choice (1, 2, 3):[/]");

            string priority = "Medium"; // Default value

            // Map user input to the corresponding priority value
            if (priorityInput == "1")
            {
                priority = "High";
            }
            else if (priorityInput == "2")
            {
                priority = "Medium";
            }
            else if (priorityInput == "3")
            {
                priority = "Low";
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Invalid choice, setting priority to Medium by default.[/]");
            }

            // Style priority based on user selection
            string levelOfTheStyledPriority = priority switch
            {
                "High" => "[red]High[/]",
                "Medium" => "[yellow]Medium[/]",
                "Low" => "[green]Low[/]",
                _ => "[grey]Unknown[/]" // Fallback, though it should not happen
            };

            Task updatedTask = new(id, title, description, dueDate, priority, isCompleted, task.CompletedAt)
            {
                Id = task.Id,
                Title = title,
                Description = description,
                DueDate = dueDate,
                Priority = priority,
                IsCompleted = isCompleted,
                CreatedAt = task.CreatedAt,
                CompletedAt = isCompleted ? DateTime.Now.ToLocalTime() : DateTime.Now,
                //Set completed date if applicable
            };

            // Update the task with new information
            TaskAdmin.Updater(toDoDB.AllTaskDatafromToDoDB, updatedTask, t => t.Id);


            // Save changes to the database
            SaveAllData(toDoDB); // This method saves the updated task list

            // Feedback and confirmation message
            AnsiConsole.MarkupLine("[green]Task updated successfully![/]");
            AnsiConsole.MarkupLine($"[yellow]Task Details Updated:[/]");
            AnsiConsole.MarkupLine($"[blue]ID:[/] {updatedTask.Id}");
            AnsiConsole.MarkupLine($"[blue]Title:[/] {updatedTask.Title}");
            AnsiConsole.MarkupLine($"[blue]Description:[/] {updatedTask.Description}");
            AnsiConsole.MarkupLine($"[blue]Due Date:[/] {updatedTask.DueDate.ToShortDateString()}");
            AnsiConsole.MarkupLine($"[blue]Priority:[/] {updatedTask.Priority}");
            AnsiConsole.MarkupLine($"[blue]Status:[/] {(updatedTask.IsCompleted ? "[green]Completed[/]" : "[red]Pending[/]")}");


        }


        public void DeleteTask(ToDoDB toDoDB)
        {


            Console.Clear();
            var TaskAdmin = new GeneriskaClass<Task>();
            ViewTasks(toDoDB);

            foreach (var t in toDoDB.AllTaskDatafromToDoDB)
            {
                TaskAdmin.AddTo(t);

            }



            int IdTodelet = AnsiConsole.Ask<int>("[green]Enter Task ID to Delete:[/]");

            var task = TaskAdmin.GetByID(IdTodelet);
            if (task == null)
            {
                Console.WriteLine("not found");
                return;
            }

            TaskAdmin.RemoveThis(IdTodelet);
            toDoDB.AllTaskDatafromToDoDB = TaskAdmin.GetAll();
            SaveAllData(toDoDB);
            AnsiConsole.MarkupLine("[red]Task deleted successfully![/]");



        }





        public void MarkTasksAsDone(ToDoDB toDoDB)
        {
            // Clear console and display a banner using Figgle
            Console.Clear();
            var figgleTitle = FiggleFonts.Standard.Render("Mark Tasks as Done");
            AnsiConsole.MarkupLine($"[bold yellow]{figgleTitle}[/]");

            // Ensure there are tasks to mark
            var pendingTasks = toDoDB.AllTaskDatafromToDoDB.Where(t => !t.IsCompleted).ToList();
            if (!pendingTasks.Any())
            {
                AnsiConsole.MarkupLine("[red]No pending tasks available to mark as done.[/]");
                return;
            }

            // Display pending tasks in a table
            var table = new Table();
            table.Border(TableBorder.Rounded);
            table.AddColumn("[yellow]ID[/]");
            table.AddColumn("[yellow]Title[/]");
            table.AddColumn("[yellow]Due Date[/]");
            table.AddColumn("[yellow]Status[/]");

            foreach (var task in pendingTasks)
            {
                string status = task.IsCompleted ? "[green]Completed[/]" : "[red]Pending[/]";
                table.AddRow(task.Id.ToString(), task.Title, task.DueDate.ToShortDateString(), status);
            }

            AnsiConsole.Write(table);

            // Get user input for task IDs to mark as done
            AnsiConsole.MarkupLine("[green]Enter task IDs separated by commas (e.g., 1, 2, 3):[/]");
            string? input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
            {
                AnsiConsole.MarkupLine("[yellow]No tasks selected.[/]");
                return;
            }

            // Parse the task IDs
            var selectedIds = input.Split(',') // split string  input into an array of strings to let the user enter multiple task
                .Select(id => int.TryParse(id.Trim(), out var parsedId) ? parsedId : (int?)null) // Trim ? Trim() and TryParse() converts it to: [1, 2, null, 3]
                .Where(id => id.HasValue)// filters all the null values 
                .Select(id => id.Value)// convert into a non-nullable int 
                .ToList();//convetr to list

            // Mark selected tasks as completed
            foreach (var task in pendingTasks)
            {
                if (selectedIds.Contains(task.Id))
                {
                    task.IsCompleted = true;
                    task.CompletedAt = DateTime.Now;
                }
            }

            // Save changes to the database
            SaveAllData(toDoDB);

            // Confirmation message with green styling
            AnsiConsole.MarkupLine($"[green]{selectedIds.Count} task(s) marked as done![/]");

            // Display updated task list in a table
            AnsiConsole.MarkupLine("[yellow]Updated Task List:[/]");
            var updatedTable = new Table();
            updatedTable.Border(TableBorder.Rounded);
            updatedTable.AddColumn("[yellow]ID[/]");
            updatedTable.AddColumn("[yellow]Title[/]");
            updatedTable.AddColumn("[yellow]Due Date[/]");
            updatedTable.AddColumn("[yellow]Status[/]");

            foreach (var task in toDoDB.AllTaskDatafromToDoDB)
            {
                string status = task.IsCompleted ? "[green]Completed[/]" : "[red]Pending[/]";
                updatedTable.AddRow(task.Id.ToString(), task.Title, task.DueDate.ToShortDateString(), status);
            }

            AnsiConsole.Write(updatedTable);
        }



        public void SaveAllData(ToDoDB toDoDB)
        {
            string dataJsonFilePath = "ToDoData.json";

            string updatedBankDB = JsonSerializer.Serialize(toDoDB, new JsonSerializerOptions { WriteIndented = true });


            File.WriteAllText(dataJsonFilePath, updatedBankDB);

            MirrorChangesToProjectRoot("ToDoData.json");

        }

        static void MirrorChangesToProjectRoot(string fileName)
        {
            // Get the path to the output directory
            string outputDir = AppDomain.CurrentDomain.BaseDirectory;

            // Get the path to the project root directory
            string projectRootDir = Path.Combine(outputDir, "../../../");

            // Define paths for the source (output directory) and destination (project root)
            string sourceFilePath = Path.Combine(outputDir, fileName);
            string destFilePath = Path.Combine(projectRootDir, fileName);

            // Copy the file if it exists
            if (File.Exists(sourceFilePath))
            {
                File.Copy(sourceFilePath, destFilePath, true); // true to overwrite
                Console.WriteLine($"{fileName} has been mirrored to the project root.");
            }
            else
            {
                Console.WriteLine($"Source file {fileName} not found.");
            }
        }

    }
}
