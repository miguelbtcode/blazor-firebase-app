CREATE OR REPLACE FUNCTION fx_query_product_by_name(in _name text)
RETURNS TABLE(id int4, name text, description text, price numeric) AS
$$
BEGIN
    RETURN QUERY SELECT "Id", "Name", "Description", "Price" FROM "Products" WHERE "Name" LIKE '%' || _name || '%';
END
$$
LANGUAGE plpgsql;