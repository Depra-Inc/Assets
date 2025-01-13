// SPDX-License-Identifier: Apache-2.0
// © 2023-2025 Nikolay Melnikov <n.melnikov@depra.org>

namespace Depra.Assets.QoL.UnitTests;

public sealed class AssetStorageTests
{
	private readonly IAssetStorage _storage = new AssetStorage();

	[Fact]
	public void Get_WithNonExistentAsset_ReturnsNull()
	{
		// Arrange:
		var assetUri = new AssetName("test");

		// Act:
		var result = _storage.Get<IBaseAsset>(assetUri);

		// Assert:
		result.Should().BeNull();
	}

	[Fact]
	public void Get_WithExistentAsset_ReturnsAsset()
	{
		// Arrange:
		var assetUri = new AssetName("test");
		var asset = Substitute.For<IAssetFile<NestedAsset>>();
		asset.Metadata.Returns(new AssetMetadata(assetUri, FileSize.Unknown));
		_storage.Add(asset);

		// Act:
		var result = _storage.Get<IBaseAsset>(assetUri);

		// Assert:
		result.Should().BeSameAs(asset);
	}

	public interface IBaseAsset { }

	public sealed class NestedAsset : IBaseAsset { }
}