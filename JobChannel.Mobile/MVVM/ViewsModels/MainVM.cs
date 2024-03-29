﻿using JobChannel.Mobile.Domain.Base;
using JobChannel.Mobile.Domain.BO;
using JobChannel.Mobile.Domain.Requests;
using JobChannel.Mobile.Domain.Responses;
using JobChannel.Mobile.Http;
using JobChannel.Mobile.MVVM.Models;
using Microsoft.Toolkit.Uwp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml.Controls;

namespace JobChannel.Mobile.MVVM.ViewsModels
{
    public class MainVM : ViewModelBase
    {
        private readonly Page associatedPage;
        public string Titre { get; set; }

        public static JobOfferFindRequest JobOfferFindRequest { get; private set; }

        public IncrementalLoadingCollection<JobOfferModel, JobOfferFindResponse> JobOffers = new IncrementalLoadingCollection<JobOfferModel, JobOfferFindResponse>(30);

        public ObservableCollection<Region> Regions = new ObservableCollection<Region>();
        public ObservableCollection<Region> SelectedRegions = new ObservableCollection<Region>();
        public ObservableCollection<Region> SearchRegions = new ObservableCollection<Region>();

        public ObservableCollection<Contract> Contracts = new ObservableCollection<Contract>();
        public ObservableCollection<Contract> SelectedContracts = new ObservableCollection<Contract>();
        public ObservableCollection<Contract> SearchContracts = new ObservableCollection<Contract>();

        public ObservableCollection<Job> Jobs = new ObservableCollection<Job>();
        public ObservableCollection<Job> SelectedJobs = new ObservableCollection<Job>();
        public ObservableCollection<Job> SearchJobs = new ObservableCollection<Job>();

        public DateTime dateMin;
        public DateTime dateMax;

        private readonly ApplicationDataContainer _settings;

        public MainVM(Page page)
        {
            associatedPage = page;
            _settings = ApplicationData.Current.LocalSettings;
            JobOfferFindRequest = new JobOfferFindRequest();

            var taskRegions = Task.Run(() => RefreshRegions());
            var taskContracts = Task.Run(() => RefreshContracts());
            var taskJobs = Task.Run(() => RefreshJobs());

            try
            {
                string filters = _settings.Values["Filter"]?.ToString();
                if (!string.IsNullOrEmpty(filters))
                {
                    var jobOfferFindRequest = JsonSerializer.Deserialize<JobOfferFindRequest>(filters);

                    if (jobOfferFindRequest != null)
                    {
                        Task.WhenAll(taskRegions, taskContracts, taskJobs).Wait();

                        JobOfferFindRequest = jobOfferFindRequest;

                        JobOfferFindRequest.Id_Region?.ToList().ForEach(rId =>
                        {
                            SelectedRegions.Add(Regions.First(r => r.Id == rId));
                        });

                        JobOfferFindRequest.Id_Contract?.ToList().ForEach(cId =>
                        {
                            SelectedContracts.Add(Contracts.First(c => c.Id == cId));
                        });

                        JobOfferFindRequest.Id_Job?.ToList().ForEach(jId =>
                        {
                            SelectedJobs.Add(Jobs.First(j => j.Id == jId));
                        });

                        dateMin = JobOfferFindRequest.StartDate ?? DateTime.Now.AddMonths(-6);
                        dateMax = JobOfferFindRequest.EndDate ?? DateTime.Now;
                    }
                }

            }
            catch (Exception)
            {
            }
        }

        public async void Test()
        {
            Windows.Globalization.ApplicationLanguages.PrimaryLanguageOverride = Windows.Globalization.ApplicationLanguages.PrimaryLanguageOverride switch
            {
                "fr_FR" => "en-US",
                "en-US" => "fr-FR",
                _ => "fr-FR"
            };

            await Task.Delay(50);

            associatedPage.Frame.Navigate(typeof(MainPage));
        }

        public async Task RefreshRegions()
        {
            var regions = await JobChannelHttpClient.Instance.GetRequest<IEnumerable<Region>>("api/Region");

            regions?.ToList().ForEach(region => Regions.Add(region));
        }

        public void RefreshSuggestedRegions(string text = null)
        {
            SearchRegions.Clear();

            var regions = Regions.Except(SelectedRegions);

            if (!string.IsNullOrEmpty(text))
            {
                var textLower = text.Trim().ToLower();
                regions = regions.Where(r => r.Name.ToLower().Contains(textLower)).ToList();
            }

            regions.ToList().ForEach(region => SearchRegions.Add(region));
        }

        public async Task RefreshContracts()
        {
            var contracts = await JobChannelHttpClient.Instance.GetRequest<IEnumerable<Contract>>("api/Contract");

            contracts?.ToList().ForEach(contract => Contracts.Add(contract));
        }

        public void RefreshSuggestedContracts(string text = null)
        {
            SearchContracts.Clear();

            var contracts = Contracts.Except(SelectedContracts);

            if (!string.IsNullOrEmpty(text))
            {
                var textLower = text.Trim().ToLower();
                contracts = contracts.Where(j => j.Name.ToLower().Contains(textLower) || j.Code.ToLower().Contains(textLower)).ToList();
            }

            contracts.ToList().ForEach(contract => SearchContracts.Add(contract));
        }

        public async Task RefreshJobs()
        {
            var jobs = await JobChannelHttpClient.Instance.GetRequest<IEnumerable<Job>>("api/Job");

            jobs?.ToList().ForEach(job => Jobs.Add(job));
        }

        public void RefreshSuggestedJobs(string text = null)
        {
            SearchJobs.Clear();

            var jobs = Jobs.Except(SelectedJobs);

            if (!string.IsNullOrEmpty(text))
            {
                var textLower = text.Trim().ToLower();
                jobs = jobs.Where(j => j.Name.ToLower().Contains(textLower) || j.CodeRome.ToLower().Contains(textLower)).ToList();
            }

            jobs.ToList().ForEach(job => SearchJobs.Add(job));
        }

        public void SearchJobOffers()
        {
            JobOfferFindRequest.Id_Region = SelectedRegions.Count > 0 ? SelectedRegions.Select(r => r.Id).ToList() : null;
            JobOfferFindRequest.Id_Contract = SelectedContracts.Count > 0 ? SelectedContracts.Select(c => c.Id).ToList() : null;
            JobOfferFindRequest.Id_Job = SelectedJobs.Count > 0 ? SelectedJobs.Select(j => j.Id).ToList() : null;
            JobOfferFindRequest.StartDate = dateMin.Date;
            JobOfferFindRequest.EndDate = dateMax.Date;

            while (JobOffers.IsLoading)
            {
                Task.Delay(100);
            }

            JobOffers.RefreshAsync();
        }

        public void SaveFilters()
        {
            if (_settings != null)
            {
                _settings.Values["Filter"] = JsonSerializer.Serialize(JobOfferFindRequest);
            }

            SearchJobOffers();
        }

        internal void ResetFilters()
        {
            JobOfferFindRequest = new JobOfferFindRequest();

            SelectedContracts.Clear();
            SelectedJobs.Clear();
            SelectedRegions.Clear();

            SearchJobOffers();
        }
    }
}
