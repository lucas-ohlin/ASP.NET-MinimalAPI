namespace Labb1_MinimalAPI.Models.DTOs {
    public class BookUpdateDTO {

        public Guid Id { get; set; } 
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public bool IsLoanAble { get; set; }

    }
}
