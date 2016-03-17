This is a simple asp.net core 1.0 middlewear to add a livecheck to your app without having to make controllers, and what not. This is useful to keep your livecheck requests out of your MVC action filter lifecycle




## Usage

`Install-Package Alive.Net`

#### Ultra-Simple:
```c#
//this will default to /livecheck with 200
app.UseAlive(a =>
            {

            });

```
#### Simple
```c#

app.UseAlive(a =>
            {
                a.BodyText = "Im awesome";
                a.StatusCode = System.Net.HttpStatusCode.OK;
                a.LivecheckPath = new Microsoft.AspNet.Http.PathString("/CustomLivecheck");
            });

```

#### Complex
```c#

app.UseAlive(a =>
           a.OnLivecheckResponse = (response) =>
           {
               if(ThingsThatCouldMakeOurAppDown)
               {
                   response.BodyText = "awesome";
                   response.StatusCode = System.Net.HttpStatusCode.BadGateway;
               }
               else
               {
                   response.BodyText = "awesome";
                   response.StatusCode = System.Net.HttpStatusCode.OK;
               }
           });

```
