using estate_emporium.Models;
using estate_emporium.Models.db;
using estate_emporium.Models.PropertyManager;
using Microsoft.EntityFrameworkCore;

namespace estate_emporium.Services
{
    public class DbService(EstateDbContext dbContext)
    {
        EstateDbContext _dbContext= dbContext;
        public async Task<long> initPropertySaleAsync(PurchaseModel purchaseModel)
        {
            var newSale = new PropertySale
            {
                BuyerId = (long)purchaseModel.BuyerId,

            };
            _dbContext.Add(newSale);
           await _dbContext.SaveChangesAsync();
            return newSale.SaleId;
        }
        public async Task updateWithPropertyResponse(long saleID, GetPropertyResponseModel getPropertyResponseModel)
        {
            var thisSale=await _dbContext.PropertySales.Where(s => s.SaleId==saleID).FirstOrDefaultAsync();
            thisSale.SalePrice = getPropertyResponseModel.price;
            thisSale.PropertyId = getPropertyResponseModel.propertyId;
            await _dbContext.SaveChangesAsync();
        }
        public List<PropertySale> getAllSales()
        {
            return _dbContext.PropertySales.ToList();
        }
        public async Task<PropertySale> getSaleByIdAsync(long saleId)
        {
            return await _dbContext.PropertySales.Where(s => s.SaleId == saleId).FirstOrDefaultAsync();
        }
        public async Task failSaleAsync(long saleID)
        {
            var thisSale = await getSaleByIdAsync(saleID);
            thisSale.StatusId = -1;
            await _dbContext.SaveChangesAsync();
            //Maybe here try back up the chain to tell everyone else it failed?
        }
        public async Task populateHomeLoanId(long saleID, long loanId)
        {
            var thisSale = await getSaleByIdAsync(saleID);
            thisSale.HomeLoanId = loanId;
            thisSale.StatusId += 1;
            await _dbContext.SaveChangesAsync();
        }
        public async Task saveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
