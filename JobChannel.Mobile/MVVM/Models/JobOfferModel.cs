using JobChannel.Mobile.Domain.Requests;
using JobChannel.Mobile.Domain.Responses;
using JobChannel.Mobile.Http;
using JobChannel.Mobile.MVVM.ViewsModels;
using Microsoft.Toolkit.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace JobChannel.Mobile.MVVM.Models
{
    public class JobOfferModel : IIncrementalSource<JobOfferFindResponse>
    {
        public JobOfferModel()
        {
        }

        public async Task<IEnumerable<JobOfferFindResponse>> GetPagedItemsAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = default)
        {
            var request = MainVM.JobOfferFindRequest;
            request.Count = pageSize;
            request.Page = pageIndex + 1;

            return await JobChannelHttpClient.Instance.PostRequest<IEnumerable<JobOfferFindResponse>, JobOfferFindRequest>("api/JobOffer/search", request);
            //return await Task.FromResult(_jobOffers.Skip(pageIndex * pageSize).Take(pageSize));
        }
    }
}
