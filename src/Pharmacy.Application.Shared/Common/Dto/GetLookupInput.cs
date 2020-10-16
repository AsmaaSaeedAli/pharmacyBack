namespace Pharmacy.Common.Dto
{
    public class GetLookupInput
    {
        public EnumLookupType LookupType { get; set; }
        public string Filter { get; set; }
    }
}
