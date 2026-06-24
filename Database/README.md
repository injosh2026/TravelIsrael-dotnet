# גיבוי מסד הנתונים

קובץ **`projectTripDB.bak`** — גיבוי SQL Server של `ProjectTripsDB` עם כל נתוני הדוגמה (טיולים, מקומות, ביקורות וכו').

## שחזור ב-SSMS (מומלץ)

1. פתחי **SQL Server Management Studio**
2. התחברי ל-`(localdb)\MSSQLLocalDB`
3. אם קיים DB בשם `ProjectTripsDB` — מחקי אותו (קליק ימני → Delete)
4. קליק ימני על **Databases** → **Restore Database...**
5. **Device** → **Add** → בחרי את `Database/projectTripDB.bak` מתוך ה-repo
6. **Database** → שם: `ProjectTripsDB`
7. **Options** → סמני **Overwrite the existing database**
8. **OK**

## שחזור ב-T-SQL

החליפי את הנתיב לנתיב המלא של הקובץ אצלך:

```sql
RESTORE DATABASE ProjectTripsDB
FROM DISK = N'C:\path\to\TravelIsrael-dotnet\Database\projectTripDB.bak'
WITH REPLACE,
MOVE N'ProjectTripsDB' TO N'C:\Users\<USER>\ProjectTripsDB.mdf',
MOVE N'ProjectTripsDB_log' TO N'C:\Users\<USER>\ProjectTripsDB_log.ldf';
```

> אם שמות ה-logical files שונים, הריצי קודם:
> `RESTORE FILELISTONLY FROM DISK = N'...\projectTripDB.bak'`

## אחרי השחזור

```bash
dotnet run --project ProjectTrips
```

**לא** צריך להריץ `dotnet ef database update` — הגיבוי כבר כולל את כל הטבלאות והנתונים.

**Admin (אם קיים בגיבוי):**

| Email | Password |
|-------|----------|
| `Admin@gmail.com` | `Admin#613` |
