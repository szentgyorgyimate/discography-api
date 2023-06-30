# Discography API
A simple api to manage bands, their albums, and their members. Using ASP.NET Core WebAPI controllers, Entity Framework Core and SQLite.

## Album Controller usage
### Endpoints
|HTTP Method|Route|Description|
|-----------|-----|-----------|
|**GET**|`api/album`|Get all albums, or an album list filtered by querystring parameters|
|**GET**|`api/album/{id}`|Get an album by id|
|**POST**|`api/album`|Create an album|
|**PUT**|`api/album/{id}`|Update an existing album|
|**DELETE**|`api/album/{id}`|Delete the specified album|

### 1. Get all albums / Get filtered album list
#### Call examples
```
[GET] api/album
[GET] api/album?bandid=1&typeid=1&releasefrom=2023-01-01&releaseto=2023-12-31
```
#### Request parameters
|Name|Value type|Type|Required|
|----|----------|----|--------|
|**bandid**|`integer`|Query string|:x:|
|**typeid**|`integer`|Query string|:x:|
|**releasefrom**|`date`|Query string|:x:|
|**releaseto**|`date`|Query string|:x:|

### 2. Get album
#### Call example
```
[GET] api/album/1
```
#### Request parameters
|Name|Value type|Type|Required|
|----|----------|----|--------|
|**id**|`integer`|Route|:heavy_check_mark:|

### 3. Create album
#### Call example
```
[POST] api/album
```
``` json
{
  "bandId": 1,
  "typeId": 1,
  "name": "Algoritmic madness",
  "releaseDate": "2023-01-01"
}
```
#### Request parameters
|Name|Value type|Type|Required|
|----|----------|----|--------|
|**bandId**|`integer`|Request body|:heavy_check_mark:|
|**typeId**|`integer`|Request body|:heavy_check_mark:|
|**name**|`string`|Request body|:heavy_check_mark:|
|**releaseDate**|`date`|Request body|:heavy_check_mark:|

### 4. Update album
#### Call example
```
[PUT] api/album/1
```
``` json
{
  "typeId": 2,
  "name": "Modified madness",
  "releaseDate": "2023-12-31"
}
```

#### Request parameters
|Name|Value type|Type|Required|
|----|----------|----|--------|
|**id**|`integer`|Route|:heavy_check_mark:|
|**bandId**|`integer`|Request body|:heavy_check_mark:|
|**typeId**|`integer`|Request body|:heavy_check_mark:|
|**name**|`string`|Request body|:heavy_check_mark:|
|**releaseDate**|`date`|Request body|:heavy_check_mark:|

### 5. Delete album
#### Call example
```
[DELETE] api/album/1
```
#### Request parameters
|Name|Value type|Type|Required|
|----|----------|----|--------|
|**id**|`integer`|Route|:heavy_check_mark:|

## Band Controller usage
### Endpoints
|HTTP Method|Route|Description|
|-----------|-----|-----------|
|**GET**|`api/band`|Get all bands|
|**GET**|`api/band/{id}`|Get a band by id|
|**POST**|`api/band`|Create a band|
|**POST**|`api/band/{id}/member`|Add a member to a band|
|**PUT**|`api/band/{id}`|Update an existing band|
|**DELETE**|`api/band/{id}`|Delete the specified band|
|**DELETE**|`api/band/{id}/member`|Delete member from band|

### 1. Get all bands
#### Call example
```
[GET] api/band
```

### 2. Get band
#### Call example
```
[GET] api/band/1
```
#### Request parameters
|Name|Value type|Type|Required|
|----|----------|----|--------|
|**id**|`integer`|Route|:heavy_check_mark:|

### 3. Create band
#### Call example
```
[POST] api/band
```
``` json
{
  "name": "The Algoritmics",
  "genre": "Pop",
  "countryOfOrigin": "Canada",
  "formedIn": 2020
}
```
#### Request parameters
|Name|Value type|Type|Required|
|----|----------|----|--------|
|**name**|`string`|Request body|:heavy_check_mark:|
|**genre**|`string`|Request body|:heavy_check_mark:|
|**countryOfOrigin**|`string`|Request body|:heavy_check_mark:|
|**formedIn**|`integer`|Request body|:heavy_check_mark:|

### 3. Add member to band
#### Call example
```
[POST] api/band/1/member
```
``` json
{
  "memberId": 1
}
```
#### Request parameters
|Name|Value type|Type|Required|
|----|----------|----|--------|
|**id**|`integer`|Route|:heavy_check_mark:|
|**memberId**|`integer`|Request body|:heavy_check_mark:|

### 4. Update band
#### Call example
```
[PUT] api/band/1
```
``` json
{
  "name": "The Algoritmics Changed",
  "genre": "Latin Jazz",
  "countryOfOrigin": "Spain",
  "formedIn": 2019
}
```
#### Request parameters
|Name|Value type|Type|Required|
|----|----------|----|--------|
|**id**|`integer`|Route|:heavy_check_mark:|
|**name**|`string`|Request body|:heavy_check_mark:|
|**genre**|`string`|Request body|:heavy_check_mark:|
|**countryOfOrigin**|`string`|Request body|:heavy_check_mark:|
|**formedIn**|`integer`|Request body|:heavy_check_mark:|

### 5. Delete band
#### Call example
```
[DELETE] api/band/1
```
#### Request parameters
|Name|Value type|Type|Required|
|----|----------|----|--------|
|**id**|`integer`|Route|:heavy_check_mark:|

### 6. Delete member from band
#### Call example
```
[DELETE] api/band/1/member
```
``` json
{
  "memberId": 1
}
```
#### Request parameters
|Name|Value type|Type|Required|
|----|----------|----|--------|
|**id**|`integer`|Route|:heavy_check_mark:|
|**memberId**|`integer`|Request body|:heavy_check_mark:|

## Member Controller usage
### Endpoints
|HTTP Method|Route|Description|
|-----------|-----|-----------|
|**GET**|`api/member`|Get all members|
|**GET**|`api/member/{id}`|Get a member by id|
|**POST**|`api/member/`|Create a member|
|**PUT**|`api/member/{id}`|Update an existing member|
|**DELETE**|`api/member/{id}`|Delete the specified member|

### 1. Get all members
#### Call example
```
[GET] api/member
```

### 2. Get member
#### Call example
```
[GET] api/member/1
```
#### Request parameters
|Name|Value type|Type|Required|
|----|----------|----|--------|
|**id**|`integer`|Route|:heavy_check_mark:|

### 3. Create member
#### Call example
```
[POST] api/member
```
``` json
{
  "name": "Robert (fast fingers) Bell"
}
```
#### Request parameters
|Name|Value type|Type|Required|
|----|----------|----|--------|
|**name**|`string`|Request body|:heavy_check_mark:|

### 3. Update member
#### Call example
```
[PUT] api/member/1
```
``` json
{
  "name": "Robert (slow fingers) Bell"
}
```
#### Request parameters
|Name|Value type|Type|Required|
|----|----------|----|--------|
|**id**|`integer`|Route|:heavy_check_mark:|
|**name**|`string`|Request body|:heavy_check_mark:|

### 5. Delete member
#### Call example
```
[DELETE] api/member/1
```
#### Request parameters
|Name|Value type|Type|Required|
|----|----------|----|--------|
|**id**|`integer`|Route|:heavy_check_mark:|

