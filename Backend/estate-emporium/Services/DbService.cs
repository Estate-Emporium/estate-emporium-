using estate_emporium.Models;
using estate_emporium.Models.db;
using estate_emporium.Models.PropertyManager;
using estate_emporium.Utils;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace estate_emporium.Services
{
    public class DbService(EstateDbContext dbContext)
    {
        private readonly EstateDbContext _dbContext = dbContext;

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
            thisSale.SalePrice = getPropertyResponseModel.price+getPropertyResponseModel.price*Consts.Commission/100;
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
            //TODO Maybe here try back up the chain to tell everyone else it failed?
        }
        public async Task populateHomeLoanId(PropertySale thisSale, long loanId)
        {
            thisSale.HomeLoanId = loanId;
            thisSale.StatusId += 1;
            await _dbContext.SaveChangesAsync();
        }
        public async Task<PropertySale> getSalebyLoanId(long loanId)
        {
            return await _dbContext.PropertySales.Where(s => s.HomeLoanId == loanId).FirstOrDefaultAsync();
        }
        public async Task resetData()
        {
            _dbContext.Database.ExecuteSqlRaw("TRUNCATE TABLE PropertySales");
            await _dbContext.SaveChangesAsync();
        }
        public  List<FrontendModel> getFrontData()
        {

            var result= _dbContext.PropertySales.Select(s =>
            new FrontendModel
            {
                SaleId = s.SaleId,
                Status= s.Status.StatusName,
                Price = (s.SalePrice / (1 + Consts.Commission / 100)),
                Commission =  s.SalePrice - (s.SalePrice / (1 + Consts.Commission / 100))
            }).ToList() ;
            return result;
        }
        public async Task saveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
