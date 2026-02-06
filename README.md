# GDTask.Nuget.GlobalCancellation

Adds global cancellations to [GDTask.Nuget](https://github.com/Delsin-Yu/GDTask.Nuget).

[![GitHub Release](https://img.shields.io/github/v/release/Joy-less/GDTask.Nuget.GlobalCancellation)](https://github.com/Joy-less/GDTask.Nuget.GlobalCancellation/releases/latest)
[![NuGet Version](https://img.shields.io/nuget/v/GDTask.GlobalCancellation)](https://www.nuget.org/packages/GDTask.GlobalCancellation)
![NuGet Downloads](https://img.shields.io/nuget/dt/GDTask.GlobalCancellation)
[![Stars](https://img.shields.io/github/stars/Joy-less/GDTask.Nuget.GlobalCancellation?color=brightgreen)](https://github.com/Joy-less/GDTask.Nuget.GlobalCancellation/stargazers)
[![License](https://img.shields.io/badge/license-MIT-blue.svg)](https://github.com/Joy-less/GDTask.Nuget.GlobalCancellation/blob/main/LICENSE)

## Usage

```cs
_ = GDTask.Create(async () => {
    GD.Print(1);
    await GDTask.Delay(TimeSpan.FromSeconds(1.0)).AttachGlobalCancellation();
    GD.Print(2);
});

GDTaskGlobalCancellationManager.Cancel();
```
```
1
OperationCanceledException
```