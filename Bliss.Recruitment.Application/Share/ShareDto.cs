using System;
using System.Collections.Generic;
using System.Text;

namespace Bliss.Recruitment.Application.Share
{
    public class ShareDto
    {
        public string Status { get; set; }

        public ShareDto(string status)
        {
            this.Status = status;
        }
    }
}
