-- SELECT p.Name,plph.ListPrice FROM Production.Product p JOIN  Production.ProductListPriceHistory as plph
-- ON p.ProductID=plph.ProductID WHERE plph.EndDate is null

-- SELECT e.BusinessEntityID as EmployeeID,p.FirstName,p.LastName,e.JobTitle,d.Name as DepartmentName FROM HumanResources.Employee e 
-- INNER JOIN Person.Person p ON e.BusinessEntityID=p.BusinessEntityID
-- INNER JOIN HumanResources.EmployeeDepartmentHistory edh ON e.BusinessEntityID=edh.BusinessEntityID
-- INNER JOIN HumanResources.Department d ON edh.DepartmentID=d.DepartmentID WHERE d.Name = 'Research and Development'
-- SELECT p.LastName+''+p.FirstName as FullName,COUNT(soh.SalesOrderID) as orderCount from Person.Person as p JOIN Sales.SalesOrderHeader as soh on p.BusinessEntityID=soh.CustomerID WHERE soh.OrderDate>='2014-01-01' AND soh.OrderDate<'2015-01-01'
-- GROUP BY p.LastName,p.FirstName ORDER BY orderCount DESC

-- SELECT p.Name,p.ProductNumber,SUM(sod.OrderQty)as TotalSold from Production.Product as p INNER JOIN Sales.SalesOrderDetail as sod on p.ProductID = sod.ProductID INNER JOIN Sales.SalesOrderHeader as soh 
-- on soh.SalesOrderID = sod.SalesOrderID WHERE soh.OrderDate>='2013-01-01' AND soh.OrderDate<='2014-01-01' GROUP BY p.Name,p.ProductNumber HAVING SUM(sod.OrderQty)>=100
-- ORDER BY TotalSold DESC;

-- SELECT * from HumanResources.Employee as e INNER JOIN Person.Person as p on e.BusinessEntityID = p.BusinessEntityID INNER JOIN Sales.SalesOrderHeader as soh 
-- on soh.SalesPersonID = e.BusinessEntityID
-- -- SELECT * FROM Sales.SalesOrderHeader


-- SELECT p.FirstName+' '+p.LastName as FullName,COUNT(soh.SalesOrderID) as OrderCount FROM HumanResources.Employee as e INNER JOIN Person.Person as p on e.BusinessEntityID = p.BusinessEntityID INNER JOIN
-- Sales.SalesOrderHeader as soh on e.BusinessEntityID = soh.SalesPersonID WHERE soh.OrderDate>='2012-01-01' AND soh.OrderDate<='2013-01-01'
-- GROUP BY p.FirstName,p.LastName HAVING COUNT(soh.SalesOrderID)>=5 ORDER BY OrderCount DESC

-- SELECT p.ProductID,p.Name as ProductName, sc.Name as SubcategoryName, pc.Name as CategoryName FROM Production.Product as p JOIN Production.ProductSubcategory as sc ON p.ProductSubcategoryID = sc.ProductSubcategoryID JOIN Production.ProductCategory
-- as pc on pc.ProductCategoryID = sc.ProductCategoryID

-- SELECT p.ProductID,p.Name as ProductName,sc.Name as SubcategoryName,pc.Name as CategoryName FROM Production.Product as p LEFT JOIN Production.ProductSubcategory as sc on p.ProductSubcategoryID = sc.ProductSubcategoryID LEFT JOIN
-- Production.ProductCategory as pc on pc.ProductCategoryID = sc.ProductCategoryID WHERE p.SellStartDate is NOT null ORDER BY pc.Name,sc.Name,p.Name
-- OFFSET 0 ROWS FETCH NEXT 50 ROWs ONLY


WITH ActiveProduct as (
    SELECT ProductID,ProductSubcategoryID from Production.Product WHERE SellStartDate is not null
)
SELECT * FROM Production.ProductCategory