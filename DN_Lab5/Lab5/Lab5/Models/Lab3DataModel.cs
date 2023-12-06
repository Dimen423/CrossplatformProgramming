namespace Lab5.Models
{
    public class Lab3DataModel
    {
        public string Connections { get; set; } // Зв'язки у форматі "1 2,2 3,3 4"
        public string Restrictions { get; set; } // Обмеження у форматі "IB,IV,IB"
        public string ErrorValue { get; set; }
        public string Response { get; set; }
        public string Calculated { get; set; }
    }
}
