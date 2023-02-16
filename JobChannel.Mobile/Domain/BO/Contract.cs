using JobChannel.Mobile.Domain.Base;

namespace JobChannel.Mobile.Domain.BO
{
    public class Contract : BaseEntity<int>
    {
        public string Name { get; set; }
        public string Code { get; set; }

        public Contract() : base(-1)
        {
            Name = string.Empty;
            Code = string.Empty;
        }

        public Contract(int id, string name, string code) : base(id)
        {
            Name = name;
            Code = code;
        }
    }
}