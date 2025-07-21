DO $$
BEGIN
    IF NOT EXISTS (SELECT 1 FROM pg_type WHERE typname = 'reservation_status') THEN
        CREATE TYPE reservation_status AS ENUM ('Pending', 'Paid', 'Cancelled');
    END IF;
END
$$;