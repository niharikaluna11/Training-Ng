namespace TODOapi.Models
{
    public class TodoItem
    {
        //todo class model containing data
        public int Id { get; set; }
        public string Task { get; set; }
        public bool IsCompleted { get; set; }
    }
}
