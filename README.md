# .Net-8-Console-App-Template
Configuration + Logging Template without Dependency Injection

## Template includes
+ Configuration from appsettings.json
+ Logging from 2 providers, Console and EventLog (if the OS platfrom is Windows)

## Dependencies
```xml
<ItemGroup>
  <PackageReference Include="Microsoft.Extensions.Configuration" Version="8.0.0" />
  <PackageReference Include="Microsoft.Extensions.Configuration.Binder" Version="8.0.1" />
  <PackageReference Include="Microsoft.Extensions.Configuration.Json" Version="8.0.0" />
  <PackageReference Include="Microsoft.Extensions.Logging" Version="8.0.0" />
  <PackageReference Include="Microsoft.Extensions.Logging.Console" Version="8.0.0" />
  <PackageReference Include="Microsoft.Extensions.Logging.EventLog" Version="8.0.0" />
</ItemGroup>
```
