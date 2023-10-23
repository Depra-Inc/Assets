// Copyright © 2023 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Depra.Assets.Delegates;
using Depra.Assets.Exceptions;
using Depra.Assets.Files;
using Depra.Assets.Idents;
using Depra.Assets.ValueObjects;
using FluentAssertions;
using NSubstitute;
using Xunit;
using Xunit.Abstractions;

namespace Depra.Assets.UnitTests;

public sealed class AssetGroupTests
{
	private const int ASSET_SIZE = 1;
	private const int GROUP_SIZE = 3;
	private const int CANCEL_DELAY = 1000;

	private readonly Stopwatch _stopwatch;
	private readonly AssetName _assetIdent;
	private readonly ITestOutputHelper _output;
	private readonly List<ILoadableAsset<object>> _testAssets;

	public AssetGroupTests(ITestOutputHelper output)
	{
		_output = output;
		_stopwatch = new Stopwatch();
		_assetIdent = new AssetName(nameof(AssetGroup<object>));
		_testAssets = new List<ILoadableAsset<object>>(GROUP_SIZE);

		var fileSize = new FileSize(ASSET_SIZE);
		for (var index = 0; index < GROUP_SIZE; index++)
		{
			var expectedAsset = new object();
			var fakeAssetFile = Substitute.For<ILoadableAsset<object>>();
			SetupTestAsset(fakeAssetFile, index, fileSize, expectedAsset);
			_testAssets.Add(fakeAssetFile);
		}
	}

	[Fact]
	public void Add_ShouldAddAssetToChildren()
	{
		// Arrange:
		var assetGroup = new AssetGroup<object>(_assetIdent);
		var fakeAsset = Substitute.For<ILoadableAsset<object>>();
		fakeAsset.Ident.RelativeUri.Returns(nameof(fakeAsset));

		// Act:
		assetGroup.AddAsset(fakeAsset);

		// Assert:
		assetGroup.Children.Count().Should().Be(1);
		assetGroup.Children.Should().Contain(fakeAsset);
	}

#if DEBUG
	[Fact]
	public void Add_ShouldThrowException_WhenAssetIsNull()
	{
		// Arrange:
		var assetGroup = new AssetGroup<object>(_assetIdent);

		// Act:
		var act = () => assetGroup.AddAsset(null);

		// Assert:
		act.Should().Throw<ArgumentNullException>();
	}

	[Fact]
	public void Add_ShouldThrowException_WhenAssetAlreadyAdded()
	{
		// Arrange:
		var assetGroup = new AssetGroup<object>(_assetIdent);
		var fakeAsset = Substitute.For<ILoadableAsset<object>>();
		fakeAsset.Ident.RelativeUri.Returns(nameof(fakeAsset));
		assetGroup.AddAsset(fakeAsset);

		// Act:
		var act = () => assetGroup.AddAsset(fakeAsset);

		// Assert:
		act.Should().Throw<AssetAlreadyAddedToGroup>();
	}
#endif

	[Fact]
	public void Load_ShouldSucceed()
	{
		// Arrange:
		var assetGroup = new AssetGroup<object>(_assetIdent, _testAssets);

		// Act:
		var loadedAssets = assetGroup.Load().ToArray();

		// Assert:
		loadedAssets.Should().NotBeNull();
		loadedAssets.Should().NotBeEmpty();

		// Debug:
		_output.WriteLine($"Loaded asset group with {_testAssets.Count} children.");
	}

#if DEBUG
	[Fact]
	public void Load_ShouldThrowException_WhenLoadedAssetIsNull()
	{
		// Arrange:
		var assetGroup = new AssetGroup<object>(_assetIdent);
		var fakeAsset = Substitute.For<ILoadableAsset<object>>();
		fakeAsset.Load().Returns(null);
		fakeAsset.IsLoaded.Returns(false);
		fakeAsset.Ident.RelativeUri.Returns(nameof(fakeAsset));
		assetGroup.AddAsset(fakeAsset);

		// Act:
		var act = () => assetGroup.Load();

		// Assert:
		act.Should().Throw<AssetCannotBeLoadedFromGroup>();
	}

