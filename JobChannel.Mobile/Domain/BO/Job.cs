using JobChannel.Mobile.Domain.Base;

namespace JobChannel.Mobile.Domain.BO
{
    public class Job : BaseEntity<int>
{
    public string Name { get; set; }
    public string CodeRome { get; set; }

    public Job() : base(-1)
    {
        Name = string.Empty;
        CodeRome = string.Empty;
    }

    public Job(int id, string name, string codeRome) : base(id)
    {
        Name = name;
        CodeRome = codeRome;
    }
}
}