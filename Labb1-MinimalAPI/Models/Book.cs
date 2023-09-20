using System.ComponentModel.DataAnnotations;

namespace Labb1_MinimalAPI.Models {
    public class Book {

        [Key]
        public Guid Id { get; set; } 
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public DateTime? Year { get; set; }
        public bool IsLoanAble { get; set; }

    }
}
