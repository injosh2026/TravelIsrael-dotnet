# TravelIsrael — .NET Backend

API של **TravelIsrael** — פלטפורמה לגלישה, תכנון ויצירת טיולים בישראל.

> **Repository זה:** [TravelIsrael-dotnet](https://github.com/injosh2026/TravelIsrael-dotnet)  
> **Frontend (React):** [TravelIsrael-react](https://github.com/injosh2026/TravelIsrael-react)

**פרויקט לימודי** — נבנה בזוגות במסגרת קורס.

---

## ארכיטקטורה

```
┌─────────────────────┐       HTTPS / REST API       ┌─────────────────────┐
│   React Frontend    │  ◄────────────────────────►  │  .NET 8 Backend     │
│   TravelIsrael-react│       localhost:7081/api     │  (Repository זה)    │
└─────────────────────┘                              └──────────┬──────────┘
                                                                │
                                                                ▼
                                                     ┌─────────────────────┐
                                                     │     SQL Server      │
                                                     │   ProjectTripsDB    │
                                                     └─────────────────────┘
```

| שכבה | פרויקט | תיאור |
|------|--------|--------|
| **ProjectTrips** | Web API | Controllers, JWT, Swagger, CORS |
| **Service** | Business logic | DTOs, AutoMapper, TokenService |
| **Repository** | Data access | Entities, Repositories, IContext |
| **ProjectTripsDB** | EF Core | DbContext, Migrations |

---

## דרישות מקדימות

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- **SQL Server** / **LocalDB** — `(localdb)\MSSQLLocalDB`
- [TravelIsrael-react](https://github.com/injosh2026/TravelIsrael-react) — לצד ה-Client (אופציונלי לבדיקות API)

---

## התקנה והרצה

### 1. Clone

```bash
git clone https://github.com/injosh2026/TravelIsrael-dotnet.git
cd TravelIsrael-dotnet
```

### 2. יצירת מסד הנתונים

```bash
dotnet ef database update --project ProjectTripsDB --startup-project ProjectTrips
```

Connection String מוגדר ב-`ProjectTripsDB/Models/ProjectTripsDataBase.cs`.  
אם SQL Server שלך רץ על שרת אחר — עדכני את השורה לפני הרצת Migrations.

**משתמש Admin ראשוני (Seed):**

| שדה | ערך |
|-----|-----|
| Email | `Admin@gmail.com` |
| Password | `Admin#613` |

### 3. הרצת השרת

```bash
dotnet run --project ProjectTrips
```

| כתובת | שימוש |
|-------|--------|
| `https://localhost:7081` | HTTPS (ברירת מחדל ל-Frontend) |
| `http://localhost:5188` | HTTP |
| `/swagger` | תיעוד API (Development) |

### 4. Build

```bash
dotnet build ProjectTrips.sln
```

---

## חיבור ל-Frontend

ב-React (`TravelIsrael-react`) כתובת ה-API מוגדרת ב-`src/services/axios.ts`:

```typescript
const baseURL = 'https://localhost:7081/api';
```

CORS מוגדר ב-`Program.cs` לאפשר `localhost:517x` (Vite).

---

## API Controllers

| Controller | Route | תיאור |
|------------|-------|--------|
| Auth | `api/Auth` | הרשמה, התחברות, Refresh Token |
| User | `api/User` | פרופיל, שינוי סיסמה |
| DayTrip | `api/DayTrip` | טיולים, סינון, המלצות, יצירה |
| Lookups | `api/Lookups` | אזורים, סוגים, קושי, נגישות |
| Suggestions | `api/Suggestions` | הצעות תחנות לבניית מסלול |
| Place | `api/Place` | מקומות |
| Route | `api/Route` | מסלולים |
| Admin | `api/Admin` | סטטיסטיקות וניהול |
| Review | `api/Review` | ביקורות |
| Enum | `api/enums` | ערכי Enum (קושי, נגישות, travel-mode) |

---

## מבנה הפרויקט

```
ProjectTrips/
├── ProjectTrips.sln
├── ProjectTrips/           # ASP.NET Core Web API
│   ├── Controller/
│   ├── Program.cs
│   └── appsettings.json
├── Service/                # שירותים + DTOs
├── Repository/             # Entities + Repositories
└── ProjectTripsDB/         # EF Core + Migrations
```

---

## JWT

הגדרות ב-`ProjectTrips/appsettings.json`:

```json
"Jwt": {
  "Key": "...",
  "Issuer": "ProjectTrips",
  "Audience": "ProjectTrips",
  "ExpireMinutes": 30
}
```

---

## תמיכה

לשאלות או באגים — פתחי **Issue** ב-GitHub.
