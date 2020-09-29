namespace PhoneRegistryDDD.Helpdesk.Core.ValueObjects
{
    public class EmployeeHrInfo
    {
        public string Position { get; }
        public string Department { get; }

        public EmployeeHrInfo(string department, string position)
        {
            Position = position;
            Department = department;
        }
    }
}
