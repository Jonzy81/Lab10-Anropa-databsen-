--H�mta alla produkter med deras namn, pris och kategori namn. Sortera p� kategori namn och sen produkt namn

select ProductName, UnitPrice, CategoryName from Products
join Categories on products.CategoryID = Categories.CategoryID 
--order by CategoryName asc --This will sort by CategoryName
order by ProductName asc


--H�mta alla kunder och antal ordrar de gjort. Sortera fallande p� antal ordrar.
select CompanyName, count(Orders.OrderID) as NumOrders from Customers
join Orders on Orders.CustomerID = Customers.CustomerID
group by CompanyName
order by NumOrders desc

--H�mta alla anst�llda tillsammans med territorie de har hand om (EmployeeTerritories och Territories tabellerna)
select FirstName, LastName, TerritoryDescription from Employees
join EmployeeTerritories on Employees.EmployeeID= EmployeeTerritories.EmployeeID
join Territories on EmployeeTerritories.TerritoryID = Territories.TerritoryID

--Extra utmaning ist�llet f�r att skriva antal ordrar, skriv ut summan f�r deras totala orderv�rde
--unitprice * quantity 

select CompanyName, UnitPrice * Quantity * (1 - Discount) as Ordervalue from [Order Details]
join Orders on [Order Details].OrderID = Orders.OrderID 
join Customers on Orders.CustomerID = Customers.CustomerID
order by OrderValue desc;

