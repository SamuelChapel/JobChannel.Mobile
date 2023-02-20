using System;
using System.Collections.Generic;
using JobChannel.Mobile.Domain.Contracts;

namespace JobChannel.Mobile.Domain.Requests
{
    public class JobOfferFindRequest : IRequest
    {
        public int Count { get; set; }
        public int Page { get; set; }
        public IEnumerable<int> Id_Region { get; set; }
        public IEnumerable<int> Id_Department { get; set; }
        public IEnumerable<int> Id_City { get; set; }
        public IEnumerable<int> Id_Job { get; set; }
        public IEnumerable<int> Id_Contract { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string SearchString { get; set; }
    }
}
