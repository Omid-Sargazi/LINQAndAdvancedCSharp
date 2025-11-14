SELECT 
    pc.Name AS ProductCategoryName,
    psc.Name AS ProductSubcategoryName,
    sp.Name AS StateProvinceName,
    SUM(sod.LineTotal) AS TotalSales,
    SUM(sod.OrderQty) AS TotalQuantity,
    COUNT(DISTINCT soh.SalesOrderID) AS OrderCount
FROM Sales.SalesOrderHeader soh
INNER JOIN Sales.SalesOrderDetail sod ON soh.SalesOrderID = sod.SalesOrderID
INNER JOIN Production.Product p ON sod.ProductID = p.ProductID
INNER JOIN Production.ProductSubcategory psc ON p.ProductSubcategoryID = psc.ProductSubcategoryID
INNER JOIN Production.ProductCategory pc ON psc.ProductCategoryID = pc.ProductCategoryID
INNER JOIN Person.Address a ON soh.ShipToAddressID = a.AddressID
INNER JOIN Person.StateProvince sp ON a.StateProvinceID = sp.StateProvinceID
WHERE soh.OrderDate >= '2013-01-01'
    AND pc.Name IN ('Bikes', 'Components')
    AND soh.Status = 5 -- Shipped orders only
GROUP BY 
    pc.Name,
    psc.Name, 
    sp.Name
ORDER BY 
    pc.Name,
    psc.Name,
    TotalSales DESC;