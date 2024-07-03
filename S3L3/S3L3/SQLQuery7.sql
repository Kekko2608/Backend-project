select 
	count(customerId) as 'clienti', city as 'città'
from Customers

group by city