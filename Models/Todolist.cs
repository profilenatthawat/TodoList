namespace TodoListApp.Models
{
    public class Todolist
    {
        public int id { get; set; }
        public string? list { get; set; }
        public DateTime finish_date { get; set; }
        public int priority { get; set; }

        public string formatdate(DateTime date){
            string new_date = date.ToString("dd/MM/yyyy") ;
            return new_date;
        }

    }
}