# Softoria_Test
🚀 Running the Database with Docker

To build and run the PostgreSQL database container, use the following command:
docker build -t news-db . | docker run --rm -d --name news-cont -p 5432:5432 -v pgdata:/var/lib/postgresql/18 news-db

📥 First Application Run
During the initial startup, the application will:
Scroll through the entire news feed
Extract all news items
Insert them into the database
This step only happens once.

🔄 Subsequent Runs
On every following launch, the application will:
Check for new news items
Insert only entries that are not already in the database
Avoid inserting duplicates
Continue updating the feed incrementally
This ensures that the database always stays fresh without reprocessing the entire feed.

🧱 Technology Stack
.NET / C#
Playwright (web scraping)
PostgreSQL (database)
Docker (database container)
