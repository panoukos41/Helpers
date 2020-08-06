# Panoukos41.Helpers

This is a project containing helper classes and extensions i have been creating and using through out my projects.  
It also contains modified usefull classes from [Template10](https://github.com/Windows-XAML/Template10) and [ReactiveUI](https://github.com/reactiveui/ReactiveUI) with licence included.

# Getting Started
Install the packages from nuget depending on what you need!  

| Package                       | Nuget                                | Description                | Platforms                      |
|-------------------------------|--------------------------------------|----------------------------|--------------------------------|
| Panoukos41.Helpers            |                                      | Cross-Platform helpers.    | All<sup>1</sup>                |
| Panoukos41.Helpers.AndroidX   |                                      | AndroidX helpers.          | MonoAndroid9.0                 |
| Panoukos41.Helpers.Mvvm       |                                      | Simple MVVM helpers.       | All<sup>1</sup>                |
| Panoukos41.Helpers.ReactiveUI |                                      | ReactiveUI helpers.        | MonoAndroid9.0                |
| Panoukos41.Helpers.UI         |                                      | UI Components and Helpers. | MonoAndroid9.0 UAP<sup>2</sup> |

> 1 => .NET Standard (1.4, 2.0), MonoAndroid (9.0), UAP (14393, 16299)  
> 2 => UAP (14393, 16299)  

# Contributing
Feedback and contributions are welcome!  

If you have a pull request please make it in dev.

Following reactiveui and xamarin essentials structure the csproj compiles the code in the following folders:  

| Folder                   | Code to put                                             |
|--------------------------|---------------------------------------------------------|
| Abstractions             | Non platform specific code that can be used everywhere. |
| Android                  | Android specific code.                                  |
| Ios<sup>1</sup>          | IOS specific code. (I have none :smirk:)                |
| Mac<sup>1</sup>          | Mac specific code. (I have none :smirk:)                |
| Netstandard              | .Net Standard specific code.                            |
| Uwp                      | Universal Windows Platform (Store apps) specific code.  |
| Net<sup>1</sup>          | .Net Framework specific code. (I have none :smirk:)     |
| Netcoreapp<sup>1</sup>   | .Net Core. (I have none :smirk:)                        |
| Tizen<sup>1</sup>        | Tizen specific code. (I have none :smirk:)              |

>1 => Folders don't exist, as a result the csproj "code" deosn't exist.
>This is because i have no code for these platforms yet, 
>this means that the name might change if i ever add code for them.
>For example the "Net" folder may change since .net 5 might make
>it the default name to target etc, for now it stands for .Net Framework. 

You should just follow the app structure and avoid unnecessary spaces etc. 
You can ask me anything if you find this helpful and want to contribute something 
even if it corrects a licence or a typo :blush:, happy coding!

# License
The project is licenced under the [MIT License](https://github.com/panoukos41/Helpers/blob/master/LICENSE.md)