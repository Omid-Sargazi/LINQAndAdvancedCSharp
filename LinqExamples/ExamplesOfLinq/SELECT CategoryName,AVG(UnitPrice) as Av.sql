-- SELECT CategoryName,AVG(UnitPrice) as AvePrice FROM Products  as p JOIN Categories as c on p.CategoryID = c.CategoryID GROUP BY CategoryName HAVING AVG(UnitPrice)>28
-- SELECT CategoryName,COUNT(*) NumbersOfCategories FROM Products as p JOIN Categories as c on p.CategoryID = c.CategoryID GROUP BY CategoryName

-- SELECT City,COUNT(CustomerID) NumbersOfCustomer FROM Customers GROUP BY City HAVING COUNT(CustomerID)>2

-- SELECT OrderID,YEAR(OrderDate) as Date,CustomerID FROM Orders WHERE YEAR(OrderDate)=1997 

-- SELECT * from Products WHERE UnitPrice>20 and UnitPrice<50
-- SELECT ProductID,UnitPrice,UnitsInStock from Products WHERE UnitPrice BETWEEN 20 AND 50 and UnitsInStock<20

-- SELECT ProductID,ProductName,UnitPrice,UnitsInStock from Products WHERE UnitPrice BETWEEN 20 and 50 AND UnitsInStock<20

SELECT * FROM Customers WHERE CompanyName LIKE 'q%'