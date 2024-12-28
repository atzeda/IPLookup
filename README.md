# IPLookup
Retrieve details for a given IP address

IP Lookup Microservice

Endpoint 1: Get IP Details

Request Type: GET 
URL: http://<IP_LOOKUP_SERVICE_URL>/api/IPLookup/{ipAddress}

Description: Fetch IP details for a given IP address.
Example Request: GET http://localhost:5000/api/IPLookup/8.8.8.8

Example Response:
{
  "ip": "8.8.8.8", 
  "city": "Mountain View", 
  "region": "California", 
  "country": "United States" 
}

Batch Processing Microservice

Endpoint 1: Create Batch
Request Type: POST
URL: http://<BATCH_PROCESSING_SERVICE_URL>/api/Batch

Description: Submit a batch of IP addresses for processing.

Example Request:
http
POST http://localhost:5002/api/Batch
Content-Type: application/json

[
  "8.8.8.8",
  "8.8.4.4"
]
Example Response:
json
{
  "batchId": "f47ac10b-58cc-4372-a567-0e02b2c3d479"
}

Endpoint 2: Get Batch Status
Request Type: GET
URL: http://<BATCH_PROCESSING_SERVICE_URL>/api/Batch/{batchId}

Description: Query the status of a batch by its GUID.
Example Request:
GET http://localhost:5002/api/Batch/f47ac10b-58cc-4372-a567-0e02b2c3d479

Example Response (in progress):
json
{
  "batchId": "f47ac10b-58cc-4372-a567-0e02b2c3d479",
  "total": 2,
  "processed": 1,
  "isCompleted": false
}

Example Response (completed):
json
{
  "batchId": "f47ac10b-58cc-4372-a567-0e02b2c3d479",
  "total": 2,
  "processed": 2,
  "isCompleted": true
}
