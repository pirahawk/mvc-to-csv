==Create the Package==

* This project has a dependency on _[Microsoft.AspNet.Mvc 3.0.20105.1](https://www.nuget.org/packages/Microsoft.AspNet.Mvc/3.0.20105.1)_
* To create a new `.nuspec` file, navigate to folder `../MvcToCsv` and run the following command
```
nuget pack MvcToCsv.csproj
```
* Once this is done, you can create a new _*.nupkg_ file via running
```
nuget pack MvcToCsv.csproj -Prop Configuration=Release
```