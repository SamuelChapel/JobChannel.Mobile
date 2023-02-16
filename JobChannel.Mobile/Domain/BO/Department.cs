using JobChannel.Mobile.Domain.Base;

namespace JobChannel.Mobile.Domain.BO
{
    public class Department : BaseEntity<int>
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public Region Region { get; set; }

        public Department() : base(-1)
        {
            Name = string.Empty;
            Code = string.Empty;
            Region = new Region();
        }

        public Department(
            int id,
            string name,
            string codeRome,
            Region region) : base(id)
        {
            Name = name;
            Code = codeRome;
            Region = region;
        }
    }
}