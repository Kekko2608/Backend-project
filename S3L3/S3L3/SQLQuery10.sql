select 
	count (productId) as 'prodotti', CategoryID as 'categoria'
from 
	Products
group by CategoryID