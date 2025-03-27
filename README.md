https://medium.com/@madu.sharadika/authentication-and-authorization-in-net-web-api-with-jwt-b46ef2f54e31
JWT Authentication

Usage
Once the application is running, you can access the API endpoints using tools like Postman or through a browser.

JWT Authentication
JWT Structure
A JWT consists of three parts (Header, Payload, Signature), separated by dots (.).

Code
HEADER.PAYLOAD.SIGNATURE
Header (Metadata)
Specifies the algorithm and token type:

JSON
{
  "alg": "HS256",
  "typ": "JWT"
}
Payload (Claims)
Contains user data and metadata:

JSON
{
  "sub": "user123",
  "name": "John Doe",
  "role": "Admin",
  "iat": 1711200000,
  "exp": 1711203600
}
sub: Subject (User ID)
name: User’s name
role: User’s role (Admin, User, etc.)
iat: Issued At (timestamp)
exp: Expiry time (timestamp)
Signature (Validation)
Ensures token integrity using the secret key:

Code
HMACSHA256(
  base64UrlEncode(header) + "." + base64UrlEncode(payload),
  secret_key
)
API Endpoints
Here are some of the main API endpoints available in this application:

POST /api/auth/login - Authenticate and receive a JWT token
GET /api/users - Get a list of all users (Admin only)
GET /api/rooms - Get a list of all rooms
POST /api/rooms/book - Book a room
For a complete list of endpoints and their usage, refer to the API documentation.

Contributing
Contributions are welcome! Please fork the repository and create a pull request with your changes. Ensure that your code follows the project's coding standards and includes relevant tests.
