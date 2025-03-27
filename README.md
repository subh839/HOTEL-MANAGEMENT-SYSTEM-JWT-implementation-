https://medium.com/@madu.sharadika/authentication-and-authorization-in-net-web-api-with-jwt-b46ef2f54e31
JWT Authentication

🔹 JWT Structure
A JWT consists of three parts (Header, Payload, Signature), separated by dots (.):

css
Copy
Edit
HEADER.PAYLOAD.SIGNATURE
1️⃣ Header (Metadata)
Specifies the algorithm and token type:

json
Copy
Edit
{
  "alg": "HS256",
  "typ": "JWT"
}
2️⃣ Payload (Claims)
Contains user data and metadata:

json
Copy
Edit
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

3️⃣ Signature (Validation)
Ensures token integrity using the secret key:

scss
Copy
Edit
HMACSHA256(
  base64UrlEncode(header) + "." + base64UrlEncode(payload),
  secret_key
)
