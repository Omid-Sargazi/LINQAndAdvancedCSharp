-- SELECT p.Name,plph.ListPrice FROM Production.Product p JOIN  Production.ProductListPriceHistory as plph
-- ON p.ProductID=plph.ProductID WHERE plph.EndDate is null

-- SELECT e.BusinessEntityID as EmployeeID,p.FirstName,p.LastName,e.JobTitle,d.Name as DepartmentName FROM HumanResources.Employee e 
-- INNER JOIN Person.Person p ON e.BusinessEntityID=p.BusinessEntityID
-- INNER JOIN HumanResources.EmployeeDepartmentHistory edh ON e.BusinessEntityID=edh.BusinessEntityID
-- INNER JOIN HumanResources.Department d ON edh.DepartmentID=d.DepartmentID WHERE d.Name = 'Research and Development'
SELECT p.LastName+''+p.FirstName as FullName,COUNT(soh.SalesOrderID) as orderCount from Person.Person as p JOIN Sales.SalesOrderHeader as soh on p.BusinessEntityID=soh.CustomerID WHERE soh.OrderDate>='2014-01-01' AND soh.OrderDate<'2015-01-01'
GROUP BY p.LastName,p.FirstName ORDER BY orderCount DESC