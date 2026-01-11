CREATE OR REPLACE FUNCTION fx_query_product_by_id(in _id int4)
RETURNS TABLE(id int4, name text, description text, price numeric) AS
$$
BEGIN
    RETURN QUERY SELECT "Id", "Name", "Description", "Price" FROM "Products" WHERE "Id" = _id;
END
$$
LANGUAGE plpgsql;