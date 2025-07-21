CREATE EXTENSION IF NOT EXISTS "pgcrypto";

CREATE TABLE IF NOT EXISTS reservations (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    user_id UUID NOT NULL REFERENCES users(id),
    schedule_id UUID NOT NULL REFERENCES schedules(id),
    status reservation_status NOT NULL DEFAULT 'Pending',
    reserved_at TIMESTAMP DEFAULT NOW()
);