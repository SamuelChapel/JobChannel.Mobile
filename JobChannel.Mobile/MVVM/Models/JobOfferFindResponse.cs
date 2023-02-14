using Microsoft.Toolkit.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace JobChannel.Mobile.MVVM.Models
{
    public class JobOfferFindRequest
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

    public class JobOfferFindResponse
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

        public override string ToString()
        {
            return Title;
        }
    }

    public class JobOfferFindResponseSource : IIncrementalSource<JobOfferFindResponse>
    {
        private readonly List<JobOfferFindResponse> _jobOffers;

        public JobOfferFindResponseSource()
        {
            _jobOffers = Enumerable.Range(0, 1000).Select(i => new JobOfferFindResponse() { Id = i, Title = "Titre " + i}).ToList();
        }

        public async Task<IEnumerable<JobOfferFindResponse>> GetPagedItemsAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = default)
        {
            var request = new JobOfferFindRequest() { Count = pageSize, Page = pageIndex };

            await Task.Delay(1000);

            return await Task.FromResult(_jobOffers.Skip(pageIndex * pageSize).Take(pageSize));
        }
    }
}
