namespace Hospital.Core.Entities
{
    public class Department : BaseEntity
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();
    }
}
