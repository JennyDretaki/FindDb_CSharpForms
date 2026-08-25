# FindDb – Database Search & Exploration Tool

## Overview

**FindDb** is a C# Windows Forms application designed to simplify the exploration of large Microsoft SQL Server databases.

The application was developed to address a common challenge when working with complex database environments containing a large number of tables, columns, and records: quickly locating relevant database objects without manually navigating through the entire schema.

FindDb provides a centralized interface that allows users to search across multiple databases, identify tables and columns using both exact and approximate keyword matching, inspect matching records, preview generated SQL queries, and review previous searches.

The application currently supports searches across the **DEV** and **CTCOLLECT** databases, individually or simultaneously.

---

## Purpose

Working with large databases can make locating a specific table, field, or value time-consuming, particularly when the exact database object name is unknown.

For example, a developer may remember that a field is related to:

```text
payment code
```

but may not know whether the actual database field is named:

```text
PaymentCode
KodikosPliromis
CustomerPaymentCode
PaymentIdentifier
```

FindDb is designed to reduce this search time by combining SQL Server metadata inspection with fuzzy string matching.

Instead of requiring an exact object name, the application can rank potentially relevant results based on their similarity to the entered keywords.

---

## Main Features

### Multi-Database Search

Users can select the environment in which the search should be executed:

- **DEV**
- **CTCOLLECT**
- **Both databases simultaneously**

Each result clearly indicates the database from which it originated.

---

### Table Search

FindDb searches SQL Server metadata for table names matching the provided keywords.

The application uses information from SQL Server system catalogs such as:

```sql
sys.tables
sys.schemas
```

This allows table discovery without scanning the actual table contents.

Example:

```text
Search: customer payment
```

Possible results:

```text
CustomerPayments
CustomerPaymentHistory
PaymentCustomers
CustomerTransactionHistory
```

---

### Column Search

The application can search all available database columns and display the table and schema to which each matching column belongs.

SQL Server metadata including:

```sql
sys.columns
sys.tables
sys.schemas
sys.types
```

is used to discover the database structure.

Example result:

```text
Database: DEV
Schema: dbo
Table: Payments
Column: KodikosPliromis
```

---

### Record Search

FindDb can optionally search actual database records.

Record searches are performed primarily against textual SQL Server data types such as:

```text
varchar
nvarchar
char
nchar
text
ntext
```

To protect performance when working with large databases, several limits are applied, including:

- Maximum number of returned records
- Maximum matches per column
- SQL command timeout
- Search cancellation support
- Restricted record-search data types

Record searching is intentionally optional because full-text scans across very large databases can be significantly more expensive than metadata searches.

---

## Fuzzy Search

One of the key features of FindDb is approximate string matching.

The application does not rely only on exact SQL matches.

It uses:

- Normalized string comparison
- Partial matches
- Multi-keyword matching
- Levenshtein distance
- Similarity scoring

This allows FindDb to return potentially relevant objects even when the search term does not exactly match the database object name.

For example:

```text
Search:
kod plirom
```

may identify:

```text
KodikosPliromis
```

as a strong match.

Similarly:

```text
customer payment
```

can match:

```text
CustomerPaymentCode
Customer_Payment_History
CustomerPayments
```

---

## Similarity Scoring

Each matching database object receives a similarity score ranging from:

```text
0% – 100%
```

Higher scores indicate stronger matches.

| Database | Type | Table | Column | Match | Score |
|---|---|---|---|---|---:|
| DEV | Column | Payments | KodikosPliromis | KodikosPliromis | 97% |
| DEV | Table | CustomerPayments | - | CustomerPayments | 92% |
| CTCOLLECT | Column | Transactions | PaymentCode | PaymentCode | 86% |

Users can configure a **minimum similarity threshold** to filter less relevant results.

---

## SQL Preview

Selecting a result automatically generates an SQL preview.

For a table result:

```sql
SELECT TOP (100) *
FROM [dbo].[Payments];
```

For a column result:

```sql
SELECT TOP (100)
    [PaymentCode]
FROM [dbo].[Payments];
```

For a record match:

```sql
SELECT TOP (100) *
FROM [dbo].[Payments]
WHERE CONVERT(NVARCHAR(4000), [PaymentCode])
      LIKE N'%payment%';
```

The generated SQL can be copied directly to the clipboard for use in SQL Server Management Studio or another database client.

---

## Data Preview

FindDb allows users to preview the contents of a selected table directly inside the application.

The preview window displays up to a configured number of records in a `DataGridView`.

Additional functionality includes:

- Copy selected cell
- Copy selected row
- View column names
- Horizontal navigation through large tables

This makes it possible to inspect a table before opening it manually in another database management application.

---

## Search History

Each executed search is stored locally.

The history contains:

- Search keywords
- Selected database
- Search type
- Date and time

Example:

| Date | Search | Database | Search In |
|---|---|---|---|
| 25/08/2026 12:30 | payment code | BOTH | Tables, Columns |
| 25/08/2026 12:22 | customer id | DEV | Columns |
| 25/08/2026 12:10 | invoice | CTCOLLECT | Tables, Columns, Records |

Users can:

- Run a previous search again
- Delete individual history entries
- Clear the complete search history

Search history is stored locally in JSON format.

---

## Search Cancellation

Database operations are asynchronous and support cancellation through `CancellationToken`.

This prevents the interface from becoming unnecessarily blocked during longer searches.

The user can cancel an active operation by selecting:

```text
Cancel
```

This is particularly useful when performing record-level searches across large database environments.

---

## User Interface

The application is implemented using **Windows Forms**.

The main interface is divided into several functional areas:

### Search Area

Contains:

