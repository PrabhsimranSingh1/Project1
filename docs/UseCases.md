# Use Cases (Week 2)

## UC-01: Generate report — Countries ordered by population
- Primary actor: User
- Preconditions: MySQL running; world DB available; app can connect.
- Trigger: User selects “Countries by population”.
- Main flow:
  1. System shows menu.
  2. User selects report.
  3. System queries DB sorted by population desc.
  4. System formats and displays results.
  5. System returns to menu.
- Alternate flows:
  - DB connection fails → show error → return to menu.
  - No rows → show “No data” → return to menu.
- Postconditions: Report displayed or error shown.

## UC-02: Generate report — Top N cities by population (world)
- Primary actor: User
- Preconditions: DB connection available.
- Trigger: User selects “Top N cities (world)”.
- Main flow:
  1. System shows menu.
  2. User selects report.
  3. System prompts for N.
  4. User enters N.
  5. System validates N.
  6. System queries DB (ORDER BY population DESC LIMIT N).
  7. System displays results.
  8. System returns to menu.
- Alternate flows:
  - Invalid N → show guidance → re-prompt.
  - DB error → show error → return to menu.
- Postconditions: Top-N report displayed.

## UC-03: Generate report — Population by continent
- Primary actor: User
- Preconditions: DB connection available.
- Trigger: User selects “Population by continent”.
- Main flow:
  1. System shows menu.
  2. User selects report.
  3. System queries totals grouped by continent.
  4. System displays results.
  5. System returns to menu.
- Alternate flows:
  - DB error → show error → return to menu.
- Postconditions: Continent totals displayed.

## UC-04: Generate report — Language statistics
- Primary actor: User
- Preconditions: DB connection available.
- Trigger: User selects “Language statistics”.
- Main flow:
  1. System shows menu.
  2. User selects report.
  3. System queries language speaker estimates.
  4. System displays results.
  5. System returns to menu.
- Alternate flows:
  - DB error → show error → return to menu.
- Postconditions: Language stats displayed.
