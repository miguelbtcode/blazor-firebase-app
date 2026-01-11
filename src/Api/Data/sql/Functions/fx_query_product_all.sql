CREATE OR REPLACE FUNCTION fx_query_product_all()
RETURNS TABLE(id int4, name text, description text, price numeric) AS
$$
BEGIN
    RETURN QUERY SELECT "Id", "Name", "Description", "Price" FROM "Products";
END
$$
LANGUAGE plpgsql;