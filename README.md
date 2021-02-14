# Rehber

Run
----------
```
docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d
```

Create Contact Api
--------------------------
**Url:** http://localhost:5050/api/Contact \
**Request Type:** POST 

**Body:**
```json
{        
    "Name": "Yeşim",
    "Surname": "Türker",
    "CompanyName": "CITS"
}
```

Delete Contact Api
--------------------------
**Url:** http://localhost:5050/api/Contact/{{id}} \
**Sample Url:** http://localhost:5050/api/Contact/e072baed-ffca-485c-a17f-9e0ee3ef29bd \
**Request Type:** DELETE 


Add Contact Info
--------------------------
**Url:** http://localhost:5050/api/Contact/AddContactInfo \
**Request Type:** PUT 

**Body:**
```json
{
    "ContactId":"83902aa9-968f-4f56-ba8a-5396a4f5441f",
    "InfoType": "PhoneNumber",
    "InfoContent":"1111111111"
}
```
Info Type should be "PhoneNumber", "Location" or "Email" 

Delete Contact Info
--------------------------
**Url:** http://localhost:5050/api/Contact/DeleteContactInfo \
**Request Type:** PUT 

**Body:**
```json
{
    "ContactId":"83902aa9-968f-4f56-ba8a-5396a4f5441f",
    "InfoType": "PhoneNumber",
    "InfoContent":"1111111111"
}
```
Info Type should be "PhoneNumber", "Location" or "Email" 

Get Contacts  Api
------------------

**Url:** http://localhost:5050/api/Contact \
**Request Type:** GET 

**Result:**
```json
[
   {
        "id": "7cf7a1c0-09c3-4d1f-a751-9c8bf7395472",
        "name": "Serkan",
        "surname": "Çetintaş",
        "companyName": "CITS"
    }
]
```

Get Contact Details Api
------------------

**Url:** http://localhost:5050/api/Contact/3290f01c-8acd-4dc4-b698-7669e4b8a9fa \
**Request Type:** GET 

**Result:**
```json
{
    "id": "3290f01c-8acd-4dc4-b698-7669e4b8a9fa",
    "name": "Derya",
    "surname": "Tosun",
    "companyName": "Hayat Has.",
    "contactInfos": [
        {
            "infoType": "Location",
            "infoContent": "40.215347825551916, 29.06141544696081"
        },
        {
            "infoType": "PhoneNumber",
            "infoContent": "5442221166"
        },
        {
            "infoType": "PhoneNumber",
            "infoContent": "6442221166"
        }
    ]
}
```

Create Report Request
--------------------------
**Url:** http://localhost:5055/api/ReportRequest \
**Request Type:** POST 

Get Report Requests  Api
------------------

**Url:** http://localhost:5055/api/ReportRequest \
**Request Type:** GET 

**Result:**
```json
[
    {
        "id": "31927722-20e7-4cfa-8239-4e9a9b3ffbf0",
        "requestDate": "2021-02-14T15:28:35.332Z",
        "state": "Preparing"
    },
    {
        "id": "329183a3-baa4-41dd-8ab2-02e532a0c4dc",
        "requestDate": "2021-02-14T15:30:42.334Z",
        "state": "Completed"
    },
    {
        "id": "73b76a9f-3d64-457d-b9a7-0861b6b0ded0",
        "requestDate": "2021-02-14T15:38:24.718Z",
        "state": "Completed"
    },
    {
        "id": "11a8d313-af90-4951-9314-51324a11fee9",
        "requestDate": "2021-02-14T19:05:22.52Z",
        "state": "Completed"
    }
]
```

Get Report Details  Api
------------------

**Url:** http://localhost:5055/api/ReportRequest/329183a3-baa4-41dd-8ab2-02e532a0c4dc \
**Request Type:** GET 

**Result:**
```json
{
    "id": "329183a3-baa4-41dd-8ab2-02e532a0c4dc",
    "requestDate": "2021-02-14T15:30:42.334Z",
    "state": "Completed",
    "reportResults": [
        {
            "location": "40.21537518443339, 29.04801587269219",
            "contactCount": 1,
            "phoneNumberCount": 2
        },
        {
            "location": "40.215347825551916, 29.06141544696081",
            "contactCount": 2,
            "phoneNumberCount": 3
        }
    ]
}
```




