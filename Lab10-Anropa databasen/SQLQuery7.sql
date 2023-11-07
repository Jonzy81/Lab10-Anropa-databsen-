--Hämta alla produkter med deras namn, pris och kategori namn. Sortera på kategori namn och sen produkt namn

select ProductName, UnitPrice, CategoryName from Products
join Categories on products.CategoryID = Categories.CategoryID 
--order by CategoryName asc --This will sort by CategoryName
order by ProductName asc


--Hämta alla kunder och antal ordrar de gjort. Sortera fallande på antal ordrar.
select CompanyName, count(Orders.OrderID) as NumOrders from Customers
join Orders on Orders.CustomerID = Customers.CustomerID
group by CompanyName
order by NumOrders desc

--Hämta alla anställda tillsammans med territorie de har hand om (EmployeeTerritories och Territories tabellerna)
select FirstName, LastName, TerritoryDescription from Employees
join EmployeeTerritories on Employees.EmployeeID= EmployeeTerritories.EmployeeID
join Territories on EmployeeTerritories.TerritoryID = Territories.TerritoryID

--Extra utmaning istället för att skriva antal ordrar, skriv ut summan för deras totala ordervärde
--unitprice * quantity 

select CompanyName, UnitPrice * Quantity * (1 - Discount) as Ordervalue from [Order Details]
join Orders on [Order Details].OrderID = Orders.OrderID 
join Customers on Orders.CustomerID = Customers.CustomerID
order by OrderValue desc;

