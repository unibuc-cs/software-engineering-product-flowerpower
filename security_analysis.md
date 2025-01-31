**Security analysis Blinq24/1**

- **Application Components Identification**

**Main Components**

- **Frontend**:

  - **Technology**: Angular

  - **Role**: Manages the user interface and client-side interactions.

  - **Communication**: Communicates with the backend through REST APIs.

- **Backend API**:

  - **Technology**: .NET

  - **Role**: Processes requests from the frontend, performs CRUD
    operations on the database, and manages business logic.

  - **Important Entry Points**: Endpoints for registration,
    authentication, user search, and other application functions.

- **Database**:

  - **Technology**: SQL Server

  - **Role**: Stores and manages all application data, including user
    information, authentication data, and other essential data.

  - **Data Security:** Ensures security measures, such as encrypting
    sensitive data.

**Secondary Components and External Services:**

- **Authentication Services**:

  - **Authentication Scheme:** Uses Negotiated authentication which is
    ideal for intranet scenarios and can utilize Kerberos or NTLM.

  - **Importance of Security:** Ensures that authentication and sessions
    are securely managed.

- **CORS (Cross-Origin Resource Sharing)**:

  - **Configuration in Program.cs:** Allows the frontend application
    from a certain origin to interact with the backend.

  - **Security Risk:** Inadequate configurations may allow malicious
    sites to access sensitive resources.

<!-- -->

- **Identification of Entry Points and Sensitive Data**

**Entry Points**

> Identifying the entry points through which data enters the system
> helps us to determine potential vulnerabilities and apply appropriate
> security measures. In the context of the application, entry points
> include:

- **API Endpoints**:

  - **User Registration (UserController.cs):** Receives user data,
    including email and password, for creating new accounts

  - **Authentication (UserController.cs):** Receives user credentials
    for authentication.

  - **User Search:** Allows searching for users in the database,
    possibly based on name or other attributes.

- **User Interface (Frontend):**

Forms and UI controls that collect information from users, such as
registration and login forms.

**Sensitive Data**

Recognizing and protecting the sensitive data managed by the application
is crucial to prevent information leaks and ensure compliance with data
protection regulations.

- **Authentication Data:**

<!-- -->

- **Passwords:** Managed by the .NET backend, where PasswordHasher is
  used for hashing passwords. This is crucial for protecting user
  credentials.

<!-- -->

- **Personal Data:**

<!-- -->

- **Emails and Usernames: T**hese details are collected during the
  registration process and are stored in the database. They are
  considered personal and sensitive data, as they can be used to
  directly identify an individual.

<!-- -->

- **Other Sensitive Data:**

<!-- -->

- **User Search:** The search functionality can process and display
  personal data, which requires security measures to prevent
  unauthorized exposure of personal information.

<!-- -->

- **Threat Modeling and Risk Assessment**

**Implementing the STRIDE Method:**

**S - Spoofing Identity:**

- **Description:** The threat refers to the possibility of an attacker
  impersonating a legitimate user.

- **Vulnerable Points:** User authentication, particularly at the /login
  and /register endpoints.

- **Proposed Security Measures:** Implementation of Multi-Factor
  Authentication (MFA) to add an additional layer of security.

> **T - Tampering with Data:**

- **Description:** The risk of data being altered by an attacker.

- **Vulnerable Points:** Data transactions at the API level, where user
  data is transmitted and received.

- **Proposed Security Measures:** Use of checksums or hashes to validate
  data integrity before processing.

> **R - Repudiation:**

- **Description:** The possibility that a user denies an action they
  actually performed.

- **Vulnerable Points:** Operations that are not properly logged.

- **Proposed Security Measures:** Implementation of a robust logging
  system that records all critical actions.

> **I - Information Disclosure:**

- **Description:** Unintentional exposure of confidential information.

<!-- -->

- **Vulnerable Points:** Data communication between client and server,
  data storage.

- **Proposed Security Measures:** Ensuring all communications are
  encrypted via HTTPS, using encryption for stored sensitive data.

> **D - Denial of Service:**

- **Description:** Attacks that prevent legitimate users from accessing
  the service.

- **Vulnerable Points:** Web server and network infrastructure.

- **Proposed Security Measures:** Implementation of rate limiting at the
  API level, use of anti-DDoS solutions.

> **E - Elevation of Privilege:**

- **Description:** When an attacker gains access to functions or data
  they should not have access to.

- **Vulnerable Points:** Insufficient access control within the
  application.

- **Proposed Security Measures:** Reviewing and strengthening access
  control policies, ensuring the principle of least privilege is adhered
  to.

Code and Configuration Security Audit  

- **Purpose of the Audit:**

<!-- -->

- **Introduction**: This security audit was conducted to assess the
  current security practices of the Blinq24/1 application, with the aim
  of identifying and remedying potential vulnerabilities.

- **Methodology:** The audit was based on reviewing system
  configurations and source code, using both manual and automated
  methods to evaluate compliance with best security practices.

<!-- -->

- **Summary of Findings**

<!-- -->

- The audit revealed several security concerns that require immediate
  attention, including issues related to the management of
  authentication and authorization, input validation, and security
  configurations in appsettings.json and Program.cs. These aspects are
  essential for protecting user data and the integrity of the
  application.

<!-- -->

- **Configuration Audit**

<!-- -->

- **Configurations in appsettings.json and Program.cs:** The current
  configurations are suitable for a development environment but require
  adjustments for production, such as tightening CORS policies and
  ensuring full encryption of sensitive data.

<!-- -->

- **Source Code Audit:**

<!-- -->

- **Authentication and Authorization:** It is recommended to implement
  multi-factor authentication to strengthen user access security.

- **Input Validation and Sanitization:** Implementing robust validation
  and sanitization methods is essential to prevent SQL Injection and XSS
  attacks.

- **Error Handling:** Needs improvement to prevent the disclosure of
  sensitive information through error messages.

<!-- -->

- **Recomandations**

<!-- -->

- **Strengthening Authentication:** Implement multi-factor
  authentication for all users.

- **Restricting CORS in Production Environment:** Adjust CORS policies
  to allow only specific domains.

- **Full Data Encryption:** Ensure encryption of data in transit and at
  rest for all sensitive data.

<!-- -->

- **Conclusions**

<!-- -->

- The audit identified several critical areas that require improvements
  to ensure compliance with industry standards and to protect user data.
  Implementing these recommendations will significantly enhance the
  security of the application.

<!-- -->

- **Anexes**

**Evaluation of appsettings.json**

> The content of **appsettings.json** is straightforward and focuses on
> configuring logging:

- **Logging:** The configuration specifies logging levels for the
  application and the ASP.NET Core framework. No sensitive details are
  directly exposed in this file, which is a good practice.

**Evaluation of Program.cs**

> The **Program.cs** file contains several important configurations:

- **Database Connection:** Uses environment variables to retrieve the
  connection string, which is a recommended practice to avoid hardcoding
  sensitive data.

- **CORS:** The CORS policy is configured to allow requests from
  http://localhost:4200, which is appropriate for development.

- **Authentication:** Uses the Negotiate authentication scheme, which is
  suitable for corporate scenarios.

- **Security Middleware:** The application enforces HTTPS redirection
  and applies CORS and authorization policies, which are essential for
  protecting transmitted data.
