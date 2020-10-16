namespace Pharmacy.Customers.Dtos
{
    public class GetAllCustomerForExcelInput
    {
        public string Filter { get; set; }
        public int GenderId { get; set; }
        public int NationalityId { get; set; }
    }
}
