using JobChannel.Mobile.Domain.Base;

namespace JobChannel.Mobile.Domain.BO
{
    public class Region : BaseEntity<int>
    {
        public string Name { get; set; }
        public string Code { get; set; }

        public Region() : base(-1)
        {
            Name = string.Empty;
            Code = string.Empty;
        }

        public Region(int id, string name, string codeRome) : base(id)
        {
            Name = name;
            Code = codeRome;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}