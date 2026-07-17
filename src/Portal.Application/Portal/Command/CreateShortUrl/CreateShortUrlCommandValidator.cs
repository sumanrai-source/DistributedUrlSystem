using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Application.Portal.Command.CreateShortUrl
{
    public class CreateShortUrlCommandValidator : AbstractValidator<CreateShortUrlCommand>
    {
        public CreateShortUrlCommandValidator()
        {
            RuleFor(x => x.url)
                .NotEmpty().WithMessage("Url is Required");
        }
    }
}
