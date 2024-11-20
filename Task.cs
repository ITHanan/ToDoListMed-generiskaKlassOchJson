using Microsoft.VisualBasic;

namespace ToDoListMed_generiskaKlassOchJson
{
    public class Task :IIdentifiable
    {

        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; }= string.Empty;

        public DateTime DueDate { get; set; }

        public string Priority { get; set; }

        public bool IsCompleted { get; set; } = false;

        public DateTime CreatedAt { get; set; }= DateTime.Now;

        public DateTime CompletedAt { get; set; }



        public Task(int id, string title, string description, DateTime dueDate, string priority, bool isCompleted, DateTime createdAt)
        {

            Id = id;
            Title = title;
            Description = description;
            DueDate = dueDate;
            Priority = priority;
            IsCompleted = isCompleted;
            CreatedAt = createdAt;

        }
    }
}
