using System;
using JobChannel.Mobile.Domain.Contracts;

namespace JobChannel.Mobile.Domain.Responses
{
    public class JobOfferFindResponse : IResponse

    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime PublicationDate { get; set; }
        public string Url { get; set; }
        public string Salary { get; set; }
        public string Experience { get; set; }
        public string Company { get; set; }
        public int Id_Job { get; set; }
        public string Job { get; set; }
        public int Id_Contract { get; set; }
        public string Contract { get; set; }
        public int Id_City { get; set; }
        public string City { get; set; }
        public int Id_Department { get; set; }
        public string Department { get; set; }
        public int Id_Region { get; set; }
        public string Region { get; set; }

        public string Header { get => $"{Id} / {PublicationDate} / {Title} / {Contract} / {Job} / {Region}"; }

        public override string ToString()
        {
            return Title;
        }
    }
}
