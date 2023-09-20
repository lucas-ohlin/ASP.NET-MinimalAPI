using FluentValidation;
using Labb1_MinimalAPI.Models.DTOs;

namespace Labb1_MinimalAPI.Models.Validations {
    public class BookCreateValidations : AbstractValidator<BookCreateDTO> {
        public BookCreateValidations() {

            RuleFor(model => model.Title).NotEmpty();
            RuleFor(model => model.Author).NotEmpty();
            RuleFor(model => model.Year).NotEmpty();

        }
    }
}
