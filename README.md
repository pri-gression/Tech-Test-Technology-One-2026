# Tech-Test-Technology-One-2026

# Number To Words Converter 

A web page featuring a web server routine that converts numerical input into words and passes these words as a string output parameter.

** Example: **
- Input : `123.54`
- Output: `ONE HUNDRED AND TWENTY THREE DOLLARS AND FIFTY FOUR CENTS`

---

# Prerequisities

The following should be installed to be able to run the program:

- [.NET 9 SDK](https://dotnet.microsoft.com/download)
- [Node.js and npm](https://nodejs.org/)

To verify: 

```bash 
dotnet --version 
node --version
npm --version
```
---

# Run the backend API 

Assuming that the git repo has already been cloned on the local machine or the machine its been running on

```bash 
cd NumberToWords.API 
dotnet restore
dotnet run 
```

The API should start on: `http://localhost:5161`

To verify its working: 

```
http://localhost:5161/api/convert?number=123.45
```
# Run the frontend 

Open a new terminal
```bash
cd frontend 
npm start
```

# Run the Tests 

Open a new terminal
```bash
cd NumbersToWords.Tests
dotnet test
```

Expected output:
```
Test summary: total: 13, failed: 0, succeeded: 13
```
---

# Using The App

1. Open `http://localhost:3001` in your browser
2. Enter a currency amount (e.g. `123.45`)
3. Click **Convert** (You know you're doing something right if you hear that sound from the Mario)
4. The result appears below the button

# Valid Inputs

- Positive numbers up to 999 trillion
- Up to 2 decimal places
- Examples: `123.45`, `1000`, `0.50`, `999999999999999`

# Invalid Inputs

- Letters or special characters
- Negative numbers
- More than 2 decimal places
- Multiple decimal points

---

# API Reference

# Convert Number To Words

```
GET /api/convert?number={value}
```

**Success Response:**
```json
{
  "success": true,
  "result": "ONE HUNDRED AND TWENTY THREE DOLLARS AND FORTY FIVE CENTS"
}
```

**Error Response:**
```json
{
  "success": false,
  "error": "Input must only contain digits and a decimal point"
}
```

---

## Tech Stack

- **Backend:** C# / ASP.NET Core 9 
- **Frontend:** React 
- **Testing:** xUnit
- **CI:** GitHub Actions

