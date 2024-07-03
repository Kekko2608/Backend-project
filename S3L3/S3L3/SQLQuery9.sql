select 
	sum (Quantity * UnitPrice) as 'totale ordine', OrderID as 'ID'
from [Order Details]
where orderId = 10248
group by OrderID