	[Fact]
	public void Load_ShouldThrowsException_WhenGroupAlreadyContainsAsset()
	{
		// Arrange:
		var assetGroup = new AssetGroup<object>(_assetIdent);
		var fakeAsset = Substitute.For<ILoadableAsset<object>>();
		fakeAsset.Load().Returns(new object());
		fakeAsset.IsLoaded.Returns(false);
		fakeAsset.Ident.RelativeUri.Returns(nameof(fakeAsset));
		assetGroup.AddAsset(fakeAsset);
		assetGroup.Load();

		// Act:
		var act = () => assetGroup.Load();

		// Assert:
		act.Should().Throw<AssetAlreadyLoaded>();
	}
#endif

	[Fact]
	public async Task LoadAsync_ShouldSucceed()
	{
		// Arrange:
		var cts = new CancellationTokenSource(CANCEL_DELAY);
		var assetGroup = new AssetGroup<object>(_assetIdent, _testAssets);

		// Act:
		_stopwatch.Restart();
		var loadedAssets = await assetGroup.LoadAsync(cancellationToken: cts.Token);
		loadedAssets = loadedAssets.ToArray();
		_stopwatch.Stop();

		// Assert:
		loadedAssets.Should().NotBeNull();
		loadedAssets.Should().NotBeEmpty();

		// Debug:
		_output.WriteLine($"Loaded asset group with {_testAssets.Count} children " +
		                  $"in {_stopwatch.ElapsedMilliseconds} ms.");
	}

	[Fact]
	public async Task LoadAsync_WithProgress_ShouldSucceed()
	{
		// Arrange:
		var callbackCalls = 0;
		var callbacksCalled = false;
		DownloadProgress lastProgress = default;
		var assetGroup = new AssetGroup<object>(_assetIdent, _testAssets);
		var cancellationToken = new CancellationTokenSource(CANCEL_DELAY).Token;

		// Act:
		_stopwatch.Restart();
		await assetGroup.LoadAsync(
			progress =>
			{
				callbackCalls++;
				callbacksCalled = true;
				lastProgress = progress;
			}, cancellationToken);

		_stopwatch.Stop();

		// Assert:
		callbacksCalled.Should().BeTrue();
		callbackCalls.Should().BeGreaterThan(0);
		lastProgress.Should().BeEquivalentTo(DownloadProgress.Full);

		// Debug:
		_output.WriteLine("Progress event was called " +
		                  $"{callbackCalls} times " +
		                  $"in {_stopwatch.ElapsedMilliseconds} ms. " +
		                  $"Last value is {lastProgress.NormalizedValue}.");
	}

	[Fact]
	public void Name_ShouldBeEqual()
	{
		// Arrange:
		var assetGroup = new AssetGroup<object>(_assetIdent, _testAssets);

		// Act:
		var name = assetGroup.Name;

		// Assert:
		name.Should().Be(_assetIdent.Name);
	}

	[Fact]
	public void SizeOfGroup_ShouldBeThreeBytes()
	{
		// Arrange:
		_testAssets.Sum(x => x.Size.SizeInBytes).Should().Be(3);
		var assetGroup = new AssetGroup<object>(_assetIdent, _testAssets);
		var unused = assetGroup.Load();

		// Act:
		var assetSize = assetGroup.Size;

		// Assert:
		assetSize.Should().NotBe(FileSize.Zero);
		assetSize.Should().NotBe(FileSize.Unknown);

		// Cleanup:
		_output.WriteLine($"Size of {assetGroup.Ident.Uri} is {assetSize.ToHumanReadableString()}.");
	}

	[Fact]
	public void Unload_ShouldSucceed()
	{
		// Arrange:
		var resourceAsset = new AssetGroup<object>(_assetIdent, _testAssets);
		var unused = resourceAsset.Load();

		// Act:
		resourceAsset.Unload();

		// Assert:
		resourceAsset.IsLoaded.Should().BeFalse();

		// Debug:
		_output.WriteLine("Asset group unloaded.");
	}

	private void SetupTestAsset(ILoadableAsset<object> fakeAssetFile, int index,
		FileSize fileSize, object expectedAsset)
	{
		fakeAssetFile.Ident.Uri.Returns($"{_assetIdent.Name}/{index}");
		fakeAssetFile.Size.Returns(fileSize);
		fakeAssetFile.Load().Returns(expectedAsset);
		fakeAssetFile.LoadAsync(Arg.Any<DownloadProgressDelegate>(),
				Arg.Any<CancellationToken>())
			.Returns(Task.FromResult(expectedAsset));
	}
}