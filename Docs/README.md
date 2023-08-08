# Depra.Assets

<div align="center">
    <strong><a href="README.md">English</a> | <a href="README.RU.md">Русский</a></strong>
</div>

<details>
<summary>Table of Contents</summary>

- [Introduction](#introduction)
    - [Features](#features)
- [Installation](#installation)
- [Interfaces](#interfaces)
- [Classes](#classes)
- [Usage Examples](#usage-examples)
- [Support](#support)
- [License](#license)

</details>

## Introduction

This library provides a set of interfaces and classes for managing resources.
It allows you to create resource groups, load and unload them,
and also provides asynchronous loading with progress feedback.

### Features:

- Object-oriented approach to resource management
- Support for various types of resources
- Event support
- Extensibility

## Installation

### Manual

Add the **.dll** file from the latest release to your project.

### Via NuGet

Add `Depra.Assets` to your project using **NuGet**.

## Interfaces

### `IAssetFile`

An interface representing basic information about a resource file.

- `Ident`: Resource identifier.
- `Size`: Resource file size.

### `ILoadableAsset<TAsset>`

An interface providing methods for loading and unloading a resource.

- `IsLoaded`: Returns `true` if the resource is loaded, otherwise `false`.
- `Load()`: Loads the resource synchronously.
- `Unload()`: Unloads the resource.
- `LoadAsync(onProgress, cancellationToken)`: Loads the resource asynchronously with progress feedback.

### `IAssetIdent`

An interface for a resource identifier.

- `Uri`: Absolute URI of the resource.
- `RelativeUri`: Relative URI of the resource.

## Classes

### `AssetName`

A class representing the name of a resource.

- `Name`: Resource name.
- `Uri`: Absolute URI of the resource.
- `RelativeUri`: Relative URI of the resource.

### `AssetGroup<TAsset>`

A class for managing a group of resources.

- `Name`: Group name of resources.
- `Ident`: Group resource identifier.
- `Size`: Total size of the group of resources.
- `IsLoaded`: Returns `true` if all resources in the group are loaded.
- `Children`: Collection of child resources in the group.
- `AddAsset(asset)`: Adds a resource to the group.
- `Load()`: Loads all resources in the group synchronously.
- `LoadAsync(onProgress, cancellationToken)`: Loads all resources in the group asynchronously with progress feedback.
- `Unload()`: Unloads all resources in the group.
- `GetEnumerator()`: Returns an enumerator of child resources.

## Usage Examples

```csharp
// Example of creating a resource group.
var groupName = new AssetName("textures");
var assetGroup = new AssetGroup<Texture>(groupName);

// Adding resources to the group.
assetGroup.AddAsset(new TextureAsset("texture1.png"));
assetGroup.AddAsset(new TextureAsset("texture2.png"));

// Loading resources of the group.
assetGroup.Load();

// Iterating through loaded resources.
foreach (var texture in assetGroup)
{
    // Using the loaded textures.
}

// Unloading resources of the group.
assetGroup.Unload();

// Asynchronously loading resources of the group.
await assetGroup.LoadAsync(
    onProgress: (progress) =>
    {
        // Progress feedback during loading.
    },
    cancellationToken: default
);

assetGroup.Unload();
```

## Support

I am an independent developer,
and most of the development on this project is done in my spare time.
If you're interested in collaborating or hiring me for a project, check
out [my portfolio](https://github.com/Depression-aggression) and [reach out](mailto:g0dzZz1lla@yandex.ru)!

## License

This project is licensed under the **Apache-2.0 License**.

Copyright (c) 2022-2023 Nikolay Melnikov
[g0dzZz1lla@yandex.ru](mailto:g0dzZz1lla@yandex.ru)




