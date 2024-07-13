cURL Examples:

- Search
```
curl --location 'http://localhost:6000/api/v1/search' \
--header 'Content-Type: application/json' \
--data '{
    "Origin": "Moscow",
    "Destination": "Sochi",
    "OriginDateTime": "2024-02-14"
}'
```

- OnlyCached
```
curl --location 'http://localhost:6000/api/v1/search' \
--header 'Content-Type: application/json' \
--data '{
    "Origin": "Moscow",
    "Destination": "Sochi",
    "OriginDateTime": "2024-02-14",
    "Filters": {
        "OnlyCached": true
    }
}'
```

- Filter
```
curl --location 'http://localhost:6000/api/v1/search' \
--header 'Content-Type: application/json' \
--data '{
    "Origin": "Moscow",
    "Destination": "Sochi",
    "OriginDateTime": "2024-02-14",
    "Filters": {
        "MaxPrice": 600
    }
}'
```
