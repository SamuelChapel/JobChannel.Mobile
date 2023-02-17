using JobChannel.Mobile.Domain.Requests;
using JobChannel.Mobile.Domain.Responses;
using Microsoft.Toolkit.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace JobChannel.Mobile.MVVM.Models
{
    public class JobOfferModel : IIncrementalSource<JobOfferFindResponse>
    {
        private readonly List<JobOfferFindResponse> _jobOffers;

        public JobOfferModel()
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
