CREATE EXTENSION IF NOT EXISTS "pgcrypto";

CREATE TABLE IF NOT EXISTS movies (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    title VARCHAR(50) NOT NULL,
    description TEXT NOT NULL,
    duration INT NOT NULL,
    rating VARCHAR(10),
    poster_url VARCHAR(255)
);