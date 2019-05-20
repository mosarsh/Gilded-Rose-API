# Gilded-Rose-API
Become a merchant and order finest items online from Gilded Rose Inn

#### Table of content
1. Quick Start
2. Prerequisites
3. Environment Setup
   - IIS or IIS Express
   - Publish Database
4. Resources
   - Authentication
   - Inventory
   - Purchase
   - User
5. API Architecture
   - Three tier architecture
   - Repository Pattern
   - Unit of Work
   - Dependency Injection
6. Database
7. Running Tests
8. TODOs

## Quick Start
Several quick start options are available:
- [Download latest release](https://github.com/mosarsh/Gilded-Rose-API/archive/master.zip)
- Clone the repo: `git clone git@github.com:mosarsh/Gilded-Rose-API.git`

## Prerequisites
- SQL Server or SQL Express
- Visual Studio Community 

## Environment Setup
1. Open Control Panel and click Programs and Features Turn Windows features on
2. Publish database:
   - Open **GildedRose.DB** project
   - Double click **GildedRose.DB.publish.xml**
   - In the wizard add **Target Database Connection**
   - Write database name **GildedRose.DB**
   - Click Publish and that's it


*Database with the name **GildedRose.DB** will be created. You will see tables are created and initial data populated for Inventory, InventoryType tables.*

Note: In case having issue to connect IIS application pool to access sql database, follow this link for help.

## Resources

#### Authentication

Authentication is performed via **JWT bearer token** (JSON Web Token). Technically, JWT uses signature which is signed from headers and claims with security algorithm specified in the headers (example: HMACSHA256). Therefore, JWT is required to be transferred over HTTPs if you store any sensitive information in claims.
Now, in order to use JWT authentication, you don't really need an OWIN middleware if you have legacy Web Api system. The simple concept is how to provide JWT token and how to validate token when the request comes. That's it.
 
**Basically, a JWT token look like:**
```
<base64-encoded header>.<base64-encoded claims>.<base64-encoded signature>
```

**Example:**
```
eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImN1b25nIiwibmJmIjoxNDc3NTY1NzI0LCJleHAiOjE0Nzc1NjY5MjQsImlhdCI6MTQ3NzU2NTcyNH0.6MzD1VwA5AcOcajkFyKhLYybr3h13iZjDyHm9zysDFQ
 ```
 
JWT token has three sections:
- Header: JSON format which is encoded as a base64
- Claims: JSON format which is encoded as a base64.
- Signature: Created and signed based on Header and Claims which is encoded as a base64.

More info about JWT token [here](https://tools.ietf.org/html/rfc7519)

**How do we know if a user is authenticated?**
You will need to call below method to know if user is Authenticated or not.

```
User.Identity.IsAuthenticated
```
###### User Authentication Request

Request example:
```
POST api/Token
```

Request body
```
{
  "Email": "sample string 1",
  "Password": "sample string 2"
}
```

Request format: `application/json`

![](Images/Get-Toke.PNG)

Example Response
```
"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImFyc2hhay5tb3ZzaXNzaWFuQGdtYWlsLmNvbSIsIm5iZiI6MTU1ODMxNDk0NiwiZXhwIjoxNTU4MzE1NTQ1LCJpYXQiOjE1NTgzMTQ5NDZ9._qT0XnUWNUb1rO0zk8nsm0-drvTzmbyZef2nuurOh4Y"
```

#### Inventory
Inventory resource allows you get a list of all Inventories. No authentication needed for this resource. 

*Note: There is a need to add pagination.
It is recommended to utilizes cursor-based pagination via the start parameter. It takes an existing object Id value and return objects in reverse chronological order. The start parameter returns objects listed after the named object.*

Request example:
```
GET api/Inventory
```

![](Images/Get-Inventory.PNG)

Example response
```
[
    {
        "Id": 1,
        "Name": "Chips - Miss Vickies",
        "Description": "Sltr-haris Type II physl fx low end unsp femr, 7thD",
        "Price": 83.66
    },
    {
        "Id": 2,
        "Name": "Ice Cream Bar - Drumstick",
        "Description": "Hemorrhage due to genitourinary prosth dev/grft, subs",
        "Price": 30.46
    },
    {
        "Id": 3,
        "Name": "Pie Filling - Pumpkin",
        "Description": "Osseous stenosis of neural canal of sacral region",
        "Price": 74.24
    },
    {
        "Id": 4,
        "Name": "Beans - French",
        "Description": "Inj flexor musc/fasc/tend unsp finger at wrs/hnd lv, subs",
        "Price": 12.57
    }
]
```

#### Purchase
Purchase resource allows to buy an inventory item. It checks if inventory item is available. User can buy multiple items with one request call. If one of items is not available request will fail with the appropriate response message. 

While designing purchase logic it is important to consider deadlocks. When another user tries to purchase item, which is already locked by the first user. He will have to wait for the first user to release. When the first user leaves, then another user will lock and will enter the critical section.

**Code:**
```
lock (purchaseLock)
            {
                foreach (var purchaseModel in model)
                {
                    var availableInvetory = UnitOfWork.InventoryRepository.AvailableInventory(purchaseModel.Id, purchaseModel.Quantity);

                    if (availableInvetory != null)
                    {
                        availableInvetory.Stock = availableInvetory.Stock - purchaseModel.Quantity;
                        availableInvetory.ModifiedAt = DateTime.UtcNow;
                        UnitOfWork.InventoryRepository.Update(availableInvetory);

                        var purchase_inventory = new Purchase_Inventory
                        {
                            Purchase = new Purchase
                            {
                                UserId = userId,
                                PurchasedAt = DateTime.UtcNow
                            },
                            Inventory = availableInvetory,
                            NumberOfItems = purchaseModel.Quantity,
                            TotalPrice = availableInvetory.Price * purchaseModel.Quantity
                        };

                        UnitOfWork.PurchaseInventoryRepository.Add(purchase_inventory);                      
                    }
                }

```

**Request example:**
```
POST api/Purchase
```

Request body
```
[
  {
    "Id": 1,
    "Quantity": 2
  },
  {
    "Id": 1,
    "Quantity": 2
  }
]
```

Request format: `application/json`

![](Images/Purchase-Item.PNG)


#### User
User resource allows to register new user.

**Request example:**
```
POST api/User
```

Request body
```
{
  "FirstName": "sample string 2",
  "LastName": "sample string 3",
  "Email": "sample string 4",
  "Address": "sample string 5",
  "Phone": "sample string 6",
  "Company": "sample string 7",
  "PostalCode": "sample string 8",
  "Password": "sample string 9",
  "ConfirmPassword": "sample string 9"
}
```
Request format: `application/json`

![](Images/Add-User.PNG)

## API Architecture

Application uses following architecture, design patterns and concepts:
- Three tier architecture
- Repository pattern
- Unit of Work
- Dependency Injection



###### Three tier architecture
In three-tier architecture presentation, application processing, and data management functions are physically separated. This view holds that a layer is a logical structuring mechanism for the elements that make up the software solution, while a tier is a physical structuring mechanism for the system infrastructure.

**When do we actually need a 3-tier architecture?.**
The answer to this question isn't black and white at all because much of the time the use of a 3-tier architecture depends on the context of where you are and what you're trying to achieve. Here are some common arguments and counter-arguments concerning the use of a middle-tier application server.
Security: A physically separate middle-tier application server can increase security because it adds an extra level of indirection between the web server and the database. This means no direct route from the web server to the database server and (e.g.) SQL protocols/ports don't need to be allowed/opened on DMZ firewalls. If your web server gets hacked, your application server is safe. Also, the attack surface on the web server is reduced because there is a reduced amount of code, etc running on the web server. As a counter-argument, adding a level of physical indirection doesn't necessarily provide more security. In reality, how good are we as developers at creating secure middle-tiers? Security context propagation is complex and I've seen many middle-tiers that simply wrap up business logic/data access and expose them to the web-tier as a collection of unsecured web services.
Performance and scalability: A 3-tier architecture is more scalable than a 2-tier architecture because the web-tier and middle-tier can be scaled differently if necessary. An application server can be used to cache persistent data to increase performance and scalability. Counter-arguments include that scaling just a web server is simpler and caching could be done either locally in the web server or by duplicating the data in a different format (e.g. a NoSQL store alongside a SQL database).
Reuse and maintenance: A single physical middle-tier can be shared by a number of clients, so reuse and maintenance is increased. 

###### Repository pattern
The Repository Pattern is one of the most popular patterns to create an enterprise level application. It restricts us to work directly with the data in the application and creates new layers for database operations, business logic, and the application’s UI. If an application does not follow the Repository Pattern, it may have the following problems:
- Duplicate database operations codes
- Need of UI to unit test database operations and business logic
- Need of External dependencies to unit test business logic
- Difficult to implement database caching, etc.
- Using the Repository Pattern has many advantages:
- Your business logic can be unit tested without data access logic;
- The database access code can be reused;
- Your database access code is centrally managed so easy to implement any database access policies, like caching;
- It’s easy to implement domain logic;
- Your domain entities or business entities are strongly typed with annotations; and more.

#### Unit of Work in the Repository Pattern
Unit of Work is referred to as a single transaction that involves multiple operations of insert/update/delete and so on kinds. To say it in simple words, it means that for a specific user action (say purchase inventory item on a website), all the transactions like insert/update/delete and so on are done in one single transaction, rather then doing multiple database transactions. This means, one unit of work here involves insert/update/delete operations, all in one single transaction.

#### Why use Dependency Injection?

- Boilerplate code is reduced, as initializing of dependencies is done by the injector component.
- Extending the application becomes easier.
- Helps to enable loose coupling, which is important in application programming.


## Database
Database project is created for application. Database first approach has been chosen. 

**Why database first approach?**
- In a big applications there are always issues with generated SQL scripts. To write SQL script there is no better way to do it rather than doing it with SQL itself.
- Database deployment are always an issue, when it comes to column change and schema change you better see what change is happening.
You can always work on database optimization separately.


Here is the database table design that has been created:

![](Images/Database.PNG)

**The important case is that one user can purchase many items and one item can be purchase by many different users.**

Database model involves following sectors:
- Country
- User
- Purchase
- Inventory
- Audit

## Unit Tests
Unit tests are covering presentation layer and business layer. Database has been mock using nUnit framework.
To run unit test you need following to dos:
- Set as StartUp project GildedRose.Test project 
- Open **Test > Windows > Test Explorer**
- Run All tests

![](Images/Unit-Tests.PNG)
