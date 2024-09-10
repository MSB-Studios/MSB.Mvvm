# MSB.Mvvm (NET Standard)

## Description
The MVVM Package is a NuGet package that provides a set of classes and utilities for implementing the Model-View-ViewModel architectural pattern in your .NET applications. The MVVM pattern helps separate concerns and provides a clear separation between the user interface (View), business logic (ViewModel), and data (Model).

## Features

### ViewModelBase
- The `ViewModelBase` class is an abstract base class that implements the `INotifyPropertyChanged` interface and provides common functionality for ViewModel classes. It includes methods for raising property change notifications and managing property values.

### RelayCommand
- The `RelayCommand` class is an implementation of the `ICommand` interface that allows binding commands to methods in the ViewModel. It provides a way to encapsulate and execute parameterless commands.

### RelayCommand<T>
- The `RelayCommand<T>` class is a generic version of the `RelayCommand` class that allows binding commands to methods in the ViewModel with a single parameter of type T.

### RelayCommandAsync
- The `RelayCommandAsync` class is an asynchronous version of the `RelayCommand` class. It allows binding commands to asynchronous methods in the ViewModel.

### RelayCommandAsync<T>
- The `RelayCommandAsync<T>` class is a generic version of the `RelayCommandAsync` class that allows binding commands to asynchronous methods in the ViewModel with a single parameter of type T.

## Installation

To install the library simply download it from [NuGet](https://www.nuget.org/packages/MSB.Mvvm/) and add the reference to your project.  
Below we give you several options to install it using your preferred package manager.

- #### .NET CLI
```bash
> dotnet add package MSB.Mvvm --version 2.X.X
```
- #### Package Manager
```bash
PM> Install-Package MSB.Mvvm -Version 2.X.X
```
- #### PackageReference
```bash
<PackageReference Include="MSB.Mvvm" Version="2.X.X" />
```
- #### Paket CLI
```bash
> paket add MSB.Mvvm --version 2.X.X
```
- #### Script & Interactive
```bash
> #r "nuget: MSB.Mvvm, 2.X.X"
```
- #### Cake
```bash
// Install MSB.Mvvm as a Cake Addin
#addin nuget:?package=MSB.Mvvm&version=2.X.X

// Install MSB.Mvvm as a Cake Tool
#tool nuget:?package=MSB.Mvvm&version=2.X.X
```

## Usage
To use the MVVM Package in your project, follow these steps:

1. Install the MVVM Package from NuGet using the command mentioned above.
2. Create your ViewModel classes by inheriting from the `ViewModelBase` class.
3. Implement your commands using the `RelayCommand` or `RelayCommandAsync` classes.
4. Bind your commands to UI elements in your View.
5. Enjoy the benefits of the MVVM pattern in your application!

## Example
Here's an example of a simple ViewModel that uses the MVVM Package:

``` csharp
using System.Windows.Input;
using MSB.Mvvm.Input;
using MSB.Mvvm;

public class MyViewModel : ViewModelBase
{
    public MyViewModel()
    {
        ShowMessageCommand = new RelayCommand(ShowMessage);
    }

    public string Message
    {
        get => _message;
        set => SetValue(ref _message, value);
    }

    public ICommand ShowMessageCommand
    {
        get;
    }

    private void ShowMessage()
    {
        Message = "Hello, MVVM!";
    }

    private string _message;
}
```

## License
Feel free to modify the content according to your package's features and specifications.