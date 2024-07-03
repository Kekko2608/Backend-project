select
	avg(freight) as 'Media costo spedizione'
from 
	orders, Customers
where 
	orders.CustomerID = 'Bottm'