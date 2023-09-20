using FluentValidation;
using Labb1_MinimalAPI.Models;
using Labb1_MinimalAPI.Models.DTOs;
using Labb1_MinimalAPI.Models.Validations;

namespace Labb1_MinimalAPI.Models.Validations {
    public class BookUpdateValidations : AbstractValidator<BookUpdateDTO> {

        public BookUpdateValidations() {
            RuleFor(model => model.Id).NotEmpty();
        }

    }
}