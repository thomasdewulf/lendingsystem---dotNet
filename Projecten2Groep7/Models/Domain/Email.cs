using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projecten2Groep7.Models.Domain
{
    public class Email
    {
        public int EmailId { get; set;}
        public ReservatieStatus Status { get; set;}
        public String Header { get; set; }
        public String Body { get; set; }
        public String Footer { get; set; }
        public String Subject { get; set; }
        public Email(String header,String body,String footer,String subject,ReservatieStatus status) 
        {
            this.Header = header;
            this.Body = body;
            this.Footer = footer;
            this.Subject = subject;
            this.Status = status;
        }
        public Email()
        {

        }
    }
}