- Keyword input
- Database selection
- Search type selection
- Minimum similarity configuration
- Search button
- Cancel button
- History button

### Results Area

Displays:

- Database
- Result type
- Schema
- Table
- Column
- Match
- Similarity score

### SQL Preview Area

Displays the SQL query associated with the selected result and provides actions such as:

```text
Copy SQL
Preview Data
```

### Status Area

Displays information such as:

```text
Ready
Searching DEV...
Searching CTCOLLECT...
125 results found.
Search cancelled.
```

---

## Project Architecture

The project follows a simple separation of responsibilities.

```text
FindDb
│
├── Models
│   ├── SearchResult.cs
│   └── SearchHistoryItem.cs
│
├── Services
│   ├── DatabaseSearchService.cs
│   ├── FuzzySearch.cs
│   └── HistoryService.cs
│
├── DatabaseSettings.cs
│
├── MainForm.cs
├── MainForm.Designer.cs
│
├── HistoryForm.cs
├── HistoryForm.Designer.cs
│
├── DataPreviewForm.cs
├── DataPreviewForm.Designer.cs
│
└── Program.cs
```

---

## Components

### DatabaseSearchService

Responsible for communication with SQL Server.

Main responsibilities include:

- Retrieving table metadata
- Retrieving column metadata
- Searching supported record fields
- Generating SQL previews
- Retrieving table previews
- Applying result limits
- Handling database timeouts

### FuzzySearch

Responsible for determining how closely a database object matches the user's search terms.

The service implements:

- Input normalization
- Partial-string matching
- Keyword matching
- Levenshtein distance
- Similarity score calculation

### HistoryService

Responsible for persistent local search history.

The history is serialized using JSON and stored in the user's local application data directory.

### SearchResult

Represents an individual database search result.

Typical properties include:

```text
Database
Schema
Table
Column
Type
Match
Similarity
PreviewSql
```

---

## Technologies

The project uses:

- **C#**
- **.NET 8**
- **Windows Forms**
- **Microsoft SQL Server**
- **Microsoft.Data.SqlClient**
- **ADO.NET**
- **LINQ**
- **JSON Serialization**
- **Async/Await**
- **CancellationToken**
- **Levenshtein Distance Algorithm**

---

## Database Connection

Database connections are configured through:

```text
DatabaseSettings.cs
```

Example using Windows Authentication:

```csharp
public static string DevConnectionString =
    @"Server=YOUR_SERVER;
      Database=DEV;
      Trusted_Connection=True;
      TrustServerCertificate=True;";
```

Example using SQL Server Authentication:

```csharp
public static string DevConnectionString =
    @"Server=YOUR_SERVER;
      Database=DEV;
      User Id=YOUR_USERNAME;
      Password=YOUR_PASSWORD;
      TrustServerCertificate=True;";
```

> **Security Notice:** Real database usernames, passwords, server addresses, and production connection strings should never be committed to a public GitHub repository.

For production usage, connection strings should preferably be moved to a secure configuration mechanism or environment variables.

---

## Performance Considerations

FindDb was designed with large databases in mind.

Metadata searches are significantly less expensive because they query SQL Server's system catalogs rather than scanning application data.

Record search requires additional consideration.

A query such as:

```sql
WHERE ColumnName LIKE '%keyword%'
```

may require SQL Server to scan a large amount of data, especially when the searched column does not have an appropriate index.

For this reason, FindDb implements several safeguards:

- Maximum metadata results
- Maximum record results
- Maximum results per column
- Command timeout
- Cancellation support
- Text-column filtering

For extremely large production environments, further optimization could include SQL Server Full-Text Search or a dedicated indexed search layer.

---

## Future Improvements

Potential future enhancements include:

- Database metadata caching
- Faster startup indexing
- Full-Text Search integration
- Search within a selected table only
- Search within a selected column only
- Schema filters
- Data-type filters
- Result sorting and advanced filtering
- Primary and foreign key visualization
- Table relationship exploration
- SQL query execution directly from the preview window
- Export results to CSV or Excel
- Search favorites
- Saved queries
- Search statistics
- Dark mode
- Improved syntax highlighting for SQL previews
- Role-based database access
- Secure external configuration of connection strings

A particularly useful future enhancement would be a two-stage record search workflow:

```text
1. Locate relevant table/column through metadata search
2. Search records only inside the selected object
```

This would provide significantly better performance when working with databases containing millions of records.

---

## Use Case

A typical workflow might look like:

```text
1. Enter:
   "payment code"

2. Select:
   DEV + CTCOLLECT

3. Select:
   Tables + Columns

4. Run Search

5. Review similarity-ranked results

6. Select:
   dbo.Payments.PaymentCode

7. Review generated SQL

8. Preview table data

9. Copy SQL query if required
```

This workflow can significantly reduce the time required to understand an unfamiliar or very large database schema.

---

## Installation

### Requirements

- Windows 10/11
- .NET 8 SDK or Runtime
- Microsoft SQL Server access
- Visual Studio 2022 or newer recommended

### Setup

Clone the repository:

```bash
git clone <repository-url>
```

Open the solution in Visual Studio.

Install the required NuGet dependency:

```text
Microsoft.Data.SqlClient
```

Configure the database connection strings in:

```text
DatabaseSettings.cs
```

Then build and run the application.

---

## Status

The project is currently under active development.

The current implementation provides the core database discovery functionality, including metadata search, fuzzy matching, record searching, SQL preview, data preview, and search history.

Further performance optimization and additional database exploration tools are planned for future versions.

---

## Author

Developed as an internal database productivity and exploration tool using **C#**, **.NET**, **WinForms**, and **Microsoft SQL Server**.
