CREATE OR REPLACE PROCEDURE sp_delete_product(_id int4)
LANGUAGE SQL
AS $BODY$
    DELETE FROM "Products"
    WHERE "Id"=_id;
$BODY$;