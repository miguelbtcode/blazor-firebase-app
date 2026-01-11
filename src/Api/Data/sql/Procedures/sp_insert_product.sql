CREATE OR REPLACE PROCEDURE sp_insert_product(_price numeric, _name text, _description text)
LANGUAGE SQL
AS $BODY$
    INSERT INTO "Products"("Price", "Name", "Description")
    VALUES(_price, _name, _description);
$BODY$;