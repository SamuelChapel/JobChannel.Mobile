using JobChannel.Mobile.Domain.Base;
using System;

namespace JobChannel.Mobile.Domain.BO
{
    public class JobOffer : BaseEntity<int>
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime PublicationDate { get; set; }
    public DateTime ModificationDate { get; set; }
    public string Url { get; set; }
    public string Salary { get; set; }
    public string Experience { get; set; }
    public string Company { get; set; }
    public Job Job { get; set; }
    public Contract Contract { get; set; }
    public City City { get; set; }

    public JobOffer() : base(-1)
    {
        Title = string.Empty;
        Description = string.Empty;
        Url = string.Empty;
        Salary = string.Empty;
        Experience = string.Empty;
        Company = string.Empty;
        Job = new Job();
        Contract = new Contract();
        City = new City();
    }

    public JobOffer(
        int id,
        string title,
        string description,
        DateTime publicationDate,
        DateTime modificationDate,
        string url,
        string salary,
        string experience,
        string company,
        Job job,
        Contract contract,
        City city) : base(id)
    {
        Title = title;
        Description = description;
        PublicationDate = publicationDate;
        ModificationDate = modificationDate;
        Url = url;
        Salary = salary;
        Experience = experience;
        Company = company;
        Job = job;
        Contract = contract;
        City = city;
    }
}
}