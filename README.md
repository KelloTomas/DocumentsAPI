# Documentation

**Base URL**: `https://localhost:5000/Documents`

## Endpoints

1. **Get Document by ID**
   - **Endpoint**: `/GET/{id}`
   - Retrieve a document by its ID.

2. **Get All Documents**
   - **Endpoint**: `/GETALL`
   - Retrieve all stored documents.

3. **Add or Update Document**
   - **Endpoint**: `/POST`
   - Add a new document or update an existing one from body.

4. **Update Document**
   - **Endpoint**: `/PUT/{id}`
   - Modify an existing document. ID in body will be ignored.

All response type are based on value in Accept header. Supproted:
   - XML
   - JSON

## Sample Usage

- **Get Document by ID**: `GET BaseURL/documents/1`
- **Get All Documents**: `GET BaseURL/documents`
- **Add or Update Document**: `POST BaseURL/documents`
  - Request Body:
    ```json
    {
      "id": "3",
      "tags": ["tag3"],
      "data": {"Key": "Value3"}
    }
    ```
- **Update Document**: `PUT BaseURL/documents/3`
  - Request Body:
    ```json
    {
      "tags": ["tag3", "tag4"],
      "data": {"Key": "UpdatedValue"}
    }
    ```
