
using System.Text.Json.Serialization;

namespace ToDoListMed_generiskaKlassOchJson
{
    public class ToDoDB
    {
        [JsonPropertyName("Task")]
        public List<Task> AllTaskDatafromToDoDB { get; set; } = new List<Task>();
    }
}
