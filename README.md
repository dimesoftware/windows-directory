<p align="center"><img src="assets/tree.svg?raw=true" width="350" alt="Logo"></p>

# Directory Services 

[![Build Status](https://dev.azure.com/dimesoftware/Utilities/_apis/build/status/dimenics.windows-directory?branchName=master)](https://dev.azure.com/dimesoftware/Utilities/_build/latest?definitionId=180&branchName=master) ![Azure DevOps coverage](https://img.shields.io/azure-devops/coverage/dimesoftware/utilities/180) [![Dime.DirectoryServices package in Dime.Scheduler feed in Azure Artifacts](https://feeds.dev.azure.com/dimesoftware/_apis/public/Packaging/Feeds/a7b896fd-9cd8-4291-afe1-f223483d87f0/Packages/3510a0f3-b2de-42c1-b6d2-be1c163a8af2/Badge)](https://dev.azure.com/dimesoftware/Dime.Scheduler%20V2/_packaging?_a=package&feed=a7b896fd-9cd8-4291-afe1-f223483d87f0&package=3510a0f3-b2de-42c1-b6d2-be1c163a8af2&preferRelease=true)

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