-- SELECT CategoryName,AVG(UnitPrice) as AvePrice FROM Products  as p JOIN Categories as c on p.CategoryID = c.CategoryID GROUP BY CategoryName HAVING AVG(UnitPrice)>28
SELECT CategoryName,COUNT(*) NumbersOfCategories FROM Products as p JOIN Categories as c on p.CategoryID = c.CategoryID GROUP BY CategoryName

SELECT City,COUNT(CustomerID) NumbersOfCustomer FROM Customers GROUP BY City HAVING COUNT(CustomerID)>2

