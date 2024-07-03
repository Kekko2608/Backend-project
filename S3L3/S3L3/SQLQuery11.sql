select
	count(orderid) as 'ordini', ShipCity as 'città'
from orders

group by ShipCity