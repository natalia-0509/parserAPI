# Parser API

## Opis

Parser API to aplikacja stworzona w technologii **ASP.NET Core (.NET 8)**, której zadaniem jest przetwarzanie danych przesyłanych do endpointu HTTP.

Aplikacja:

- przyjmuje żądania POST w formacie JSON,
- dekoduje zawartość zakodowaną w Base64,
- obsługuje dwa typy danych:
  - CSV,
  - INTERNAL_JSON,
- zwraca ujednoliconą odpowiedź zawierającą status operacji, liczbę przetworzonych rekordów oraz sparsowane dane.

---
## Założenia

Projekt został przygotowany jako rozwiązanie zadania rekrutacyjnego.

W implementacji zastosowano:
- podział na Controller, Service oraz Interface,
- Dependency Injection,
- osobny serwis odpowiedzialny za dekodowanie Base64,
- walidację typu danych,
- obsługę błędów z wykorzystaniem odpowiednich kodów HTTP.

## Wymagania

Przed uruchomieniem aplikacji należy zainstalować:

- .NET 8 SDK lub nowszy
- Visual Studio 2022 / Visual Studio Code (opcjonalnie)

Sprawdzenie zainstalowanej wersji:

```bash
dotnet --version
```

---

## Uruchomienie aplikacji

1. Sklonuj repozytorium:

```bash
git clone </natalia-0509/parserAPI>
```

2. Zbuduj aplikację:

```bash
dotnet restore
```

3. Uruchom aplikację:

```bash
dotnet run
```

Po uruchomieniu aplikacja będzie dostępna pod adresem:

```
https://localhost:5000
```

lub

```
http://localhost:5298
```

---

## Swagger

Po uruchomieniu aplikacji dokumentacja API dostępna jest pod adresem:

```
https://localhost:5000/swagger
```

Swagger umożliwia testowanie endpointów bez używania zewnętrznych narzędzi.

---

## Endpoint

### POST

```
/api/v1/parse-content
```

### Przykładowy request

```json
{
  "Type": "Internal_JSON",
  "Content": "W3sia2V5MSI6ICJ2YWx1ZTEiLCAia2V5MiI6ICJ2YWx1ZTIifSwgeyJrZXkxIjogInZhbHVlMyIsICJrZXkyIjogInZhbHVlNCJ9XQ=="
}
```

```json
{
  "type": "CSV",
  "content": "a2V5MSxrZXkyCmRhdGExLGRhdGEyCmRhdGEzLGRhdGE0CmRhdGE1LGRhdGE2CmRhdGE3LGRhdGE4CmRhdGE5LGRhdGExMA=="
}
```

---

## Obsługiwane typy danych

- CSV
- INTERNAL_JSON

---

## Struktura projektu

```
Controllers/
Interfaces/
Models/
Services/
Program.cs
```

---

## Technologie

- C#
- ASP.NET Core (.NET 8)
- System.Text.Json
- Dependency Injection
- Swagger / OpenAPI