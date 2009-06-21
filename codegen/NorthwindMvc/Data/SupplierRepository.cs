using NorthwindWeb.Business;

namespace NorthwindWeb.Data
{
    public class SupplierRepository
    {
        private readonly Context _context;

        public SupplierRepository(Context context)
        {
            _context = context;
        }

        public void Create(Supplier v)
        {
            Supplier r = _context.NorthwindWebService.CreateSupplier(v);

            v.SupplierID = r.SupplierID;
            v.CompanyName = r.CompanyName;
            v.ContactName = r.ContactName;
            v.ContactTitle = r.ContactTitle;
            v.Address = r.Address;
            v.City = r.City;
            v.Region = r.Region;
            v.PostalCode = r.PostalCode;
            v.Country = r.Country;
            v.Phone = r.Phone;
            v.Fax = r.Fax;
            v.HomePage = r.HomePage;
        }

        public Supplier Read(int SupplierID)
        {
            return _context.NorthwindWebService.ReadSupplier(SupplierID);
        }

        public void Update(Supplier v)
        {
            Supplier r = _context.NorthwindWebService.UpdateSupplier(v);

            v.SupplierID = r.SupplierID;
            v.CompanyName = r.CompanyName;
            v.ContactName = r.ContactName;
            v.ContactTitle = r.ContactTitle;
            v.Address = r.Address;
            v.City = r.City;
            v.Region = r.Region;
            v.PostalCode = r.PostalCode;
            v.Country = r.Country;
            v.Phone = r.Phone;
            v.Fax = r.Fax;
            v.HomePage = r.HomePage;
        }

        public void Delete(Supplier v)
        {
            _context.NorthwindWebService.DeleteSupplier(v);
        }

        public Supplier[] Search(string companyName, string contactName, string counrty)
        {
            return _context.NorthwindWebService.SearchSupplier(companyName, contactName, counrty);
        }
    }
}