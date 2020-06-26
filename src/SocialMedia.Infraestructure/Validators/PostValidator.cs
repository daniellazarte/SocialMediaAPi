using FluentValidation;
using SocialMedia.Core.DTOs;
using System;

namespace SocialMedia.Infraestructure.Validators
{
    public class PostValidator: AbstractValidator<PostDTO>
    {
        public PostValidator()
        {
            RuleFor(post => post.Description)
                .NotNull()
                .Length(10, 200);

            RuleFor(post => post.Date)
                .NotNull()
                .LessThan(DateTime.Now);
        }
    }
}
