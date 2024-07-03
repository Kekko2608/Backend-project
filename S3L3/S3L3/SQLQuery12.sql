select
	avg(freight) as 'media' , ShipCity as 'città'
from orders

group by ShipCity