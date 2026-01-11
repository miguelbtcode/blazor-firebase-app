CREATE OR REPLACE PROCEDURE sp_update_product(_id int4, _price numeric, _name text, _description text)
LANGUAGE SQL
AS $BODY$
    UPDATE "Products"
    SET "Price"=_price,
        "Name"=_name,
        "Description"=_description
    WHERE "Id"=_id;
$BODY$;