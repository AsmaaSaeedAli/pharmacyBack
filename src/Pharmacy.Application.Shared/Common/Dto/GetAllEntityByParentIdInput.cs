namespace Pharmacy.Common.Dto
{
   public class GetAllEntityByParentIdInput
    {
        public int? ParentId { get; set; }
        public string Filter { get; set; }
    }
}
