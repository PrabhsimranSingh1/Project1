# User Stories (Week 2)

These user stories are written to match the planned report features for the Population Reporting System (C# + MySQL world database).

## US-01: Countries ordered by population
- As a user, I want to view all countries ordered by population (largest first) so that I can compare country populations quickly.
- Acceptance criteria:
  - Returns a list of all countries sorted by population descending.
  - Output columns: Country, Population.
  - If no data is available, show a clear message and return to the menu.

## US-02: Cities ordered by population
- As a user, I want to view all cities ordered by population (largest first) so that I can identify the most populated cities.
- Acceptance criteria:
  - List of cities sorted by population descending.
  - Output columns: City, Country Code, District, Population.
  - Handles DB connection/query errors with a user-friendly message.

## US-03: Capital cities ordered by population
- As a user, I want to view capital cities ordered by population (largest first) so that I can compare the largest capitals.
- Acceptance criteria:
  - Only capital cities are included.
  - Sorted by population descending.
  - Output columns: Capital City, Country, Population.

## US-04: Top N countries by population (world)
- As a user, I want to enter N and view the top N countries by population so that I can quickly see the biggest countries.
- Acceptance criteria:
  - Prompts for N.
  - N must be a positive integer; invalid input is rejected with guidance.
  - Sorted descending and limited to N rows.
  - Output columns: Country, Population.

## US-05: Top N cities by population (world)
- As a user, I want to enter N and view the top N cities by population so that I can quickly see the biggest cities.
- Acceptance criteria:
  - Prompts for N and validates it.
  - Output columns: City, Country Code, District, Population.
  - Sorted descending and limited to N rows.

## US-06: Population breakdown by continent
- As a user, I want to view population totals by continent so that I can understand population distribution across continents.
- Acceptance criteria:
  - Output columns: Continent, Total Population.
  - Sorted descending by total population.
  - Totals match the database values.

## US-07: Population breakdown by region
- As a user, I want to view population totals by region so that I can compare regions within continents.
- Acceptance criteria:
  - Output columns: Region, Total Population.
  - Sorted descending by total population.

## US-08: City vs non-city population for a country
- As a user, I want to view a country’s population split into city and non-city population so that I can understand urbanization.
- Acceptance criteria:
  - Prompts for country (name or code).
  - Output: Country, Total Population, City Population, Non-City Population.
  - Non-city population = Total - City population.
  - Handles unknown country input gracefully.

## US-09: Language speaker statistics
- As a user, I want to view speaker statistics for major languages so that I can understand language prevalence globally.
- Acceptance criteria:
  - Includes at least: Chinese, English, Hindi, Spanish, Arabic.
  - Output: Language, Estimated Speakers, % of World Population.
  - Computed from the database language percentage data.
