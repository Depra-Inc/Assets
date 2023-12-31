# Depra.Assets

<div align="center">
    <strong><a href="README.md">English</a> | <a href="README.RU.md">Русский</a></strong>
</div>

<details>
<summary>Оглавление</summary>

- [Введение](#введение)
    - [Особенности](#особенности)
- [Установка](#установка)
- [Интерфейсы](#интерфейсы)
- [Классы](#классы)
- [Примеры использования](#примеры-использования)
- [Поддержка](#поддержка)
- [Лицензия](#лицензия)

</details>

## Введение

Данная библиотека предоставляет набор интерфейсов и классов для управления ресурсами. 
Она позволяет создавать группы ресурсов, загружать и выгружать их, 
а также предоставляет асинхронную загрузку с обратной связью о прогрессе.

### Особенности:

- Однородный **API** для загрузки ассетов из различных источников;
- Поддержка различных типов ассетов;
- Предоставление информации о прогрессе загрузки;
- Расширяемость.

## Установка

### Ручная

Добавьте файл **.dll** из последнего релиза в свой проект.

### Через NuGet

Добавьте `Depra.Assets` в свой проект с помощью **NuGet**.

## Интерфейсы

### `IAssetFile`

Интерфейс, представляющий базовую информацию о файле ресурса.

- `Ident`: Идентификатор ресурса.
- `Size`: Размер файла ресурса.

### `ILoadableAsset<TAsset>`

Интерфейс, предоставляющий методы для загрузки и выгрузки ресурса.

- `IsLoaded`: Возвращает `true`, если ресурс загружен, иначе `false`.
- `Load()`: Загружает ресурс синхронно.
- `Unload()`: Выгружает ресурс.
- `LoadAsync(onProgress, cancellationToken)`: Загружает ресурс асинхронно с обратной связью о прогрессе.

### `IAssetIdent`

Интерфейс для идентификатора ресурса.

- `Uri`: Абсолютный URI ресурса.
- `RelativeUri`: Относительный URI ресурса.

## Классы

### `AssetName`

Класс, представляющий имя ресурса.

- `Name`: Имя ресурса.
- `Uri`: Абсолютный URI ресурса.
- `RelativeUri`: Относительный URI ресурса.

### `AssetGroup<TAsset>`

Класс для управления группой ресурсов.

- `Name`: Имя группы ресурсов.
- `Ident`: Идентификатор группы ресурсов.
- `Size`: Общий размер группы ресурсов.
- `IsLoaded`: Возвращает `true`, если все ресурсы в группе загружены.
- `Children`: Коллекция дочерних ресурсов в группе.
- `AddAsset(asset)`: Добавляет ресурс в группу.
- `Load()`: Загружает все ресурсы в группе синхронно.
- `LoadAsync(onProgress, cancellationToken)`: Загружает все ресурсы в группе асинхронно с обратной связью о прогрессе.
- `Unload()`: Выгружает все ресурсы в группе.
- `GetEnumerator()`: Возвращает перечислитель дочерних ресурсов.

## Примеры использования

```csharp
// Пример создания группы ресурсов.
var groupName = new AssetName("textures");
var assetGroup = new AssetGroup<Texture>(groupName);

// Добавление ресурсов в группу.
assetGroup.AddAsset(new TextureAsset("texture1.png"));
assetGroup.AddAsset(new TextureAsset("texture2.png"));

// Загрузка ресурсов группы.
assetGroup.Load();

// Итерация по загруженным ресурсам.
foreach (var texture in assetGroup)
{
    // Использование загруженных текстур.
}

// Выгрузка ресурсов группы.
assetGroup.Unload();

// Асинхронная загрузка ресурсов группы.
await assetGroup.LoadAsync(
    onProgress: (progress) =>
    {
        // Обратная связь о прогрессе загрузки.
    },
    cancellationToken: default
);

assetGroup.Unload();
```

## Поддержка

Я независимый разработчик,
и большая часть разработки этого проекта выполняется в свободное время.
Если вы заинтересованы в сотрудничестве или найме меня для проекта,
ознакомьтесь с моим [портфолио](https://github.com/Depression-aggression)
и [свяжитесь со мной](mailto:g0dzZz1lla@yandex.ru)!

## Лицензия

Этот проект распространяется под лицензией **Apache-2.0**

Copyright (c) 2023 Николай Мельников
[g0dzZz1lla@yandex.ru](mailto:g0dzZz1lla@yandex.ru)