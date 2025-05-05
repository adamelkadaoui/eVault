eVault - Secure Document Archiving 📂
=====================================

**eVault** is an ASP.NET Core API designed to ensure secure electronic document archiving. This project emphasizes data integrity, traceability, and regulatory compliance — ideal for regulated sectors such as banking, insurance, or the public sector.

* * *

🚀 Features
-----------

*   🔐 Electronic archiving with SHA-256 hashing
*   📜 Full audit logging (audit trail)
*   📁 Document organization by user and category
*   📤 Upload and 🧾 view documents
*   ✅ Integrity verification
*   🔧 Integrated Swagger UI for endpoint testing

🛠️ Tech Stack
--------------

*   **.NET 8 / ASP.NET Core Web API**
*   **Entity Framework Core + SQL Server**
*   **Docker**
*   **JWT Auth (optional)**
*   Swagger / OpenAPI

🔧 Run the project locally
--------------------------

    git clone https://github.com/your-username/evault-document-archiver.git
    cd evault-document-archiver
    dotnet restore
    dotnet ef database update
    dotnet run
  

Then access Swagger UI at: [http://localhost:5000/swagger](http://localhost:5000/swagger)
