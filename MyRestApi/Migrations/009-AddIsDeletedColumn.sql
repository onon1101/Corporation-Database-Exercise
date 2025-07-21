-- Users
DO $$
BEGIN
    IF NOT EXISTS (
        SELECT 1 FROM information_schema.columns WHERE table_name='users' AND column_name='is_deleted'
    ) THEN
        ALTER TABLE users ADD COLUMN is_deleted BOOLEAN DEFAULT FALSE;
    END IF;
END$$;

-- Movies
DO $$
BEGIN
    IF NOT EXISTS (
        SELECT 1 FROM information_schema.columns WHERE table_name='movies' AND column_name='is_deleted'
    ) THEN
        ALTER TABLE movies ADD COLUMN is_deleted BOOLEAN DEFAULT FALSE;
    END IF;
END$$;

-- Theaters
DO $$
BEGIN
    IF NOT EXISTS (
        SELECT 1 FROM information_schema.columns WHERE table_name='theaters' AND column_name='is_deleted'
    ) THEN
        ALTER TABLE theaters ADD COLUMN is_deleted BOOLEAN DEFAULT FALSE;
    END IF;
END$$;

-- Seats
DO $$
BEGIN
    IF NOT EXISTS (
        SELECT 1 FROM information_schema.columns WHERE table_name='seats' AND column_name='is_deleted'
    ) THEN
        ALTER TABLE seats ADD COLUMN is_deleted BOOLEAN DEFAULT FALSE;
    END IF;
END$$;

-- Scheules
DO $$
BEGIN
    IF NOT EXISTS (
        SELECT 1 FROM information_schema.columns WHERE table_name='schedules' AND column_name='is_deleted'
    ) THEN
        ALTER TABLE schedules ADD COLUMN is_deleted BOOLEAN DEFAULT FALSE;
    END IF;
END$$;

-- Reservations
DO $$
BEGIN
    IF NOT EXISTS (
        SELECT 1 FROM information_schema.columns WHERE table_name='reservations' AND column_name='is_deleted'
    ) THEN
        ALTER TABLE reservations ADD COLUMN is_deleted BOOLEAN DEFAULT FALSE;
    END IF;
END$$;

-- Reservation_seats
DO $$
BEGIN
    IF NOT EXISTS (
        SELECT 1 FROM information_schema.columns WHERE table_name='reservation_seats' AND column_name='is_deleted'
    ) THEN
        ALTER TABLE reservation_seats ADD COLUMN is_deleted BOOLEAN DEFAULT FALSE;
    END IF;
END$$;