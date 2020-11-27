<p align="center"><img src="assets/tree.svg?raw=true" width="350" alt="Logo"></p>

# Directory Services 

## About the project

This is a simple wrapper around Microsoft's directory services assembly.

## Installation

> 🚧 Warning: the packages are not available yet on NuGet.

Use the package manager NuGet to install this library:

`dotnet add package Dime.DirectoryServices`

## Usage

``` csharp
public async Task<boolean> Exists(string userName)
{
  WindowsUserStore store = new WindowsUserStore("Admin", "Admin123");
  string sid = await store.GetSidByLoginName(userName);

  return !string.IsNullOrEmpty(sid);
}
```

## Contributing

![PRs Welcome](https://img.shields.io/badge/PRs-welcome-brightgreen.svg?style=flat-square)

Pull requests are welcome. Please check out the contribution and code of conduct guidelines.

## License

![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)