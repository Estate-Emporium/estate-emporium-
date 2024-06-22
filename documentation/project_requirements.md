# Design patterns level-up Requirements

## Requirements: 
1. Persona Manager (manage the fake people in this economy and what they spend money on)
  Persona manager is not a company (cannot have a company bank account or stocks)
  Manages all the fake people who interact with the system
  Spawn a starting set of at least 1000 personas
  Personas have a next of kin to inherit
  Personas can marry and have children (there are no genders , personas are sentient amoebas)
  Personas should rent or buy a home
  Personas should eat
  Persona's actions are largely determined by the Hand of Zeus
  Babies grow up and become adults in 6 months
2. Property manager (who owns what)
  Property manager is not a company (cannot have a bank account or stocks)
  Intermediary for all real estate stuff
  Centrally managed contracts for sales, mortgages and rentals
  Spawn a starting set of at least 5000 properties with a capacity of between1 and 8 personas
  Track who owns what
  Hand of Zeus decides on the initial price of a property not yet owned by anyone
  If property is currently not owned, then the sale money (via estate agent) goes to Central revenue service)
3. Retail Bank (serving the personas)
  Only personas can have accounts here
  Debit orders for all payments (except food and electronics)
  Card payments for electronics and food retailers
  Payments have to go between retail and commercial bank (i.e. salary payments and paying for insurance)
4. Commercial bank (serving other businesses, including business loans)
  Only companies ca have accounts here
  Scheduled payments (like salaries)
5. Heath Insurance
  Will pay for healthcare
  All personas have to have a health insurance policy
6. Life insurance
  Will pay out to the next of kin when a persona dies
  All personas have to have a life insurance policy
7. Short term insurance
  All personas have to have a short term insurance policy
  Reimburse for damage dealt to electronics by hand of Zeus
8. Healthcare - Doctor / Hospital / Pharmacy
  Charge the insurer for care
9. Central revenue service
  VAT, property tax, income tax on business and personas
10. Labor broker (everyone should have a job )
  Labor broker has to hire everyone
  Labor broker is funded by all income from the central revenue service
  Labor broker has to pay salaries every 1st of the month
  Increases semi-annually (amount determined by Hand of Zeus)
11. Stock exchange
  Allow personas and companies to buy shares
  All profits from companies are disbursed to share-holders every month
  Each company owns 100% of the 100 000 shares of their company at the start
  You have to list some (or all) of your stock on the stock exchange)
  Disbursing profit at the end of the month should be based on the number of shares owned
12. Real estate agent (property sales)
  Sell properties between companies and personas
  Interact with the Property Manager and Home Loans
13. Real estate agent (rentals)
  Rent properties owed by companies or personas
  Short term lender (lend money to personas)
  Calculate interest daily, compound interest monthly
  Debit orders for repayments
14. Home loans (mortgages)
  Loans money to personas and companies to allow buying property
  Calculate interest daily, compound interest monthly
  Debit orders for repayments
15. Electronics retailer (electronics have to be insured)
  Sells one kind of electronic item
  Each persona 'wants' as many of these electronic items as they can have
16. Food retailer (unhealthy food leads to doctors visits)
  Sells one kind of food item
  If persona is hungry and cannot afford to buy or loa the money they die
17. Hand of Zeus
  (create random events, such as people getting sick, burst pipes, breaking, getting married, having a baby)
  hand of Zeus to determine the purchasing power of a unit of currency (what is an electronic item or food item worth)
  Hand of Zeus determines the prime lending rate (changes monthly)
  Hand of Zeus determines the tax rates (changes yearly)
  Hand of Zeus decides on salary amounts when a person is hired by labor broker