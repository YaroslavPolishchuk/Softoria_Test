CREATE TABLE newsitems (
    id SERIAL PRIMARY KEY,
    title VARCHAR(500) NOT NULL,
    url VARCHAR(1000) NOT NULL,
    createdat TIMESTAMPTZ NOT NULL
);

CREATE INDEX idx_news_items_title
    ON newsitems (title);