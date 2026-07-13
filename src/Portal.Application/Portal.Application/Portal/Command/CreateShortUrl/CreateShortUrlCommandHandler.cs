using Akka.Util;
using AutoMapper;
using FluentValidation;
using MediatR;
using Portal.Application.Common;
using Portal.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Application.Portal.Command.CreateShortUrl
{
    public class CreateShortUrlCommandHandler : IRequestHandler<CreateShortUrlCommand, ApiResponse<CreateShortUrlResponse>>
    {

        private readonly IValidator<CreateShortUrlCommand> _validator;
        private readonly IMapper _mapper;
        private readonly IUrlService _urlService;

        public CreateShortUrlCommandHandler(
            IValidator<CreateShortUrlCommand> validator,
            IMapper mapper,
            IUrlService urlService
            )
        {
            _validator = validator;
            _mapper = mapper;
            _urlService = urlService;

        }
        public async Task<ApiResponse<CreateShortUrlResponse>> Handle(CreateShortUrlCommand request, CancellationToken cancellationToken)
        {
            var entityName = typeof(CreateShortUrlCommand).Name
                .Replace("Command", "")
                .Replace("Create", "");

            try
            {
                var validationResult = await _validator.ValidateAsync(request, cancellationToken);
                if(!validationResult.IsValid)
                {
                    var errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                    return ApiResponse<CreateShortUrlResponse>.FailResponse($"Validation failed for {entityName}.", validationResult.Errors.Select(e => e.ErrorMessage).ToList());
                }

                var create = await _urlService.CreateShortUrlAsync(request);

                if(create.Errors.Any())
                {
                    var errors = string.Join(", ", create.Errors);
                    return ApiResponse<CreateShortUrlResponse>.FailResponse($"Failed to create {entityName}.", create.Errors);
                }

                var displayResponse = _mapper.Map<CreateShortUrlResponse>(create.Data);

                return ApiResponse<CreateShortUrlResponse>.SuccessResponse(displayResponse, $"{entityName} created successfully.");

            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
