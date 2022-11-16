# invtmgmtsystem
 kiratech

The system should satisfy the following requirements:
a. Have a "Supplier" model that represents a supplier/manufacturer of a product.
b. Have an "Product" model that represents a product. It should contain at least the following fields:
o Name - the name of the product.
o SKU - the amount of available stock.
o Availability - whether any of the stock is available.
o Supplier - the reference to the supplier/manufacturer.
c. Expose a GET API endpoint that lists all available products in JSON format.
d. Expose a POST API endpoint that allows creating a new product.
e. Have an HTML page that displays all available products and meets the following requirements:
o Request the list of products from the API endpoint mentioned in item "c" rather than
directly from the database.
o Have a way to let the user create a new product. Creation of the products should be done
through the API endpoint mentioned in item "d".