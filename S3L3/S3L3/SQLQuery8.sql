select 
	sum (Quantity * UnitPrice) as 'totale ordine', OrderID as 'ID'
from [Order Details]

group by OrderID

