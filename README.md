# EHRLICH CODING CHALLENGE

This repository contains an ASP.NET Core Web API project using Vertical Slice Architecture. This API serves as a backend service to provide data to client applications. Follow the instructions below to set up and run the project locally.

## Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) (version 8 or higher)
- [Visual Studio](https://visualstudio.microsoft.com/downloads/) or any other preferred code editor
- [Git](https://git-scm.com/downloads) (optional, if you prefer cloning via Git)

## Getting Started

1. **Clone the repository:**
```bash
git clone https://github.com/jerold-tolentino/EhrlichCodeChallenge.git
```

2. **Navigate to the project directory:**
```bash
cd your-repo
```

3. **Restore dependencies:**

```bash
dotnet restore
```

4. **Apply Migrations:**

```bash
dotnet ef database update
```


## Configuration

Before running the application, make sure to configure the necessary settings.

- **AppSettings:** Modify the `appsettings.json` file located in the `appsettings` directory to set up your database connection string or any other configuration settings required by your application.

## Running the Application

1. **Using the Command Line:**

```bash
dotnet run
```

2. **Using Visual Studio:**
- Open the solution in Visual Studio.
- Set the startup project to the Web API project.
- Press `F5` to run the project.

## Testing the API

Once the application is running, you can test the API endpoints using tools like [Postman](https://www.postman.com/) or simply by accessing the endpoints through a web browser or any HTTP client.

- By default, the base URL for the API will be `http://localhost:5051` (or `https://localhost:7073` for HTTPS).

## Deployment

When you're ready to deploy your application, you can publish it using the following command:
```bash
dotnet publish -c Release -o <publish-directory>
```

This will generate a publishable version of your application in the specified directory, which can then be deployed to your hosting environment.

## Further Information

For more detailed information on ASP.NET Core, refer to the [official documentation](https://docs.microsoft.com/en-us/aspnet/core/?view=aspnetcore-5.0).

## Contributing

Contributions are welcome! Feel free to open an issue or submit a pull request for any improvements or feature additions.

## License

This project is licensed under the [MIT License](LICENSE).
