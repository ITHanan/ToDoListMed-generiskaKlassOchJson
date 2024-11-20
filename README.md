# ToDo List Management System

## Overview

The **ToDo List Management System** is a console-based application built with C# that helps users manage their tasks effectively. 
It utilizes generics, JSON for data storage, and the **Spectre.Console** library for a styled and interactive command-line interface.
Users can add, update, view, and delete tasks while maintaining priorities, statuses, and deadlines.

## Features

1. **Task CRUD Operations**:
   - Add, View, Update,Mark Tasks As Done ,and Delete tasks with ease.

2. **Priority Management**:
   - Assign priorities to tasks: High, Medium, or Low.

3. **Interactive Interface**:
   - Styled CLI prompts and outputs using **Spectre.Console**.

4. **Persistent Storage**:
   - Task data is stored in JSON for persistence between sessions.

5. **Generics**:
   - A reusable generic class for performing operations on collections.

6. **Validation**:
   - Input validation to prevent incorrect data entries.

## Requirements

- **.NET 6.0 or later**: Ensure you have the .NET SDK installed.
- **NuGet Packages**:
  - `Spectre.Console` for styled console output.
  - `System.Text.Json` for JSON handling.

## Installation

1. Clone the repository:
   
https://github.com/ITHanan/ToDoListMed-generiskaKlassOchJson.git

2. Restore dependencies:
  
dotnet restore

3. Build the project:
4. 
  dotnet build

5. Run the application:
   ```bash
   dotnet run
   ```

## How to Use

### Adding a Task
1. Launch the application.
2. Select the option to add a task.
3. Provide details like title, description, due date, priority, and completion status.

### Viewing Tasks
- View all tasks in a tabular format with their IDs, priorities, and statuses.

### Updating a Task
1. Choose a task by its ID.
2. Modify any details (title, description, due date, priority, status).

### Deleting a Task
1. Select the option to delete a task.
2. Enter the task ID to remove it.

## Code Structure

- **Classes**:
  - `Task`: Represents a single task.
  - `ToDoDB`: Manages the list of tasks and JSON storage.
  - `GeneriskaClass<T>`: A generic class for collection operations.
  - `DisplayUserToDoSystemInteraction`: Handles user interactions and menu navigation.

- **Methods**:
  - **CRUD Operations**:
    - `AddTask(ToDoDB toDoDB)`
    - `ViewTasks(ToDoDB toDoDB)`
    - `UpdateTask(ToDoDB toDoDB)`
    - `DeleteTask(ToDoDB toDoDB)`
    - `MarkTasksAsDone(ToDoDB toDoDB)`

  - **Helper Methods**:
    - `SaveAllData(ToDoDB toDoDB)`: Saves tasks to JSON.

## Key Technologies

- **C#**: Main programming language.
- **Spectre.Console**: Styled console user interface.
- **System.Text.Json**: Serialization and deserialization of task data.

## Example Output

### Task List
```
+----+------------------+-----------------------+------------+-----------+------------+
| ID | Title            | Description           | Due Date   | Priority  | Status     |
+----+------------------+-----------------------+------------+-----------+------------+
| 1  | Finish project   | Complete the report   | 2024-11-25 | [red]High[/]  | [red]Pending[/] |
| 2  | Buy groceries    | Milk, eggs, bread     | 2024-11-21 | [green]Low[/]  | [green]Completed[/] |
+----+------------------+-----------------------+------------+-----------+------------+
```

## Future Enhancements

1. **Search Functionality**:
   - Allow users to search tasks by title or description.
   
2. **Sorting and Filtering**:
   - Enable sorting by priority, due date, or status.

3. **Unit Tests**:
   - Add comprehensive test coverage.

4. **Cross-Platform GUI**:
   - Extend to a GUI-based application.

## Contribution

Contributions are welcome! Please fork the repository and submit a pull request.

## License

This project is licensed under the MIT License.

## Contact

- **Author**: Hanan Ahmed
- **Email**: ithanan@gmail.com
- **GitHub**: ITHanan

---

Enjoy managing your tasks effectively! 😊

