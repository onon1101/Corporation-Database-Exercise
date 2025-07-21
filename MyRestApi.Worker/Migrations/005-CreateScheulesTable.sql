
CREATE EXTENSION IF NOT EXISTS "pgcrypto";

CREATE TABLE IF NOT EXISTS schedules (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    movie_id UUID NOT NULL REFERENCES movies(id),
    theater_id UUID NOT NULL REFERENCES theaters(id),
    start_time TIMESTAMP NOT NULL,
    end_time TIMESTAMP NOT NULL
);