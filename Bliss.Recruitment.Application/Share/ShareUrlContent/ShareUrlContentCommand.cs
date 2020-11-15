using Bliss.Recruitment.Application.Configuration.Commands;
using Bliss.Recruitment.Application.Questions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bliss.Recruitment.Application.Share.ShareUrlContent
{
    public class ShareUrlContentCommand : CommandBase<ShareDto>
    {
        public string DestinationEmail { get; set; }
        public string ContentUrl { get; set; }

        public ShareUrlContentCommand(string destinationEmail, string contentUrl)
        {
            this.DestinationEmail = destinationEmail;
            this.ContentUrl = contentUrl;
        }

    }
}
