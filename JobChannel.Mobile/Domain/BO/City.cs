using JobChannel.Mobile.Domain.Base;
using System;
using System.Collections.Generic;

namespace JobChannel.Mobile.Domain.BO
{
    public class City : BaseEntity<int>
{
    public string Name { get; set; }
    public string Code { get; set; }
    public Department Department { get; set; }
    public List<string> Postcodes { get; set; }
    public int Population { get; set; }

    public City() : base(-1)
    {
        Name = string.Empty;
        Code = string.Empty;
        Department = new Department();
        Postcodes = new List<string>();
        Population = 0;
    }

    public City(
        int id,
        string name,
        string codeRome,
        Department department,
        List<string> postcode,
        int population) : base(id)
    {
        Name = name;
        Code = codeRome;
        Department = department;
        Postcodes = postcode;
        Population = population;
    }
}
}