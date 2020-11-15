using Bliss.Recruitment.Data.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Bliss.Recruitment.Application.Questions;
using Bliss.Recruitment.Domain.Framework;
using Bliss.Recruitment.Domain.Questions;
using Bliss.Recruitment.Application.Configuration.Emails;

namespace Bliss.Recruitment.Application.Share.ShareUrlContent
{
    public class ShareUrlContentCommandHandler : IRequestHandler<ShareUrlContentCommand, ShareDto>
    {
        protected readonly IEmailSender _emailSender;
        public ShareUrlContentCommandHandler(IEmailSender emailSender)
        {
            this._emailSender = emailSender;
        }
        public async Task<ShareDto> Handle(ShareUrlContentCommand request, CancellationToken cancellationToken)
        {
            //TODO: FluentValidation
            string contentUrl = String.IsNullOrWhiteSpace(request.ContentUrl) ? throw new ArgumentNullException(nameof(request.ContentUrl)) : request.ContentUrl;
            string destinationEmail = String.IsNullOrWhiteSpace(request.DestinationEmail) ? throw new ArgumentNullException(nameof(request.DestinationEmail)) : request.DestinationEmail;

            EmailMessage message = new EmailMessage(null, destinationEmail, contentUrl);
            await _emailSender.SendEmailAsync(message);

            return new ShareDto("OK");
        }
    }

}
