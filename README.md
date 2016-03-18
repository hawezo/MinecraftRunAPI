# MinecraftRunAPI

MinecraftRunAPI is a class library created in C# (usable in all .NET languages) to facilitate the launching of Minecraft from your applications.
It is really simple to use.

## Preparation

Before you can use MinecraftRunAPI to launch Minecraft, you will have to get the natives corresponding at the version you want to launch.
Those natives are automatically downloaded by the official launcher at runtime, and deleted after Minecraft is closed.


#### Simple method

You can just copy the natives folder when Minecraft run, in going to `C:\Users\<You>\AppData\Roaming\.minecraft\versions\<Your version>` and copying the folder named `<some-random-numbers>-natives` where you want (personnaly I created a folder at `C:\Users\<You>\AppData\Roaming\.minecraft\natives` and put all my natives in there).
The inconveniant is that it will work only for you, and not your users.
You can make your launcher downloading thoses natives, for example.


#### Less simple method

The second way to get the natives is to download them directly from the link situated at the end of the `<version>.json` file located in `C:\Users\<you>\AppData\Roaming\.minecraft\versions\<version>\`.
You can use, for example, [the JSON.NET library](http://www.newtonsoft.com/json) for parsing this file.
_Maybe I'll make MinecraftRunAPI automatically retrieve those files, later._


## Getting the command line

Now, you can easily get the command line that will be used to launcher Minecraft:

```csharp
CommandLine MinecraftCommand = new CommandLine(
  @"1.9",
  new UserCredentials("<UUID>, <Username>, <AccessToken>"),
  @"C:\Users\<You>\AppData\Roaming\.minecraft\natives\<version>-natives"
  );
```

The third argument is the path to the `natives` folder.
This constructor can be overloaded to modify the arguments, like the heap min, initial and max size of the JVM, the game path, or the version type.

Now you have prepared the command, you can either retrieve it or directly launch Minecraft via the CommandLine object.

```csharp
Console.WriteLine(MinecraftCommand.GetCommandLine(true));
```
The parameter of the method `GetCommandLine()` determine if the java executable path will be set before the command.

```csharp
MinecraftCommand.Run();
```
If you launch Minecraft like this, you'll have to show the console in order to get the log.
_I'm working on a way to redirect the stream, which is easy and should not take a long time._


## UserCredentials

The UserCredentials class is compatible with MojangAPI.
It means you can use the requests of [MojangAPI](https://github.com/hawezo/MojangAPI) to run Minecraft through MinecraftRunAPI.

```csharp
// Creating the auth request
Request authRequest = new Request(Request.Method.POST, URL.AUTHENTICATION.SIGN_IN);
string raw = authRequest.Execute(Header.Authentication.Signin("<you>", "<your password>"));
AuthenticationResponse authResponse = new AuthenticationResponse(raw);

// Creating the uuid request
Request uuidRequest = new Request(Request.Method.GET, URL.UUID_BY_NAME, authResponse.GetResponse.PlayerName);
raw = uuidRequest.Execute();
UuiByNameResponse uuidResponse = new UuiByNameResponse(raw);

// Creating the MinecraftLaunching credentials
UserCredentials credentials = new UserCredentials(
    (MinecraftLaunching.Structures.AuthenticationResponse)authResponse.GetResponse,
    (MinecraftLaunching.Structures.UuidByNameResponse)uuidResponse.GetResponse);
    
CommandLine MinecraftCommand = new CommandLine(
@"1.9",
new UserCredentials(
    (MinecraftLaunching.Structures.AuthenticationResponse)authResponse.GetResponse,
    (MinecraftLaunching.Structures.UuidByNameResponse)uuidResponse.GetResponse),
@"C:\Users\<you>\AppData\Roaming\.minecraft\natives\1.9-natives"
);
```
This example will run Minecraft 1.9 if the folder `C:\Users\<you>\AppData\Roaming\.minecraft\natives\1.9-natives` contains the 1.9 natives files.

## Ro-do

- [ ] Add a way to redirect the console stream

## Issues

If you spotted an issue, please start a new ticket. I will answer as soon as possible. Thank you.
